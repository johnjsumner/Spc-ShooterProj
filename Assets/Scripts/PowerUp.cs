using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    private Player _player;
    [SerializeField] private int _powerUpID; // 0 = TripleShot 1 = Speed 2 = Shields 3 = Ammo
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioClip _ammoReload;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("The Player is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -7.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            if (_powerUpID == 0 || _powerUpID == 1 || _powerUpID == 2)
            {
                AudioSource.PlayClipAtPoint(_audioClip, new Vector3(0, 0, -10), 0.5f);
            }
            else
            {
                AudioSource.PlayClipAtPoint(_ammoReload, new Vector3(0, 0, -10), 1f);
            }

            switch (_powerUpID)
            {
                case 0:
                    _player.TripleShotActive();
                    break;
                case 1:
                    _player.SpeedActive();
                    break;
                case 2:
                    _player.ShieldActive();
                    break;
                case 3:
                    _player.AmmoReload();
                    break;
                default:
                    Debug.Log("Default Value");
                    break;
            }

            Destroy(gameObject);
        }
    }
}
