using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SaveData(BFManager bFManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.streamingAssetsPath + "/BananaFiendSaveData.seventy";

        FileStream  fileStream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(bFManager);


        formatter.Serialize(fileStream, data);
        fileStream.Close();

        Debug.Log("SAVED BANANA FIEND DATA TO " + path);
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.streamingAssetsPath + "/BananaFiendSaveData.seventy";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data =  binaryFormatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();

            return data;

        }
        else
        {
            Debug.Log("No savefile found :( path  was " + path);
            BFManager.instance.SaveData();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            PlayerData data = binaryFormatter.Deserialize(fileStream) as PlayerData;
            fileStream.Close();

            return data;
            
        }
    }
}

