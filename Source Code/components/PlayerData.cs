using System;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class PlayerData
{
    public double bananas;
    public float bpcMultiplier;
    public float multiplier;
    public float workerSpeed;
    public float workerBPCM;
    public float bpcUpgradePrice;
    public float workerUpgradePrice;
    public float bananaUpgradePrice;
    public float bananaSellValue;
    public int prestige;
    public int prestigePoints;
    public bool autoSave;
    public bool hasWorkers;
    public bool hasBananas;
    public int playtimesec;
    public int playtimemin;
    public int playtimeh;
    public int playtimed;
    public int playtimew;
    public int multiplierpotions;
    public bool hasGenerator;
    public float generatorTime;
    public float generatorPrice;
    public int generatorStorage;
    public float generatorMaxStorage;

    public PlayerData(BFManager bfm)
    {
        bananas = bfm.bananas;
        bpcMultiplier = bfm.bpcMultiplier;
        multiplier = bfm.multiplier;
        workerSpeed = bfm.workerSpeed;
        workerBPCM = bfm.workerBPCM;
        bpcUpgradePrice = bfm.bpcUpgradePrice;
        workerUpgradePrice = bfm.workerUpgradePrice;
        autoSave = bfm.autoSave;
        hasWorkers = bfm.hasWorkers;
        prestige = bfm.prestige;
        prestigePoints = bfm.prestigePoints;
        bananaSellValue = bfm.bananaSellValue;
        bananaUpgradePrice = bfm.bananaUpgradePrice;
        hasBananas = bfm.hasBananas;
        playtimesec = bfm.playtimesec;
        playtimemin = bfm.playtimemin;
        playtimeh = bfm.playtimeh;
        playtimed = bfm.playtimed;
            playtimew = bfm.playtimew;
        multiplierpotions = bfm.multiplierpotions;
        hasGenerator = bfm.hasGenerator;
        generatorTime = bfm.generatorTime;
        generatorStorage = bfm.generatorStorage;
       generatorPrice = bfm.generatorPrice;
        generatorMaxStorage = bfm.generatorMaxStorage;
    }

}
