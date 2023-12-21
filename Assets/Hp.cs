using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    private Slider hpSlider;
    
    public void Start()
    {
        hpSlider = GetComponent<Slider>();
        hpSlider.maxValue = GameManager.instance.player.health;
        hpSlider.value = GameManager.instance.player.health;
    }

    public void SetHp()
    {
        hpSlider.value = GameManager.instance.player.health;
    }
}
