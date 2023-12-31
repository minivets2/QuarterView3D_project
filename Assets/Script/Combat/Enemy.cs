using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Unit
{
    public float damage;
    public Transform damagePopupPoint;
        
    private Rigidbody _target;
    private bool _isLive;
    private bool _isDead;
    public BlinkEffect[] _blinkEffect;
    public bool isAreaAttack;
    private float _areaAttackDamage;
    private float _timer;

    private void OnEnable()
    {
        rigid = GetComponent<Rigidbody>();
        _target = GameManager.instance.player.GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        _isDead = false;
        _blinkEffect = GetComponentsInChildren<BlinkEffect>();
    }

    private void Update()
    {
        if (_isDead) return;

        if (isAreaAttack)
        {
            _timer += Time.deltaTime;

            if (_timer > 1)
            {
                _timer = 0f;
                GetAttack(_areaAttackDamage);
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    public override void Move()
    {
        if (_isDead) return;
        
        Vector3 dirVec = _target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;
    }
    
    
    public override void Turn()
    {
        Vector3 dirVec = _target.position - rigid.position;
        transform.LookAt(transform.position + dirVec);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("AreaAttack")) return;

        if (!isAreaAttack)
        {
            isAreaAttack = true;
            _areaAttackDamage = other.GetComponent<Bullet>().damage;
            GetAttack(_areaAttackDamage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("AreaAttack")) return;
        
        isAreaAttack = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;

        GetAttack(other.GetComponent<Bullet>().damage);
    }

    void GetAttack(float playerDamage)
    {
        health -= playerDamage;
        AudioManager.instance.PlaySFX(SoundName.Hit);

        for (int i = 0; i < _blinkEffect.Length; i++)
        {
            _blinkEffect[i].StartBlinking();    
        }
        
        DamagePopupManager.instance.CreatePopup(damagePopupPoint.position, playerDamage.ToString());
        
        if (health > 0)
        {
            
        }
        else
        {
            Dead();
        }
    }

    void Dead()
    {
        ani.SetBool("isDead", true);
        //AudioManager.instance.PlaySFX(SoundName.EnemyDead);
        _isDead = true;
        
        Invoke(nameof(Destroy), 1f);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
        
        Transform exp = GameManager.instance.pool.Get(3).transform;
        exp.position = transform.position;
        
        GameManager.instance.enemyCount.IncreaseEnemyCount();
    }
}
