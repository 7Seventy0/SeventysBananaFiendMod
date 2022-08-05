using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
public class BananaGenerator : MonoBehaviour
{

    TextMeshPro storageText;
    int storedBananas;
    void Start()
    { 
        storageText = GameObject.Find("BFM(Clone)/BananaGenerator/Storage Text").GetComponent<TextMeshPro>();
        storedBananas = BFManager.instance.generatorStorage;
        gameObject.layer = 18;
    }
    float nextgenerate;
    
    void Update()
    {
        if(storageText == null)
        {
            storageText = GameObject.Find("BFM(Clone)/BananaGenerator/Storage Text").GetComponent<TextMeshPro>();
        }
        else
        {
            storageText.text = storedBananas + "/"+BFManager.instance.generatorMaxStorage+"\nBananas";
        }
        if(Time.time > nextgenerate)
        {
            GenerateBanana();
            nextgenerate = Time.time+BFManager.instance.generatorTime;
        }
    }

    public void GenerateBanana()
    {
        if (gameObject.activeSelf)
        {
            if(BFManager.instance.generatorStorage + 1<= BFManager.instance.generatorMaxStorage)
            {
                storedBananas++;
                BFManager.instance.generatorStorage = storedBananas;
            }

        }
    }
    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.GetComponent<BagClass>() != null)
        {
            BagClass bagClass = collider.gameObject.GetComponent<BagClass>();
            if (bagClass.stats.bananaStorage < bagClass.stats.bagNanaLimit && storedBananas > 0)
            {
                bagClass.AddBanana();
                storedBananas--;
            }
        }
    }
}

