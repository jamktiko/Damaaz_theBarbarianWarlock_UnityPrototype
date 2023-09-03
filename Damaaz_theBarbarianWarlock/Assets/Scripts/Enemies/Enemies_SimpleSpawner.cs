using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_SimpleSpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnRate;

    float currentTime;

    private void FixedUpdate()
    {
        if (currentTime < spawnRate)
        {
            currentTime += Time.fixedDeltaTime;
        }
        else
        {
            Instantiate(enemy, this.transform.position, enemy.transform.rotation);
            currentTime = 0;
        }
    }
}
