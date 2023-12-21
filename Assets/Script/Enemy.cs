using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float damage;

    private Rigidbody _target;
    private bool _isLive;
    private Rigidbody _rigid;

    private void OnEnable()
    {
        _rigid = GetComponent<Rigidbody>();
        _target = GameManager.instance.player.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
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
        gameObject.SetActive(false);
        Transform exp = GameManager.instance.pool.Get(3).transform;
        exp.position = transform.position;
        
        GameManager.instance.enemyCount.IncreaseEnemyCount();
    }
}
