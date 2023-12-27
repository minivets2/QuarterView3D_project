using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Weapon[] weapons;
    public Scanner scanner;
    public GameObject effect;
    public float health;
    public Vector3 _moveVec;
    public Vector3 _previousMoveVec;

    private PlayerDamage _playerDamage;
    private Vector3 _dodgeVec;
    public Animator _animator;
    
    private float _hAxis;
    private float _vAxis;
    
    private bool _wDown;
    private bool _jDown;
    private bool _isJump;
    private bool _isDodge;
    private bool _isSwap;

    
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerDamage = GetComponentInChildren<PlayerDamage>();
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

    public void OnDamageAnimationComplete()
    {
        _playerDamage.OnDamageAnimationComplete();
    }
}
