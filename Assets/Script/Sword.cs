using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public void Shoot(float damage, int count)
    {
        if (GameManager.instance.player.Scanner.nearestTarget == null) return;

        Vector3 targetPos = GameManager.instance.player.Scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(4).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}
