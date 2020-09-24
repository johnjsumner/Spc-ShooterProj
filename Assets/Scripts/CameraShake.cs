using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cineCamera;
    private float _shakeTimer;

    
    // Start is called before the first frame update
    void Start()
    {
        _cineCamera = GetComponent<CinemachineVirtualCamera>();

        if(_cineCamera == null)
        {
            Debug.LogError("Cinemachine Camera is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_shakeTimer > 0f)
        {
            _shakeTimer -= Time.deltaTime;
        
            if(_shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
                cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
            }
        }

        
    }

    public void Shake(float intensity, float frequency, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = _cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        _shakeTimer = time;
    }
}


