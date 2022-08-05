using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Clicker : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        int num;
        num = UnityEngine.Random.Range(1, 5);
        BFManager.instance.Click();
        BFManager.instance.BananaParticle(transform.position);
        if(num == 3)
        {
           BFManager.instance.MonkeyAhSound(transform.position);

        }

    }

}

