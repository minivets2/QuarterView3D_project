using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject[] weapons;
    public bool[] hasWeapons;
    public float jumpPower;
    public GameObject weapon;

    private float _hAxis;
    private float _vAxis;
    public Vector3 _moveVec;
    public Vector3 _previousMoveVec;
    private Vector3 _dodgeVec;
    private Animator _animator;
    private Rigidbody _rigidbody;
    
    private bool _wDown;
    private bool _jDown;
    private bool _iDown;
    public bool _sDown1;
    public bool _sDown2;
    public bool _sDown3;
    
    private bool _isJump;
    private bool _isDodge;
    private bool _isSwap;

    private GameObject _nearObject;
    private GameObject _equipWeapon;

    private int _equipWeaponIndex = -1;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _previousMoveVec = new Vector3(100, 100, 100);
    }

    private void Update()
    {
        GetInput();
        Move();
        Jump();
        Dodge();
        Swap();
        Interation();
    }

    void GetInput()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");
        _wDown = Input.GetButton("Walk");
        _jDown = Input.GetButtonDown("Jump");
        _iDown = Input.GetButtonDown("Interation");
        _sDown1 = Input.GetButtonDown("Swap1");
        _sDown2 = Input.GetButtonDown("Swap2");
        _sDown3 = Input.GetButtonDown("Swap3");
    }

    void Move()
    {
        _moveVec = new Vector3(_hAxis, 0, _vAxis).normalized;

        if (_previousMoveVec != _moveVec)
        {
            Turn();
        }

        if (_isDodge)
            _moveVec = _dodgeVec;

        if (_isSwap)
            _moveVec = Vector3.zero;
        
        transform.position += _moveVec * speed * (_wDown ? 0.3f : 1f) * Time.deltaTime;

        _animator.SetBool("isRun", _moveVec != Vector3.zero);
        _animator.SetBool("isWalk", _wDown);
    }

    void Turn()
    {
        weapon.transform.SetParent(null);
        transform.LookAt(transform.position + _moveVec);
        weapon.transform.SetParent(gameObject.transform);
        _previousMoveVec = _moveVec;
        weapon.transform.localScale = Vector3.one;
    }

    void Jump()
    {
        if (_jDown && _moveVec == Vector3.zero && !_isJump && !_isDodge && !_isSwap)
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            _animator.SetBool("isJump", true);
            _animator.SetTrigger("doJump");
            _isJump = true;
        }
    }
    
    void Dodge()
    {
        if (_jDown && _moveVec != Vector3.zero && !_isJump && !_isDodge && !_isSwap)
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

    void Swap()
    {
        if (_sDown1 && (!hasWeapons[0] || _equipWeaponIndex == 0)) return;
        if (_sDown2 && (!hasWeapons[1] || _equipWeaponIndex == 1)) return;
        if (_sDown3 && (!hasWeapons[2] || _equipWeaponIndex == 2)) return;

        int weaponIndex = -1;

        if (_sDown1) weaponIndex = 0;
        if (_sDown2) weaponIndex = 1;
        if (_sDown3) weaponIndex = 2;
        
        if ((_sDown1 || _sDown2 || _sDown3) && !_isJump && !_isDodge)
        {
            if (_equipWeapon != null)
               _equipWeapon.SetActive(false);

            _equipWeaponIndex = weaponIndex;
            _equipWeapon = weapons[weaponIndex];
            _equipWeapon.SetActive(true);
            
            _animator.SetTrigger("doSwap");

            _isSwap = true;
            
            Invoke(nameof(SwapOut), 0.5f);
        }
    }
    
    void SwapOut()
    {
        _isSwap = false;
    }

    void Interation()
    {
        if (_iDown && _nearObject != null && !_isJump && !_isDodge)
        {
            if (_nearObject.tag == "Weapon")
            {
                Item item = _nearObject.GetComponent<Item>();
                //int weaponIndex = item.value;

                //hasWeapons[weaponIndex] = true;
                
                Destroy(_nearObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            _animator.SetBool("isJump", false);
            _isJump = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _nearObject = null;
        }
    }
}
