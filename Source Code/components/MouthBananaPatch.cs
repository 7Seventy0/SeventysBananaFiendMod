using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MouthBananaPatch : MonoBehaviour
{
    public GameObject nana;

    void Start()
    {
        int randomInt = UnityEngine.Random.Range(0, 4);

        if (BFManager.instance.hasBananas && randomInt == 1)
        {
            nana = GameObject.Find("ValueBanana(Clone)");

            GameObject nanaInstance = Instantiate(nana);
            nanaInstance.name = "MouthNana";
            nanaInstance.transform.SetParent(gameObject.GetComponent<VRRig>().head.rigTarget, false);
            nanaInstance.transform.localPosition = new Vector3(0, 0, 0.17f);
            nanaInstance.transform.localEulerAngles = new Vector3(278.1389f, 93.8404f, 179.9984f);
            nanaInstance.AddComponent<MouthNana>();
            if (BFManager.instance.inGame)
            {
                nanaInstance.AddComponent<Holdable>();
            }
        }

    }

}

