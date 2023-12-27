using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public AnimationCurve opacityCurve;
    public AnimationCurve scaleCurve;
    public AnimationCurve heightCurve;
    
    private TextMeshProUGUI _tmp;
    private float _time = 0;
    private Vector3 _origin;

    private void OnEnable()
    {
        _tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _origin = transform.position;
    }

    private void Update()
    {
        _tmp.color = new Color(1, 1, 1, opacityCurve.Evaluate(_time));
        transform.localScale = Vector3.one * scaleCurve.Evaluate(_time);
        transform.position = _origin + new Vector3(0, 1 + heightCurve.Evaluate(_time), 0);
        _time += Time.deltaTime;
    }
}
