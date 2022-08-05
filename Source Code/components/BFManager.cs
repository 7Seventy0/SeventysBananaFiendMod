using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Threading;
using UnityEngine.XR;
using Photon.Pun;
public class BFManager : MonoBehaviour
{
    public static BFManager instance;

    public bool inGame;

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
    void Start()
    {
        LoadData();
        InvokeRepeating("SlowUpdate", 0, 0.1f);
        InvokeRepeating("SlowestUpdate", 0, 0.6f);
        // IDs
        potions[1, 1] = 1;
        potions[1, 2] = 2;

        //price
        potions[2, 1] = 8000;
        potions[2, 2] = 4000000;
        bagAnchor = GameObject.Find("DisplayBagAnchor").transform;
        InvokeRepeating("EverySec", 0, 1f);
    }

    //Saveable Data (Be sure to include said data in "PlayerData")
    public double bananas;
    public float bpcMultiplier = 1;
    public float multiplier = 1;

    public float workerSpeed = 1.5f;
    public float workerBPCM = 1.2f;
    public float bpcUpgradePrice = 10;
    public float bananaUpgradePrice = 100000;
    public float bananaSellValue = 60000;
    public float workerUpgradePrice = 50;

    public int prestige;
    public int prestigePoints;
    public int playtimesec;
    public int playtimemin;
    public int playtimeh;
    public int playtimed;
    public int playtimew;
    public int multiplierpotions;
    public bool autoSave = true;
    public bool hasWorkers = false;
    public bool hasBananas = false;
    public bool hasGenerator = false;
    public float generatorTime = 30f;
    public float generatorPrice = 12000000f;
    public int generatorStorage;
    public float generatorMaxStorage = 7;
    public float[,] potions = new float[5,5];
    //End
    float nextSave;
    float saveCooldown = 15f;
    public GameObject monkeyAhSound;
    public GameObject purchaseSound;
    public GameObject clickParticle;
    public GameObject Brunnen;
    public GameObject Generator;
    public GameObject BagShop;
    public BagClass activeBag;

    Transform bagAnchor;

    public GameObject multiplierPotion;



    public double bananasPerClick;
    public double bananasPerWorkTick;
    public double bananasperbananaSell;

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }
    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        bananas = data.bananas;
        bpcMultiplier = data.bpcMultiplier;
        multiplier = data.multiplier;
        workerSpeed = data.workerSpeed;
        workerSpeed = data.workerSpeed;
        workerBPCM = data.workerBPCM;
        bpcUpgradePrice = data.bpcUpgradePrice;
        workerUpgradePrice = data.workerUpgradePrice;
        bananaSellValue = data.bananaSellValue;
        bananaUpgradePrice = data.bananaUpgradePrice;
        autoSave = data.autoSave;
        hasWorkers = data.hasWorkers;
        hasBananas = data.hasBananas;
        prestige = data.prestige;
        prestigePoints = data.prestigePoints;
        playtimemin = data.playtimemin;
        playtimesec = data.playtimesec;
        playtimeh = data.playtimeh;
        playtimed = data.playtimed;
        playtimew = data.playtimew;
       hasGenerator = data.hasGenerator;
        multiplierpotions = data.multiplierpotions;
        generatorTime = data.generatorTime;
        generatorStorage = data.generatorStorage;
        generatorPrice = data.generatorPrice;
        generatorMaxStorage = data.generatorMaxStorage;
        Debug.Log("Yay Loaded A Save File! " + bananas);
    }


    void EverySec()
    {
        playtimesec++;
        if(playtimesec > 60)
        {
            playtimemin++;
            playtimesec = 0;
        }
        if (playtimemin > 60)
        {
            playtimeh++;
            playtimemin = 0;
        }
        if (playtimeh > 24)
        {
            playtimed++;
            playtimeh = 0;
        }
        if (playtimed > 7)
        {
            playtimew++;
            playtimed = 0;
        }
    }


    public void BananaParticle(Vector3 pos)
    {
        GameObject particle = Instantiate(clickParticle);
        particle.transform.position = pos;
        particle.transform.eulerAngles = Vector3.zero;
        particle.GetComponent<ParticleSystem>().Play();
    }
    public void MonkeyAhSound(Vector3 pos)
    {
        GameObject ahSound = Instantiate(monkeyAhSound);
        ahSound.transform.position = pos;
        ahSound.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.5f);
        ahSound.GetComponent<AudioSource>().volume = 0.15f;
        ahSound.GetComponent<AudioSource>().Play();
    }
    public void PurchaseSound(Vector3 pos)
    {
        GameObject sound = Instantiate(purchaseSound);
        sound.transform.position = pos;
        sound.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.5f);
        sound.GetComponent<AudioSource>().volume = 0.15f;
        sound.GetComponent<AudioSource>().Play();
    }



    float yvalue;

    float nextWorkerAction;
    public void Click()
    {
        bananas += (0.25f * bpcMultiplier) * multiplier;

    }
    void WorkerClick()
    {
        bananas += (1.5f * workerBPCM) * multiplier;
    }
    public void BrunnenSell()
    {
        bananas += bananaSellValue * multiplier;
    }
    
    void Update()
    {
        if(Time.time > nextSave)
        {
            SaveData();
            nextSave = Time.time + saveCooldown;
        }
        if (hasWorkers)
        {
            if (Time.time > nextWorkerAction)
            {
                WorkerClick();
                nextWorkerAction = Time.time + workerSpeed;
            }
        }
        if(Generator == null)
        {
            Generator = GameObject.Find("BananaGenerator");
            if(Generator.GetComponent<BananaGenerator>() == null)
            {
                Generator.AddComponent<BananaGenerator>();
            }
        }
        yvalue += 0.3f;

        if(bagAnchor == null) { bagAnchor = GameObject.Find("DisplayBagAnchor").transform; }
        bagAnchor.eulerAngles = new Vector3(0, yvalue, 0);
    }
    void SlowUpdate()
    {
        bananasPerClick = (0.25f * bpcMultiplier) * multiplier;
        bananasPerWorkTick = (1.5f * workerBPCM) * multiplier;
        bananasperbananaSell = (bananaSellValue)* multiplier;
    }
    void SlowestUpdate()
    {
        if (hasBananas && !Brunnen.activeSelf)
        {
            Brunnen.SetActive(true);
        }
        if (!hasBananas && Brunnen.activeSelf)
        {
            Brunnen.SetActive(false);
        }
        if(hasGenerator && !Generator.activeSelf)
        {
            Generator.SetActive(true);
        }
        if (!hasGenerator && Generator.activeSelf)
        {
            Generator.SetActive(false);
        }
    }
    public void AttemptBPCMUpgrade(Vector3 pos)
    {
        if (bananas >= bpcUpgradePrice)
        {
            bananas -= bpcUpgradePrice;
            bpcMultiplier *= 1.45f;
            bpcUpgradePrice *= Random.Range(1.3f, 2.2f);
            PurchaseSound(pos);
        }
    }
    public void AttemptPotionPurchase(float potionID,Vector3 pos)
    {
        Vector3 spawnpos = new Vector3(-65.058f, 20.6612f, -121.729f);
        if(bananas >= potions[2, (int)potionID])
        {
            bananas -= potions[2, (int)potionID];
            if(potionID == potions[1, 1] && multiplierpotions < 30)
            {
                Debug.Log("Purchased a Multiplier Potion!");
                GameObject potion = Instantiate(multiplierPotion,spawnpos,Quaternion.identity);
                potion.AddComponent<Holdable>();
                potion.AddComponent<Wobble>();
                potion.GetComponentInChildren<BoxCollider>().gameObject.AddComponent<MultiplierPotion>();
                
                multiplierpotions++;
                PurchaseSound(pos);
            }
           
        }
    }
    public void AttemptMouthNanasUpgrade(Vector3 pos)
    {
        if (bananas >= bananaUpgradePrice)
        {
            bananas -= bananaUpgradePrice;
            bananaSellValue *= 1.4f;
            bananaUpgradePrice *= Random.Range(1.2f, 2.5f);
            hasBananas = true;
            PurchaseSound(pos);
        }
    }
    public void AttemptGeneratorPurchase(Vector3 pos)
    {
        if (hasGenerator)
        {
            if (bananas >= generatorPrice && generatorTime >= 2f)
            {
                bananas -= generatorPrice;
                hasGenerator = true;
                generatorPrice *= Random.Range(1.6f, 2.5f);
                generatorTime /= 1.1f;
                generatorMaxStorage *= 1.3f;
                PurchaseSound(pos);
            }
        }
        else
        {
            if (bananas >= generatorPrice)
            {
                bananas -= generatorPrice;
                hasGenerator = true;
                generatorPrice *= 2;
                PurchaseSound(pos);
            }
        }

    }

    public void AttemptWorkerUpgrade(Vector3 pos)
    {
        if (bananas >= workerUpgradePrice)
        {
            if (hasWorkers)
            {
                if (workerSpeed > 0.1f)
                {
                    bananas -= workerUpgradePrice;
                    workerSpeed -= 0.1f;
                    workerUpgradePrice *= Random.Range(1.3f, 2f);
                    workerBPCM *= 1.45f;
                    PurchaseSound(pos);
                }
                else
                {
                    bananas -= workerUpgradePrice;

                    workerUpgradePrice *= Random.Range(1.8f, 2.3f);
                    workerBPCM *= 1.4f;
                    PurchaseSound(pos);
                }
            }
            else
            {
                bananas -= workerUpgradePrice;
                workerUpgradePrice *= 5;
                hasWorkers = true;
                PurchaseSound(pos);
            }
        }

    }

    public void NewActiveBag(GameObject newBag)
    {
        if (activeBag != null)
        {
            Destroy(activeBag.gameObject);
            activeBag = null;
            StartCoroutine(NewActiveBagFR(newBag));
        }
        else
        {
 StartCoroutine(NewActiveBagFR(newBag));
        }

    }

    IEnumerator NewActiveBagFR(GameObject bag)
    {
        yield return  new WaitForSeconds(0.3f);
        bag.AddComponent<BagClass>();
        activeBag = bag.GetComponent<BagClass>();
        yield return new WaitForSeconds(0.1f);
        activeBag.PutOnBody();
    }
}