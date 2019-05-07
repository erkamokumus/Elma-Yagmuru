using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{


    public float hiz, maxHiz, distance , rekormesafe;
    public bool yerdemi, android , sol , sag;
    public int can, maxcan, ring, rekorring ;
    public Transform baslangicNoktasi;
    public Text ringMiktar, mesafeYazi,mesafeoyunsonuyazi , rekormesafeoyunsonuyazi , ringoyunsonuyazi , rekorringoyunsonuyazi;
    public GameObject[] canlar;
    public GameObject GameOverPanel;
    public AudioClip[] sesler;

    Rigidbody2D agirlik;
    Animator anim;

    void Start()
    {
        GameOverPanel.SetActive(false);
        anim = GetComponent<Animator>();
        agirlik = GetComponent<Rigidbody2D>();
        can = maxcan;
        canSistemi();

    }


    public void Update()
    {
        distance = Vector2.Distance(baslangicNoktasi.position, transform.position);
        mesafeYazi.text = (int)distance + "M";
        ringMiktar.text = "" + ring;

        if (can <= 0)
        {
            dead();
            Time.timeScale = 0;
        }

        if( can > 0)
        {
            Time.timeScale = 1;
        }

        if(can < 3 && can > 0 )
        {

            if( ring % 10 == 0)
            {
                can++;
                canSistemi();
            }
           
        }
    }



    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        h = Input.GetAxis("Horizontal");
        anim.SetFloat("Hiz", Mathf.Abs(h));
        anim.SetBool("Yerde", yerdemi);
        if (android)
        {
            if(sol)
            {
                h = -1;
                transform.localScale = new Vector2(-6, 6);
                transform.Translate(h * hiz * Time.deltaTime, 0, 0);
                anim.SetFloat("Hiz", Mathf.Abs(h));
            }


            if(sag)
            {
                h = 1;
                transform.localScale = new Vector2(6, 6);
                transform.Translate(h * hiz * Time.deltaTime, 0, 0);
                anim.SetFloat("Hiz", Mathf.Abs(h));
            }

            if( !sol  && !sag)
            {
                h = 0;
            }
        }
        
        else
        {
            h = Input.GetAxis("Horizontal");
            if (h > 0)
            {
                transform.Translate(h * hiz * Time.deltaTime, 0, 0);
            }

            if (h < 0)
            {
                transform.Translate(h * hiz * Time.deltaTime, 0, 0);
            }

            
            if (h > 0.1f)
            {
                transform.localScale = new Vector2(6, 6);
            }

            if (h < -0.1f)
            {
                transform.localScale = new Vector2(-6, 6);
            }
        }


        

        if (agirlik.velocity.x > maxHiz)
        {
            agirlik.velocity = new Vector2(maxHiz, agirlik.velocity.y);
        }

        if (agirlik.velocity.x < -maxHiz)
        {
            agirlik.velocity = new Vector2(-maxHiz, agirlik.velocity.y);
        }
    }




    public void dead()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
        
        mesafeoyunsonuyazi.text = "Mesafe =" + (int)distance + " M";
        ringoyunsonuyazi.text = "Yüzük = " + ring;
        rekormesafe = PlayerPrefs.GetFloat("RekorMesafemiz");
        rekorring = PlayerPrefs.GetInt("RekorRingimiz");


        if (rekormesafe > distance &&  rekorring > ring)
        {
           rekormesafeoyunsonuyazi.text = "Rekor =" + (int)rekormesafe + " M " + rekorring + " YÜZÜK";
        }

        else if( rekormesafe > distance  &&  rekorring < ring)
        {
            rekorring = ring;
            PlayerPrefs.SetInt("RekorRingimiz", rekorring);
            rekormesafeoyunsonuyazi.text = "Rekor =" + (int)rekormesafe + " M " + ring + " YÜZÜK";

        }

        else if( rekormesafe < distance  &&  rekorring > ring)
        {
            rekormesafe = distance;
            PlayerPrefs.SetFloat("RekorMesafemiz", rekormesafe);
            rekormesafeoyunsonuyazi.text = "Rekor =" + (int)rekormesafe + " M " + rekorring + " YÜZÜK";
        }

        else if( rekormesafe < distance  &&  rekorring < ring)
        {
            rekormesafe = distance;
            rekorring = ring;
            PlayerPrefs.SetFloat("RekorMesafemiz", rekormesafe);
            PlayerPrefs.SetInt("RekorRingimiz", rekorring);
            rekormesafeoyunsonuyazi.text = "Rekor =" + (int)rekormesafe + " M " + ring + " YÜZÜK";
        }

        
    }




    public void OnCollisionEnter2D(Collision2D nesne)
    {
        if (nesne.gameObject.tag == "Tuzak")
        {
            can = can - 1;
            GetComponent<AudioSource>().PlayOneShot(sesler[0]);
            GetComponent<SpriteRenderer>().color = Color.red;
            Invoke("Duzelt", 0.5f);
            canSistemi();

        }

        if ( nesne.gameObject.tag == "dusmeOlum")
        {
            can = 0;
            canSistemi();
            dead();
            Time.timeScale = 0;
        }
    }




    void canSistemi()
    {
        for (int i = 0; i < maxcan; i++)
        {
            canlar[i].SetActive(false);
        }

        for (int i = 0; i < can; i++)
        {
            canlar[i].SetActive(true);
        }
    }




    void Duzelt()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }




    public void OnTriggerEnter2D(Collider2D nesne)
    {
        if (nesne.gameObject.tag == "gecis")
        {
            nesne.gameObject.transform.root.gameObject.GetComponent<yol>().aktif = true;

            ring++;
            GetComponent<AudioSource>().PlayOneShot(sesler[1]);
            nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[0].SetActive(false);


            if (distance > 200)
            {
                for (int i = 5; i <= 8; i++)
                {
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
                }
                SurekliElma();
            }

            if (distance > 600)
            {
                for (int i = 9; i <= 10; i++)
                {
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);

                }
                SurekliElma();
            }


            if (distance > 800)
            {
                for (int i = 11; i <= 12; i++)
                {
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
                }
                SurekliElma();
            }

            if (distance > 1000)
            {
                for(int i = 13; i <= 14; i++)
                {
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
                }
                SurekliElma();
            }

            if (distance > 1200)
            {
                for(int i = 15; i <= 16; i++)
                {
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                    nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
                }
                SurekliElma();
            }

        }


        void SurekliElma()
        {
            for (int i = 1; i <= 4; i++)
            {
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(10, 18));
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
            }

            for (int i = 17; i <= 20; i++)
            {
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(18, 22));
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
            }

            for (int i = 21; i <= 24; i++)
            {
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(22, 26));
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
            }

            for (int i = 25; i <= 28; i++)
            {
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(26, 30));
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
            }

            for (int i = 29; i <= 32; i++)
            {
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].transform.localPosition = new Vector2(Random.Range(-15, 2), Random.Range(30, 32));
                nesne.gameObject.transform.root.gameObject.GetComponent<yol>().objeler[i].SetActive(true);
            }
        }
        
    }

}