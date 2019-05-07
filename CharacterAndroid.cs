using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAndroid : MonoBehaviour
{
    Character kr;
    void Start()
    {
        kr = GetComponent<Character>();
    }

    
    void Update()
    {
        
    }


    public void sol()
    {
        kr.sol = true;
    }


    public void sag()
    {
        kr.sag = true;
    }

    public void SolUp()
    {
        kr.sol = false;
    }

    public void SagUp()
    {
        kr.sag = false;
    }
}
