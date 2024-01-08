using System.Collections;
using UnityEngine;

public class AreaAttack : MonoBehaviour, IWeapon
{
    public void Shoot(float damage, int count)
    { 
        StartCoroutine(On(damage, count));
    }
    
    IEnumerator On(float damage, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform area;

            area = GameManager.instance.pool.Get(6).transform;
            area.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);

            area.position = GameManager.instance.player.transform.position + new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
            
            yield return new WaitForSeconds(0.5f);
        }
    }
}
