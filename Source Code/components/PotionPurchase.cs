using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PotionPurchase : MonoBehaviour
{
    public float ID;
    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        BFManager.instance.AttemptPotionPurchase(ID,transform.position);
    }

}

