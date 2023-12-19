using System;
using Unity.VisualScripting;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Ammo,
        Coin,
        Grenade,
        Heart,
        Weapon
    };

    public ItemType type;
    public float exp;
    
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private bool _hasTarget;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private const float Speed = 20f;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _hasTarget = false;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * 80 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Magnet();
    }

    private void Magnet()
    {
        if (_hasTarget == false)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        if (_hasTarget)
        {
            Vector3 targetDirection = (_targetPosition - transform.position).normalized;
            _rigidbody.velocity = new Vector3(targetDirection.x, targetDirection.y, targetDirection.z) * Speed;
        }
    }

    public void SetTarget(bool value)
    {
        _targetPosition = value ? GameManager.instance.player.transform.position : transform.position;
        _hasTarget = value;
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
