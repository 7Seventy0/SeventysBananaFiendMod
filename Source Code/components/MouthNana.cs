using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

public class MouthNana : MonoBehaviour
{

    void Start()
    {
        gameObject.layer = 11;
    }
    public void KillMe()
    {
        Destroy(gameObject);
    }
    void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<Brunnen>() != null)
        {
            BFManager.instance.BrunnenSell();
            Destroy(gameObject);
            return;
        }
        if (other.GetComponent<BagClass>() != null && BFManager.instance.inGame)
        {
            if (!other.GetComponent<BagClass>().isFull)
            {

            other.GetComponent<BagClass>().AddBanana();
            Destroy(gameObject);
            return;
            }
        }
        
    }


}

