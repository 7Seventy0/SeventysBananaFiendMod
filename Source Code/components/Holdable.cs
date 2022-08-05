using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR;

public class Holdable : MonoBehaviour
{
    private readonly XRNode rNode = XRNode.RightHand;
    private readonly XRNode lNode = XRNode.LeftHand;
    bool gripr;
    bool gripl;
    bool isHolding;
    public bool isInRightHand;
    void Start()
    {
        gameObject.layer = 18;

    }
    void Update()
    {
        if (transform.parent == null)
        {
            isHolding = false;
        }

    }

    public void KillMe()
    {
        Destroy(gameObject);
    }
    float nextgrab;
    float grabcooldown = 0.2f;
    void OnTriggerStay(Collider other)
    {
        InputDevices.GetDeviceAtXRNode(rNode).TryGetFeatureValue(CommonUsages.gripButton, out gripr);
        InputDevices.GetDeviceAtXRNode(lNode).TryGetFeatureValue(CommonUsages.gripButton, out gripl);
        if (Time.time > nextgrab)
        {
            if (other.name == "RightHandTriggerCollider")
            {
                if (gripr)
                {
                    if (!isHolding && GameObject.Find("palm.01.R").GetComponentInChildren<Holdable>() == null)

                    {
                        Debug.Log("Starting to hold " + gameObject.name);
                        transform.SetParent(GameObject.Find("palm.01.R").transform);
                        isHolding = true;
                        isInRightHand = true;
                        nextgrab = Time.time + grabcooldown;
                    }
                    else
                    {
                        Debug.Log("Letting Go of " + gameObject.name);
                        transform.parent = null;
                        nextgrab = Time.time + grabcooldown;
                    }
                    
                }
            }
            if (other.name == "LeftHandTriggerCollider")
            {
                if (gripl)
                {
                    if (!isHolding && GameObject.Find("palm.01.L").GetComponentInChildren<Holdable>() == null)
                    {
                        transform.SetParent(GameObject.Find("palm.01.L").transform);
                        isHolding = true;
                        isInRightHand = false;
                        nextgrab = Time.time + grabcooldown;
                    }
                    else
                    {
                        transform.parent = null;
                        nextgrab = Time.time + grabcooldown;
                    }
                    
                }
            }
        }
    }


}

