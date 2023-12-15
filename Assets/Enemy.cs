using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

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
    }
}
