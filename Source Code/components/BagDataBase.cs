using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
public class BagDataBase : MonoBehaviour
{
    public static BagDataBase instance;
    public List<GameObject> DisplayBags  = new List<GameObject>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int BagCount()
    {
      
            return instance.DisplayBags.Count;
   
    }


    void Start()
    {
     
    }

    public GameObject GetBag(int index)
    {
        return DisplayBags[index];
    }
}
