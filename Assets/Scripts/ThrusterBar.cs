using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrusterBar : MonoBehaviour
{
    private Slider _thrustBar;
    private float _thrustBarValue;
    [SerializeField] private bool _isThrustActive = false;
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _thrustBar = GetComponent<Slider>();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isThrustActive == true)
        {
            _thrustBar.value += 0.01f;
            _thrustBarValue = _thrustBar.value;

            if(_thrustBarValue >= 4f)
            {
                _isThrustActive = false;
                _player.NoThruster(_thrustBarValue);
            }
        }
       else
        {
            _thrustBar.value -= 0.001f;
            _thrustBarValue = _thrustBar.value;
        }
    }

    public void ThrustActive(bool status)
    {
        if(status == true)
        {
            _isThrustActive = true;
        }
        else
        {
            _isThrustActive = false;
        }
        
    }

}
