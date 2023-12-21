using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    
    public float damage;
    public int count;
    public float speed;
    public int level;

    public ParticleSystem effect;
    public bool isOn;
    
    private float timer;
    private Player player;
    
    private void Start()
    {
        Init();
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        timer = 0f;
    }

    private void Update()
    {
        switch (id)
        {
            case 0 :
                
                transform.Rotate(UnityEngine.Vector3.back * speed * Time.deltaTime);
                
                timer += Time.deltaTime;
                
                if (timer > 15)
                {
                    timer = 0f;
                    StartCoroutine(nameof(On));
                }
                
                break;
            
            case 1 :
                
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                
                break;
            
            default:
                
                break;
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0 :
                level = 1;
                count = 1;
                speed = 120;
                StartCoroutine(nameof(On));
                break;
            
            case 1 :
                level = 1;
                count = 1;
                speed = 3f;
                break;
            default:
                break;
        }
    }

    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = UnityEngine.Vector3.zero;
            bullet.localRotation = Quaternion.identity;
            bullet.localScale = Vector3.one * 6f;
            
            UnityEngine.Vector3 rotVec = UnityEngine.Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 7f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    public void LevelUp(int id)
    {
        switch (id)
        {
            case 0 :
                damage++;
                count++;
                speed++;
                level++;
                break;
            case 1 :
                damage++;
                count++;
                level++;
                speed -= 0.5f;
                break;
            default:
                break;
        }
    }

    private void Fire()
    {
        if (player.scanner.nearestTarget == null) return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }

    IEnumerator On()
    {
        effect.Play();
        transform.localScale = Vector3.zero;

        while (true)
        {
            transform.localScale += Vector3.one * 0.01f;
            yield return new WaitForSeconds(0.01f);
            Batch();
            
            if (transform.localScale.x >= 1f)
            {
                transform.localScale = Vector3.one;
                Batch();
                break;
            }
        }
        
        yield return new WaitForSeconds(5f);
        
        while (true)
        {
            transform.localScale -= Vector3.one * 0.01f;
            yield return new WaitForSeconds(0.01f);
            Batch();
            
            if (transform.localScale.x <= 0f)
            {
                break;
            }
        }
   
        transform.localScale = Vector3.zero;
    }
}
