using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [Header("status")] 
    public int level;
    
    [Header("-")]
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public Exp exp;
    
    private void Awake()
    {
        instance = this;
        level = 0;
    }

    public void LevelUp()
    {
        level++;
        double point = Math.Pow((level * 50 / 49.0), 2.5) * 10;
        exp.SetExperience((int)point);
    }
}
