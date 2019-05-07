using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform karakter;
    public float x, y;

    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("character").transform;
    }

    
    void Update()
    {
        transform.position = new Vector3(karakter.position.x + x , karakter.position.y + y , -10 );
    }
}
