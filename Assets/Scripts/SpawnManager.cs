using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject[] _powerUps;
    private float _minX = -9.26f;
    private float _maxX = 9.26f;
    [SerializeField] private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while(_stopSpawning == false)
        {
            Vector3 spawnEnemyPos = new Vector3(Random.Range(_minX, _maxX), 8, 0);
            GameObject newEnemy = Instantiate(_enemy, spawnEnemyPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
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

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
