using TMPro;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{
    public static DamagePopupManager instance;
    public GameObject prefab;

    private void Awake()
    {
        instance = this;
    }

    public void CreatePopup(Vector3 position, string text)
    {
        var popup = Instantiate(prefab, position, Quaternion.identity);
        var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        popup.transform.SetParent(transform);
        temp.text = text;
        
        Destroy(popup, 1f);
    }
}
