using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    private float _timer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 2f)
        {
            Spawn();
            _timer = 0;
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
