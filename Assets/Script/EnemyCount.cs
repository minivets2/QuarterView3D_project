using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    private int _enemyCount;
    private TMP_Text _enemyCountText;

    private void Start()
    {
        _enemyCount = -1;
        _enemyCountText = GetComponent<TMP_Text>();
        IncreaseEnemyCount();
    }

    public void IncreaseEnemyCount()
    {
        _enemyCount++;
        _enemyCountText.text = _enemyCount.ToString();
    }
}
