using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WorkerUpgrade : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 18;
    }
    void OnTriggerEnter(Collider collider)
    {
        BFManager.instance.AttemptWorkerUpgrade(transform.position);
    }

}

