using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    private Rigidbody _rigid;

    private void Awake()
    {
        if (per > -1)
        {
            _rigid = GetComponent<Rigidbody>();
        }
    }

    public void Init(float damage, int per,UnityEngine.Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            _rigid.velocity = dir * 15f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") || per == -1) return;

        per--;

        if (per == 0)
        {
            _rigid.velocity = UnityEngine.Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
