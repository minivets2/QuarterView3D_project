using UnityEngine;
using System.Collections;

public enum WeaponType
{
    Sword,
    Shield,
    AreaAttack
}

public class Weapon : MonoBehaviour
{
    public WeaponType weaponType;
    public float damage;
    public int count;
    public float speed;
    public int level;

    public ParticleSystem effect;
    public bool isOn;
    
    private float timer;
    private Player player;
    
    private IWeapon _weapon;
    private GameObject _myWeapon;
    
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        switch (weaponType)
        {
            case WeaponType.Shield :
                level = 1;
                count = 1;
                speed = 120;
                _weapon = gameObject.AddComponent<Shield>();
                effect.Play();
                break;
            
            case WeaponType.Sword :
                level = 1;
                count = 1;
                speed = 1.2f;
                _weapon = gameObject.AddComponent<Sword>();
                break;
            
            case WeaponType.AreaAttack :
                level = 1;
                count = 2;
                _weapon = gameObject.AddComponent<AreaAttack>();
                break;
            
            default:
                break;
        }
        
        Fire();
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        timer = 0f;
    }

    private void Update()
    {
        switch (weaponType)
        {
            case WeaponType.Shield :
                
                transform.Rotate(UnityEngine.Vector3.back * speed * Time.deltaTime);
                
                timer += Time.deltaTime;
                
                if (timer > 15)
                {
                    timer = 0f;
                    
                    effect.Play();
                    Fire();
                }
                
                break;
            
            case WeaponType.Sword :
                
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                
                break;
            
            case WeaponType.AreaAttack :

                timer += Time.deltaTime;

                if (timer > 10)
                {
                    timer = 0f;
                    Fire();
                }

                break;
            
            default:
                
                break;
        }
    }
    
    public void LevelUp(int id)
    {
        switch (weaponType)
        {
            case WeaponType.Shield :
                damage++;
                count++;
                speed++;
                level++;
                break;
            
            case WeaponType.Sword :
                damage++;
                count++;
                level++;
                speed -= 0.1f;
                break;
            
            case WeaponType.AreaAttack :
                damage++;
                count++;
                level++;
                break;
            
            default:
                break;
        }
    }
    
    private void Fire()
    {
        _weapon.Shoot(damage, count);
    }
}
