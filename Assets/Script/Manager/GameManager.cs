using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager pool;
    public Player player;
    public Exp exp;
    public Hp hp;
    public EnemyCount enemyCount;
    public LevelUpPopup levelUpPopup;
    
    private int _level;

    public int Level => _level;
    
    private void Awake()
    {
        instance = this;
        _level = 0;
    }

    public void LevelUp()
    {
        _level++;
        double point = Math.Pow((_level * 50 / 49.0), 2.5) * 10;
        exp.SetExperience((int)point);

        if (_level > 1)
        {
            levelUpPopup.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
