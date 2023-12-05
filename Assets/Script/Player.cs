using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    
    private float _hAxis;
    private float _vAxis;
    private Vector3 _moveVec;
    private Vector3 _dodgeVec;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool _wDown;
    private bool _jDown;
    private bool _isJump;
    private bool _isDodge;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
    }

    void GetInput()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");
        _wDown = Input.GetButton("Walk");
        _jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        _moveVec = new Vector3(_hAxis, 0, _vAxis).normalized;

        if (_isDodge)
            _moveVec = _dodgeVec;

        transform.position += _moveVec * speed * (_wDown ? 0.3f : 1f) * Time.deltaTime;

        _animator.SetBool("isRun", _moveVec != Vector3.zero);
        _animator.SetBool("isWalk", _wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + _moveVec);
    }

    void Jump()
    {
        if (_jDown && _moveVec == Vector3.zero && !_isJump && !_isDodge)
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _animator.SetBool("isJump", true);
            _animator.SetTrigger("doJump");
            _isJump = true;
        }
    }
    
    void Dodge()
    {
        if (_jDown && _moveVec != Vector3.zero && !_isJump && !_isDodge)
        {
            _dodgeVec = _moveVec;
            speed *= 2;
            _animator.SetTrigger("doDodge");
            _isDodge = true;
            
            Invoke(nameof(DodgeOut), 0.4f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        _isDodge = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _animator.SetBool("isJump", false);
            _isJump = false;
        }
    }
}
