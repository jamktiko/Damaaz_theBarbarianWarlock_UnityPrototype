using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment_SimpleParalax : MonoBehaviour
{
    //Copied from By Dani
    //https://www.youtube.com/watch?v=zit45k6CUMk

    float pos;

    [SerializeField] GameObject cam;
    [SerializeField] float effectStrength;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = (cam.transform.position.x * effectStrength);

        Vector3 newPos = new Vector3(pos + dist, transform.position.y, transform.position.z);

        transform.position = newPos;
    }
}
