using System;
using System.Collections.Generic;
using UnityEngine;

public class AreaAttack : MonoBehaviour
{
    private bool _isOn;
    private float _timer;
    public List<Enemy> _nearEnemy;

    private void OnEnable()
    {
        _isOn = true;
    }

    private void Update()
    {
        if (_isOn)
        {
            _timer += Time.deltaTime;

            if (_timer > 5)
            {
                _timer = 0f;
                _isOn = false;

                for (int i = 0; i < _nearEnemy.Count; i++)
                {
                    _nearEnemy[i].isAreaAttack = false;
                }
                
                _nearEnemy.Clear();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (!_nearEnemy.Contains(other.GetComponent<Enemy>()))
        {
            _nearEnemy.Add(other.GetComponent<Enemy>());
        }
    }
}
