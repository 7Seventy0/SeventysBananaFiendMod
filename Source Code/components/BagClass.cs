using UnityEngine;
using TMPro;
using TMPLoader;
using BananaHook;
using Photon.Pun;
public class BagClass : MonoBehaviour
{
    public BagStats stats;

    public bool isInHand;
    public bool isFull;
    public bool isEmpty;
    TextMeshPro count;
    GameObject rilla;

   

     void Start()
    {
        stats = gameObject.GetComponent<BagStats>();
        rilla = GameObject.Find("Actual Gorilla");
        count = GetComponentInChildren<TextMeshPro>();
        InvokeRepeating("SuperCoolMethod", 0, 0.2f);
        gameObject.layer = 11;
        Invoke("LateStart", 1);
        BananaHook.Events.OnPlayerTagPlayer += PlayerTagged;
    }
    void LateStart()
    {
        PutOnBody();
    }
    void Update()
    {
        if(stats.bananaStorage == stats.bagNanaLimit)
        {
            isFull = true;
        }
        else
        {
           isFull=false;
        }

        if (stats.bananaStorage <= 0)
        {
            isEmpty = true;
        }
        else
        {
            isEmpty = false;
        }
    }

    public void PlayerTagged(object sender,  PlayerTaggedPlayerArgs args)
    {
        Debug.Log("Gamer");
        if (!args.victim.IsLocal && args.tagger.IsLocal)
        {
            foreach (PhotonView view in GameObject.FindObjectsOfType<PhotonView>())
            {
                if (view.CreatorActorNr == args.victim.ActorNumber)
                {
                    MouthNana nana = view.gameObject.GetComponentInChildren<MouthNana>();
                    nana.KillMe();
                    AddBanana();
                }
            }
        }
    }

    void SuperCoolMethod()
    {
       
        count.text = stats.bananaStorage.ToString() + "/" + stats.bagNanaLimit;
    }
    public void AddBanana()
    {
        if(!isFull)
        {
            stats.bananaStorage++;
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.GetComponent<Brunnen>() != null && stats.bananaStorage > 0)
        {
            BFManager.instance.BrunnenSell();
            stats.bananaStorage--;
            return;
        }
    }

    public void PutOnBody()
    {
        if(rilla == null)
        {
            rilla = GameObject.Find("Actual Gorilla");
        }
        transform.SetParent(rilla.transform, false);
        transform.localPosition = stats.offsetpos;
        transform.localEulerAngles = stats.offseteula;
        isInHand = false;
    }

    public void PutInHand(string side)
    {
        if(side == "R")
        {
            transform.SetParent(GameObject.Find("palm.01.R").transform);
            transform.localPosition = Vector3.zero;
            isInHand=true;
        }
        if(side == "L")
        {
            transform.SetParent(GameObject.Find("palm.01.L").transform);
            transform.localPosition = Vector3.zero;
            isInHand=true;
        }
    }





}

