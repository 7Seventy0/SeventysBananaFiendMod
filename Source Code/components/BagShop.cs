using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class BagShop : MonoBehaviour
{
    public static BagShop instance;
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
    int selectedBag;

    void Start()
    {
        UpdateBag();
        StartCoroutine(LateStart());
    }
    IEnumerator LateStart()
    {
        yield return new WaitForSeconds(2);
        NextBag();
        yield return new WaitForSeconds(2);
        PrevBag();
    }
    public void NextBag()
    {
        selectedBag++;
        if (selectedBag >= BagDataBase.instance.BagCount())
        {
            selectedBag = 0;
        }

        UpdateBag();
    }
    public void SelectBag()
    {
       if(BFManager.instance.bananas >= displayedBag.GetComponent<BagStats>().price)
        {
            BFManager.instance.bananas -= displayedBag.GetComponent<BagStats>().price;
            GameObject activeBag = Instantiate(displayedBag);

            BFManager.instance.NewActiveBag(activeBag);
        }
       
      
    }

    public void PrevBag()
    {
            selectedBag--;
        if (selectedBag < 0)
        {
            selectedBag = BagDataBase.instance.BagCount() -1;
        }

        UpdateBag();
    }
    public GameObject displayedBag;
    void UpdateBag()
    {
        if(displayedBag == null)
        {
            displayedBag = BagDataBase.instance.GetBag(selectedBag);
        }
        else
        {
            displayedBag.SetActive(false);
            displayedBag = BagDataBase.instance.GetBag(selectedBag);
            displayedBag.SetActive(true);
        }
    }

}


