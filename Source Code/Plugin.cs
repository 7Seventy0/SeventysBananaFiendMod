using BepInEx;
using HarmonyLib;
using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using Utilla;
using PolyLabs;

namespace SeventysBananaFiendMod
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        bool inRoom;
        TextMeshPro bananaText;
        TextMeshPro BPCMTextUpgrade;
        TextMeshPro generatorUpgrade;
        TextMeshPro WorkerTextUpgrade;
        TextMeshPro MouthNanaText;
        TextMeshPro shopBagInfoText;
        TextMeshPro multiplierPotionText;
        TextMeshPro playtimeText;

        GameObject bananafiend;

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled*/

            HarmonyPatches.ApplyHarmonyPatches();
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

            HarmonyPatches.RemoveHarmonyPatches();
            Utilla.Events.GameInitialized -= OnGameInitialized;
        }
        GameObject DisplayBagAnchor;
        void OnGameInitialized(object sender, EventArgs e)
        {
            StartCoroutine(SeventysStart());
        }
        IEnumerator SeventysStart()
        {
            var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SeventysBananaFiendMod.Assets.bananafiend");
            var bundleLoadRequest = AssetBundle.LoadFromStreamAsync(fileStream);
            yield return bundleLoadRequest;

            var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
            if (myLoadedAssetBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }
            
            var assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("BFM");
            yield return assetLoadRequest;

            GameObject gameArea = assetLoadRequest.asset as GameObject;
           bananafiend = Instantiate(gameArea);

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("MonkeyAhSound");
            yield return assetLoadRequest;

            GameObject monkeyahsound = assetLoadRequest.asset as GameObject;


            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("BananaClickParticle");
            yield return assetLoadRequest;

            GameObject clickParticle = assetLoadRequest.asset as GameObject;

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("PurchaseSound");
            yield return assetLoadRequest;

            GameObject purchaseSound = assetLoadRequest.asset as GameObject;

            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("Lvl1BackPack");
            yield return assetLoadRequest;

            GameObject baggo = assetLoadRequest.asset as GameObject;
            baggo = Instantiate(baggo);
            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("Lvl2Bag");
            yield return assetLoadRequest;

            GameObject lvl2bag = assetLoadRequest.asset as GameObject;
            lvl2bag = Instantiate(lvl2bag);
            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("ValueBanana");
            yield return assetLoadRequest;

            GameObject mouthNana = assetLoadRequest.asset as GameObject;
            Instantiate(mouthNana);
            assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("MultiplierPotion");
            yield return assetLoadRequest;

            GameObject multiplierpotion = assetLoadRequest.asset as GameObject;
           

            yield return new WaitForSeconds(0.1f);
            //get all content reffs
            GameObject.Find("ClickArea").AddComponent<Clicker>();
            GameObject.Find("BPCMultiplierUpgradeButton").AddComponent<BPCMUpgrade>();
            GameObject.Find("BuyWorkers").AddComponent<WorkerUpgrade>();
            DisplayBagAnchor = GameObject.Find("DisplayBagAnchor");
           baggo.transform.SetParent(DisplayBagAnchor.transform, false);
           BagStats bagstats =  baggo.AddComponent<BagStats>();
            bagstats.offsetpos = new Vector3(0f, 1.5f, -0.25f);
            bagstats.offseteula = new Vector3(270.8918f, 84.9989f, 179.9999f);
            bagstats.bagScale = new Vector3(20,20,20);
            bagstats.bagNanaLimit = 5;
            bagstats.price = 1000000;
            bagstats.bagName = "Green Bag";
            bagstats.bagDec = "Your first Bag:)";
            yield return new WaitForSeconds(0.3f);
            lvl2bag.transform.SetParent(DisplayBagAnchor.transform, false);
            BagStats lvl2stats =  lvl2bag.AddComponent<BagStats>();
            lvl2stats.offsetpos = new Vector3(-0.15f, 1.4f, - 0.1f);
            lvl2stats.offseteula = new Vector3(270.7605f, 262.6025f, - 0.0023f);
            lvl2stats.bagScale = new Vector3(19.32f, 17.52f, 8f);
            lvl2stats.bagNanaLimit = 10;
            lvl2stats.price = 10000000;
            lvl2stats.bagName = "Ghostly Bag";
            lvl2stats.bagDec = "A Bag that will make you stand out!";

            yield return new WaitForSeconds(0.1f);
            GameObject.Find("BuyBananas").AddComponent<MouthNanaUpgrade>();
            GameObject.Find("BuyBananaGeneratorButton").AddComponent<BuyGenerator>();
            GameObject brunnen = GameObject.Find("brunnen");
            brunnen.AddComponent<Brunnen>();

            yield return new WaitForSeconds(0.3f);
            bananaText = GameObject.Find("BanaText").GetComponent<TextMeshPro>();
            BPCMTextUpgrade = GameObject.Find("UpradeBPCMText").GetComponent<TextMeshPro>();
            WorkerTextUpgrade = GameObject.Find("BuyWorkersCostText").GetComponent<TextMeshPro>();
            playtimeText = GameObject.Find("PlayTimeText").GetComponent<TextMeshPro>();
            MouthNanaText = GameObject.Find("BuyBananasText").GetComponent<TextMeshPro>();
            shopBagInfoText = GameObject.Find("ShopBagInfoText").GetComponent<TextMeshPro>();
            multiplierPotionText = GameObject.Find("BuyMultiplierPotionText").GetComponent<TextMeshPro>();
            generatorUpgrade = GameObject.Find("BuyGeneratorText").GetComponent<TextMeshPro>();
            yield return new WaitForSeconds(0.3f);
            GameObject.Find("BagStore").AddComponent<BagShop>();
            GameObject.Find("BuyMultiplierPotionButton").AddComponent<PotionPurchase>().ID = 1;
            GameObject.Find("BagStore").AddComponent<BagDataBase>();
            GameObject.Find("PreviousBagButton").AddComponent<PrevBagButton>();
            GameObject.Find("NextBagButton").AddComponent<NextBagButton>();
            GameObject.Find("SelectBagButton").AddComponent<SelectBagButton>();


            BagDataBase.instance.DisplayBags.Add(baggo);
            BagDataBase.instance.DisplayBags.Add(lvl2bag);


            yield return new WaitForSeconds(0.5f);
            GorillaTagger.Instance.gameObject.AddComponent<BFManager>();

            BFManager.instance.clickParticle = clickParticle;
            BFManager.instance.monkeyAhSound = monkeyahsound    ;
            BFManager.instance.purchaseSound = purchaseSound    ;
            BFManager.instance.Brunnen = brunnen    ;
            BFManager.instance.multiplierPotion = multiplierpotion;
            

            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.SetParent(GameObject.Find("Actual Gorilla").GetComponent<VRRig>().head.rigTarget, false);
            cube.transform.localPosition = new Vector3(0,0.15f, -0.2f);
            cube.transform.localEulerAngles = new Vector3(0,0, 0);
            cube.transform.localScale = new Vector3(0.5f, 0.4f, 0.3f);
            cube.AddComponent<BagTrigger>();
            cube.GetComponent<BoxCollider>().isTrigger = true;
            cube.GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(6f);
            InvokeRepeating("SlowUpdate", 0, 0.2f);

            doneLoading = true;
        }
        bool doneLoading;
        void Update()
        {
            if (doneLoading)
            {
                if(!inRoom && bananafiend.activeSelf)
                {
                    bananafiend.SetActive(false);
                    BFManager.instance.inGame = false;
                }
                else if(inRoom && !bananafiend.activeSelf)
                {
                    bananafiend.SetActive(true);
                    BFManager.instance.inGame = true;
                }
            }
        }
        
        void SlowUpdate()
        {
            playtimeText.text = "Play Time :\n" + BFManager.instance.playtimew + "w " + BFManager.instance.playtimed + "d " + BFManager.instance.playtimeh + "h " + BFManager.instance.playtimemin + "m " + BFManager.instance.playtimesec + "s ";
            BagStats stats = BagShop.instance.displayedBag.GetComponent<BagStats>();
            shopBagInfoText.text = stats.bagName + "\n<#FFFFFF><size=20>Can Carry : <#FFEC00>" + stats.bagNanaLimit + " Bananas!<#FFFFFF>\nPrice :<#FFEC00> " + ShortScale.ParseDouble(stats.price, 2, 1000) + " $<#FFFFFF> \n |----------------------------------------------------|\n<size=15>" + stats.bagDec;
           
            bananaText.text = "<#FFEC00>Bananas : " + ShortScale.ParseDouble(BFManager.instance.bananas,2,1000) + "\n" + "<#fff>BPC : " + ShortScale.ParseDouble(BFManager.instance.bananasPerClick, 2, 1000) + "$\nHas Workers? "+ BFManager.instance.hasWorkers;
          
            if (BFManager.instance.multiplierpotions < 30)
            {

                multiplierPotionText.text = "Buy <#FFC100>Multiplier<#FFFFFF> Potion \n <#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.potions[2, 1], 2, 1000) + "$ \n<#FFFFFF><size=6>Permanently increase <#FFEC00>Banana<#FFFFFF> Multiplier by 0.1! Current :<#27FF00> " + ShortScale.ParseDouble(BFManager.instance.multiplier, 2, 1000);
            }
            else
            {
                multiplierPotionText.text = "<#FF0000>Sold Out";
            }
            
            BPCMTextUpgrade.text = "UPGRADE BPCM\n<#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.bpcUpgradePrice, 2, 1000) + "$";
            if (BFManager.instance.workerSpeed >= 0.1f)
            {

            if (BFManager.instance.hasWorkers)
            {
              WorkerTextUpgrade.text = "Upgrade Workers\n<#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.workerUpgradePrice, 2, 1000) + "$\n<size=5><#AEFF00>"+ ShortScale.ParseDouble(BFManager.instance.bananasPerWorkTick, 2, 0) + " Per Tick / Tick Speed  = " + BFManager.instance.workerSpeed;

            }
            else
            {
                WorkerTextUpgrade.text = "Buy Workers\n<#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.workerUpgradePrice, 2, 1000) + "$";
            }
            }
            else
            {
                WorkerTextUpgrade.text = "Upgrade Workers\n<#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.workerUpgradePrice, 2, 1000) + "$\n<size=5><#AEFF00>" + ShortScale.ParseDouble(BFManager.instance.bananasPerWorkTick, 2, 0) + " Per Tick / Tick Speed  = MAX! (0.1s)" ;
            }
            if (BFManager.instance.hasBananas)
            {

               MouthNanaText.text = "Upgrade Bananas\n<#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.bananaUpgradePrice, 2, 1000) + "$\n<size=5><#AEFF00>" + ShortScale.ParseDouble(BFManager.instance.bananasperbananaSell, 2, 1000) + "$ Per Sell";
            }
            else
            {
                MouthNanaText.text = "Buy Bananas\n<#FFEC00>" + ShortScale.ParseDouble(BFManager.instance.bananaUpgradePrice, 2, 1000) + "$";
            }
            if (BFManager.instance.hasGenerator)
            {
                generatorUpgrade.text = String.Format("Buy Generator Upgrade\n<#FFEC00>{0}$", ShortScale.ParseDouble(BFManager.instance.generatorPrice, 2, 1000));
            }
            else
            {
                generatorUpgrade.text = String.Format("Buy Generator\n<#FFEC00>{0}$\n<#FFFFFF><size=7>Requires Bananas", ShortScale.ParseDouble(BFManager.instance.generatorPrice, 2, 1000));
            }
        }

       
        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {

            inRoom = true;
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {

            inRoom = false;
        }
    }
    namespace SolidMonkeys.Patches
    {
        [HarmonyPatch(typeof(VRRig))]
        [HarmonyPatch("Start", MethodType.Normal)]
        internal class VRRigColliderPatch
        {
            public static bool ModEnabled { get; set; }

            private static void Postfix(VRRig __instance)
            {
                if (__instance.isOfflineVRRig)
                    return;

                Photon.Pun.PhotonView photView = __instance.photonView;
                if (photView != null && photView.IsMine)
                    return;


                __instance.gameObject.AddComponent<MouthBananaPatch>();



                // Debug.Log("Thank you Haunted!");
            }
        }
    }
}
