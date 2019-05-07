using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alma : MonoBehaviour
{
    public GameObject alma0;
    public float maxSpeed = 25f;

    

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
        }
    }

    

    public void OnCollisionEnter2D(Collision2D nesne)
    {
       
        if (nesne.gameObject.tag == "ground")
        {
            alma0.SetActive(false);

        }

        if (nesne.gameObject.tag == "character")
        {
            alma0.SetActive(false);
        }

        if( nesne.gameObject.tag == "dusmeOlum")
        {
            alma0.SetActive(false);
        }

        
    }

    void back()
    {
        alma0.SetActive(true);

    }

    

  



    

}
