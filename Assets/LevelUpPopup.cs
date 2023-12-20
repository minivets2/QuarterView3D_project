using UnityEngine;

public class LevelUpPopup : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnclickItemButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
