using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Weapon[] weapons;
    public Scanner scanner;
    public GameObject effect;
    
    private float _hAxis;
    private float _vAxis;
    public Vector3 _moveVec;
    public Vector3 _previousMoveVec;
    private Vector3 _dodgeVec;
    private Animator _animator;
    private Rigidbody _rigidbody;

    private bool _wDown;
    private bool _jDown;

    private bool _isJump;
    private bool _isDodge;
    private bool _isSwap;

    public float health;
    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        scanner = GetComponent<Scanner>();
        _previousMoveVec = new Vector3(100, 100, 100);
        health = 500;
    }

    private void Update()
    {
        GetInput();
        Move();
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
        weapons[0].transform.SetParent(null);
        effect.transform.SetParent(null);
        transform.LookAt(transform.position + _moveVec);
        weapons[0].transform.SetParent(gameObject.transform);
        effect.transform.SetParent(gameObject.transform);
        _previousMoveVec = _moveVec;
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
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        health -= other.GetComponent<Enemy>().damage;
        GameManager.instance.hp.SetHp();

        if (health > 0)
        {
            
        }
        else
        {
            Dead();
        }
    }

    private void Dead()
    {
        
    }
}
