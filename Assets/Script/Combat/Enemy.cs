using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float damage;
    public Transform damagePopupPoint;
        
    private Rigidbody _target;
    private bool _isLive;
    private Rigidbody _rigid;
    private Animator _ani;
    private bool _isDead;
    public BlinkEffect[] _blinkEffect;

    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody>();
        _target = GameManager.instance.player.GetComponent<Rigidbody>();
        _ani = GetComponent<Animator>();
        _isDead = false;
        _blinkEffect = GetComponentsInChildren<BlinkEffect>();
    }

    private void FixedUpdate()
    {
        if (_isDead) return;
        
        Vector3 dirVec = _target.position - _rigid.position;
        Vector3 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        _rigid.MovePosition(_rigid.position + nextVec);
        _rigid.velocity = Vector3.zero;
        transform.LookAt(transform.position + dirVec);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bullet")) return;

        health -= other.GetComponent<Bullet>().damage;

        for (int i = 0; i < _blinkEffect.Length; i++)
        {
            _blinkEffect[i].StartBlinking();    
        }
        
        DamagePopupManager.instance.CreatePopup(damagePopupPoint.position, other.GetComponent<Bullet>().damage.ToString());

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
        _ani.SetBool("isDead", true);
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
