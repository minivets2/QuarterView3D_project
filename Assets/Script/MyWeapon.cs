using UnityEngine;

public class MyWeapon
{
    //접근점
    private IWeapon _weapon;

    public void SetWeapon(IWeapon weapon)
    {
        _weapon = weapon;
    }

    public void Shoot(float damage, int count)
    {
        _weapon.Shoot(damage, count);
    }
}
