using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MouthNanaUpgrade : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        BFManager.instance.AttemptMouthNanasUpgrade(transform.position);
    }

}

