using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BPCMUpgrade : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        BFManager.instance.AttemptBPCMUpgrade(transform.position);
    }

}

