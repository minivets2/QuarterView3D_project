using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    private float _gameTime;
    private int _minute;
    private int _second;
    private TMP_Text _timeText;

    private void Start()
    {
        _timeText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        _gameTime += Time.deltaTime;
        _second = Mathf.FloorToInt(_gameTime);

        if (_second == 60)
        {
            _gameTime = 0;
            _second = 0;
            _minute++;
        }

        if (_second < 10)
        {
            _timeText.text = _minute + " : 0" + _second;
        }
        else
        {
            _timeText.text = _minute + " : " + _second;   
        }
    }
}
