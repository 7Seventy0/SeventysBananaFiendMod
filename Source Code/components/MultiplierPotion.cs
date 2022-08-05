using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MultiplierPotion:MonoBehaviour
{
    void Start()
    {
        gameObject.layer = 11;
    }
    void OnTriggerEnter(Collider collider)
    {
        BFManager.instance.multiplier += 0.1f;
        GetComponentInParent<Holdable>().KillMe();
    }
}

