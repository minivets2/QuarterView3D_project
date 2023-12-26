using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{
   public TMP_Text levelText;
   
   private Slider _expSlider;

   public void Start()
   {
      _expSlider = GetComponent<Slider>();
      _expSlider.value = 0;
      levelText.text = "LV 1";
      GameManager.instance.LevelUp();
   }

   public void ExperienceLevelUp()
   {
      _expSlider.value += 5;

      if (_expSlider.value >= _expSlider.maxValue)
      {
         GameManager.instance.LevelUp();
         levelText.text = "LV " + GameManager.instance.Level;
      }
   }

   public void SetExperience(int value)
   {
      _expSlider.maxValue = value;
      _expSlider.value = 0;
   }
}
