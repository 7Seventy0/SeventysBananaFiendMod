using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


    public class DestroyAfterTime : MonoBehaviour
    {

    float destroyin = 10;

    void Update()
    {
        destroyin -= Time.deltaTime;    
        if(destroyin >= 0)
        {
            Destroy(gameObject);
        }
    }



    }

