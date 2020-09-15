using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables
    [SerializeField] private float _speed = 5f;
    private float _doubleSpeed = 2.0f;

    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _fireLeftEngine;
    [SerializeField] private GameObject _fireRightEngine;
    [SerializeField] private GameObject _shieldVisuals;
    [SerializeField] private AudioClip _laserSound;
    
    private AudioSource _playerAudio;

    private float _offset = 1f;

    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;

    [SerializeField] private int _lives = 3;
    [SerializeField] private int _score;
    private Animator _playerExplosion;

    private UI_Manager _uiManager;
    private SpawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    private bool _isSpeedActive = false;
    private bool _isShieldActive = false;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL");
        }

        _playerAudio = GetComponent<AudioSource>();

        if(_playerAudio == null)
        {
            Debug.LogError("The Player Audio is NULL");
        }
        else
        {
            _playerAudio.clip = _laserSound;
        }

        _playerExplosion = GetComponent<Animator>();

        if(_playerExplosion == null)
        {
            Debug.LogError("Animator is NULL");
        }

        _fireLeftEngine.gameObject.SetActive(false);
        _fireRightEngine.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + (Vector3.up * _offset), Quaternion.identity);
        }

        _playerAudio.Play();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if(_isSpeedActive == false)
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * _doubleSpeed * Time.deltaTime);
        }
        
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 10.43f)
        {
            transform.position = new Vector3(-11.33f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.33f)
        {
            transform.position = new Vector3(10.43f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisuals.SetActive(false);
            return;
        }
        
        _lives--;

        _uiManager.UpdateLives( _lives);

        if(_lives == 2)
        {
            _fireLeftEngine.gameObject.SetActive(true);
        }

        else if(_lives == 1)
        {
            _fireRightEngine.gameObject.SetActive(true);
        }

        else
        {
            _playerExplosion.SetTrigger("OnPlayerDeath");
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject, 2.8f);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }

    IEnumerator TripleShotPowerDown()
    {
        while(_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isTripleShotActive = false;
        }
    }

    public void SpeedActive()
    {
        _isSpeedActive = true;
        StartCoroutine(SpeedPowerDown());
    }

    IEnumerator SpeedPowerDown()
    {
        while(_isSpeedActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _isSpeedActive = false;
        }
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisuals.SetActive(true);
    }

    public void AddToScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
