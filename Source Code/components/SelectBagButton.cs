using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SelectBagButton : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    float nextaction;
    float cooldown = 1.3f;
    void OnTriggerEnter(Collider collider)
    {
        if(Time.time > nextaction)
        {

            BagShop.instance.SelectBag();
            nextaction = Time.time+cooldown;
        }
      
    }

}

