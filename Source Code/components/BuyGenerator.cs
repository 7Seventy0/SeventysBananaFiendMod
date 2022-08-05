using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BuyGenerator : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        if (BFManager.instance.hasBananas)
        {
        BFManager.instance.AttemptGeneratorPurchase(transform.position);

        }
    }

}

