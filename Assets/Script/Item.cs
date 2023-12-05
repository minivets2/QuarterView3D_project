using System;
using UnityEngine;


public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Ammo,
        Coin,
        Grenade,
        Heart,
        Weapon
    };

    public ItemType type;
    public int value;

    private void Update()
    {
        transform.Rotate(Vector3.up * 20 * Time.deltaTime);
    }
}
