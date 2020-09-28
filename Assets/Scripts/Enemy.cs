using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables
    [SerializeField] private float _speed = 4.0f;
    private float _minX = -9.45f;
    private float _maxX = 9.45f;

    private float _minY = -2f;
    private float _maxY = 5f;

    private Player _player;
    private Animator _explosion;

    private AudioSource _audioSource;

    [SerializeField] private GameObject _enemyLaser;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    private bool _enemyDown = false;

    private bool _dirRight = true;



    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
            {
            Debug.LogError("Player is NULL");
            }

        _explosion = GetComponent<Animator>();

        if(_explosion == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _audioSource = GetComponent<AudioSource>();

        if(_audioSource == null)
        {
            Debug.LogError("AudioSource is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].EnemyLaser();
            }
        }
    }

    private void CalculateMovement()
    {
        /*transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7.0f)
        {
               transform.position = new Vector3(Random.Range(_minX, _maxX), 8, 0);
        }*/
        //transform.Translate(Vector3.right * _speed * Time.deltaTime);

        if(transform.position.x >= 9.5f)
        {
            _dirRight = false;
        }

        else if(transform.position.x <= -9.5f)
        {
            _dirRight = true;
        }
        
        if (_dirRight)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);

            /*if (transform.position.x >= 12f)
            {
                transform.position = new Vector3(-12, Random.Range(_minY, _maxY), 0);
            }*/
        }
        else
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(_player != null)
            {
                _player.Damage();
            }

            _explosion.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2f);
        }

        if(other.gameObject.tag == "Laser")
        {
            Destroy(other.gameObject);
            _player.AddToScore(10);

            _explosion.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2f);
        }
        if(other.gameObject.tag == "Missile")
        {
             Destroy(other.gameObject);
            _player.AddToScore(10);
            
            _explosion.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            gameObject.tag = ("Untagged");
            Destroy(GetComponent<Collider2D>());
            Destroy(gameObject, 2f);
        }
     }
}
