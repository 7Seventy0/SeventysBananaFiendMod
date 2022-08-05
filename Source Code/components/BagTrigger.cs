using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

public class BagTrigger : MonoBehaviour
{
    private readonly XRNode rNode = XRNode.RightHand;
    private readonly XRNode lNode = XRNode.LeftHand;
    bool gripr;
    bool gripl;
    void Start()
    {
        gameObject.layer = 18;
    }
    void Update()
    {
        InputDevices.GetDeviceAtXRNode(rNode).TryGetFeatureValue(CommonUsages.gripButton, out gripr);
        InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(CommonUsages.gripButton, out gripl);
    }
    float nextGrab;
    float nextGrabcooldown = 1f;
    void OnTriggerStay(Collider collider)
    {
        
        if (collider.name == "RightHandTriggerCollider")
        {
            Debug.Log("_R");
            if (Time.time > nextGrab)
            {
                Debug.Log(" R");
                if (gripr)
                {
                    Debug.Log("o R");
                    if (!BFManager.instance.activeBag.isInHand)
                    {
                        BFManager.instance.activeBag.PutInHand("R");
                        Debug.Log(":o R");
                    }
                    else
                    {
                        BFManager.instance.activeBag.PutOnBody();
                    }
                    nextGrab = Time.time + nextGrabcooldown;
                }
            }
        }
        if (Time.time > nextGrab)
        {
            if (collider.name == "LeftHandTriggerCollider")
            {
                if (gripl)
                {
                    if (!BFManager.instance.activeBag.isInHand)
                    {
                        BFManager.instance.activeBag.PutInHand("L");

                    }
                    else
                    {
                        BFManager.instance.activeBag.PutOnBody();
                    }
                    nextGrab = Time.time + nextGrabcooldown;
                }
            }
        }

    }

}

