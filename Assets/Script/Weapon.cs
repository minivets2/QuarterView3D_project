using System;
using UnityEngine;
using UnityEngine.Rendering;
using Vector3 = System.Numerics.Vector3;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0 :
                transform.Rotate(UnityEngine.Vector3.down * speed * Time.deltaTime);
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
                speed = 150;
                Batch();
                break;
            default:
                break;
        }
    }

    private void Batch()
    {
        for (int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;

            UnityEngine.Vector3 rotVec = UnityEngine.Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
        
        transform.Translate(UnityEngine.Vector3.up * 4.5f, Space.Self);
        
    }
}
