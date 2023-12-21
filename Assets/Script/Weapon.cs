using System;
using OpenCover.Framework.Model;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    
    public float damage;
    public int count;
    public float speed;
    public int level;

    public ParticleSystem effect;

    private float timer;
    private Player player;
    
    private void Start()
    {
        Init();
        effect.Stop();
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
                Batch();
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
            
            UnityEngine.Vector3 rotVec = UnityEngine.Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 6f, Space.World);

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
                Batch();
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
}
