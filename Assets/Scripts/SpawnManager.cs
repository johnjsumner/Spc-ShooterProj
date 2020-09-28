﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] _powerUps;
    private float _minX = -9f;
    private float _maxX = 9f;
    private float _minY = -1.5f;
    private float _maxY = 5f;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false;

    private Enemy _enemyMovement;

    private void Start()
    {
        _enemyMovement = GameObject.Find("Enemy").GetComponent<Enemy>();

        if(_enemyMovement == null)
        {
            Debug.LogError("Enemy is NULL");
        }    
    }
    public void StartSpawning()
    {
        //StartCoroutine(SpawnEnemyRoutineDown());
        StartCoroutine(SpawnEnemyRoutineAcross());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnMissilePowerUp());
    }

    IEnumerator SpawnEnemyRoutineDown()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 spawnEnemyPosX = new Vector3(Random.Range(_minX, _maxX), 8, 0);
            GameObject newEnemyX = Instantiate(_enemy, spawnEnemyPosX, Quaternion.identity);
            newEnemyX.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnEnemyRoutineAcross()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 spawnEmenyPosY = new Vector3(-12, Random.Range(_minY, _maxY), 0);
            GameObject newEnemyY = Instantiate(_enemy, spawnEmenyPosY, Quaternion.identity);
            newEnemyY.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 spawnPowerUpPos = new Vector3(Random.Range(_minX, _maxX), 8, 0);
            int randomPowerup = Random.Range(0, 5);
            GameObject newPowerUp = Instantiate(_powerUps[randomPowerup], spawnPowerUpPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 11));
        }
    }

    IEnumerator SpawnMissilePowerUp()
    {
        yield return new WaitForSeconds(30f);

        while (_stopSpawning == false)
        {
            Vector3 spawnPowerUpPos = new Vector3(Random.Range(_minX, _maxX), 8, 0);
            int missilePowerUp = 5;
            GameObject misPowerUp = Instantiate(_powerUps[missilePowerUp], spawnPowerUpPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(50, 70));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
