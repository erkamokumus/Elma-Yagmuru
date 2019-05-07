using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yol : MonoBehaviour
{
    public bool aktif;
    public GameObject[] objeler;
   

    void Start()
    {
        
    }


    public void Update()
    {
        if (aktif)
        {
            YoluTasi();
            aktif = false;
            
        }
        
    }


    public void YoluTasi()
    { 

        objeler[0].SetActive(true);
        transform.position = transform.position + new Vector3(41.9f, 0, 0);

         
            for (int i = 1; i <= 4; i++)
            {
                objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                objeler[i].SetActive(true);
            }

        for (int i = 17; i <= 20; i++)
        {
            objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(18, 22));
            objeler[i].SetActive(true);
        }

        for (int i = 21; i <= 24; i++)
        {
            objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(22, 26));
            objeler[i].SetActive(true);
        }

        for ( int i = 25; i <= 28; i++)
        {
            objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(26, 30));
            objeler[i].SetActive(true);
        }

        for ( int i = 29; i <= 32; i++)
        {
            objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(30, 32));
            objeler[i].SetActive(true);
        }

    }
}