using UnityEngine;

public class Player : Unit
{
    public Weapon[] weapons;
    public GameObject effect;
    
    private Vector3 _moveVec;
    private Vector3 _previousMoveVec;

    private PlayerDamage _playerDamage;
    private Scanner _scanner;
    
    private float _hAxis;
    private float _vAxis;
    
    private bool _wDown;
    private bool _jDown;
    private bool _isSwap;

    public Vector3 MoveVec => _moveVec;
    public Scanner Scanner => _scanner;
    
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        _playerDamage = GetComponentInChildren<PlayerDamage>();
        _scanner = GetComponent<Scanner>();
        _previousMoveVec = new Vector3(100, 100, 100);
        health = 500;
    }

    private void Update()
    {
        GetInput();
        Move();
    }

    void GetInput()
    {
        _hAxis = Input.GetAxisRaw("Horizontal");
        _vAxis = Input.GetAxisRaw("Vertical");
        _wDown = Input.GetButton("Walk");
    }

    public override void Move()
    {
        _moveVec = new Vector3(_hAxis, 0, _vAxis).normalized;

        if (_previousMoveVec != _moveVec)
        {
            Turn();
        }

        if (_isSwap)
            _moveVec = Vector3.zero;
        
        transform.position += _moveVec * speed * (_wDown ? 0.3f : 1f) * Time.deltaTime;

        ani.SetBool("isRun", _moveVec != Vector3.zero);
        ani.SetBool("isWalk", _wDown);
    }

    public override void Turn()
    {
        weapons[0].transform.SetParent(null);
        effect.transform.SetParent(null);
        transform.LookAt(transform.position + _moveVec);
        weapons[0].transform.SetParent(gameObject.transform);
        effect.transform.SetParent(gameObject.transform);
        _previousMoveVec = _moveVec;
    }

    public void OnDamageAnimationComplete()
    {
        _playerDamage.OnDamageAnimationComplete();
    }
}
