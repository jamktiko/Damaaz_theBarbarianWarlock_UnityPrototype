using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player_CameraManager : MonoBehaviour
{
    CinemachineVirtualCamera _cam;
    Player_AttackEventManager _attackEventManager;

    [SerializeField] NoiseSettings damageEnemyNoiseSettings;
    [SerializeField] NoiseSettings defaultNoiseSettings;

    // Start is called before the first frame update
    void Start()
    {
        _attackEventManager = transform.parent.GetComponentInChildren<Player_AttackEventManager>();
        _attackEventManager.onDamagedEnemy += (float amount) => ChangeCameraShake(amount);
        _cam = GetComponent<CinemachineVirtualCamera>();
    }

    void ChangeCameraShake(float shakeAmount)
    {
        _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = damageEnemyNoiseSettings;

        StartCoroutine(Timer(0.5f));
    }

    IEnumerator Timer(float timeAmount)
    {
        float currentTime = 0f;

        while (timeAmount*5f >= currentTime)
        {
            currentTime += Time.deltaTime;
            
            yield return null;
        }

        ResetNoiseProfile();
    }

    void ResetNoiseProfile()
    {
        _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_NoiseProfile = defaultNoiseSettings;
    }
}
