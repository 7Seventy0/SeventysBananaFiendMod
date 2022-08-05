using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PrevBagButton : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        BagShop.instance.PrevBag();
    }

}

