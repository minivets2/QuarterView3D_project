using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public float speed;
    protected float health;
    protected Rigidbody rigid;
    protected Animator ani;
    
    public abstract void Move();
    public abstract void Turn();
    
    public Animator GetAnimation()
    {
        return ani;
    }
    
    public float GetHealth()
    {
        return health;
    }

    public void DecreaseHealth(float value)
    {
        health -= value;
    }
}
