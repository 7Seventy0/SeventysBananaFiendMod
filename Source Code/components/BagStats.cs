using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class BagStats : MonoBehaviour 
{
    public string bagName;
    public string bagDec;
    public int bagNanaLimit;
    public int bananaStorage;
    public float price;
    public Vector3 offsetpos;
    public Vector3 offseteula;
    public Vector3 bagScale;

    void Start()
    {
        Invoke("LateStart", 1);
    }
    void LateStart()
    {
        transform.localScale = bagScale;
        transform.localPosition = Vector3.zero;
    }
}
