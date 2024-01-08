using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour, IWeapon
{
    public void Shoot(float damage, int count)
    {
        StartCoroutine(On(damage, count));
    }
    
    IEnumerator On(float damage, int count)
    {
        transform.localScale = Vector3.zero;

        while (true)
        {
            transform.localScale += Vector3.one * 0.01f;
            yield return new WaitForSeconds(0.01f);
            Batch(damage, count);
            
            if (transform.localScale.x >= 1f)
            {
                transform.localScale = Vector3.one;
                Batch(damage, count);
                break;
            }
        }
        
        yield return new WaitForSeconds(5f);
        
        while (true)
        {
            transform.localScale -= Vector3.one * 0.01f;
            yield return new WaitForSeconds(0.01f);
            Batch(damage, count);
            
            if (transform.localScale.x <= 0f)
            {
                break;
            }
        }
   
        transform.localScale = Vector3.zero;
    }
    
    private void Batch(float damage, int count)
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
                bullet = GameManager.instance.pool.Get(2).transform;
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
}
