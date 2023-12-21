using UnityEngine;

public class LevelUpPopup : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnclickItemButton(int id)
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.instance.player.weapons[id].LevelUp(id);
    }
}
