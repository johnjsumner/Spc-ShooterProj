using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    public List<Transform> Enemies;
    public Transform SelectedTarget;

    // Start is called before the first frame update
    void Start()
    {
        SelectedTarget = null;
        Enemies = new List<Transform>();
        AddEnemiesToList();
    }

    // Update is called once per frame
    void Update()
    {
        TargetedEnemy();
        float dist = Vector3.Distance(SelectedTarget.transform.position, transform.position);

        transform.position = Vector3.MoveTowards(transform.position, SelectedTarget.position, _speed * Time.deltaTime);
      
    }

    public void AddEnemiesToList()
    {
        GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject _Enemy in enemyList)
        {
            AddTarget(_Enemy.transform);
        }
    }

    public void AddTarget(Transform enemy)
    {
            Enemies.Add(enemy);
    }

    public void DistanceToTarget()
    {
        Enemies.Sort(delegate (Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.transform.position, transform.position).CompareTo(Vector3.Distance(t2.transform.position, transform.position));
        });

    }

    public void TargetedEnemy()
    {
        if (SelectedTarget == null)
        {
            DistanceToTarget();
            SelectedTarget = Enemies[0];
        }


    }
}



