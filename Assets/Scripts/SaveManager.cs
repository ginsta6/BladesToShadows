using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveFile activeSave;
    public static bool hasLoaded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (!hasLoaded)
                Load();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteSaveData();
        }
    }

    public void Save()
    {
        SaveData data = new SaveData(Coins.instance.GetBalance(), Calendar.instance.GetDayCount());
        if (instance.activeSave.data.Count == 5)
        {
            if (data.Gold > instance.activeSave.data.Last().Gold)
            {
                instance.activeSave.data.Remove(instance.activeSave.data.Last());
                instance.activeSave.data.Add(data);
                instance.activeSave.data.Sort((x, y) => y.Gold.CompareTo(x.Gold));
            }
            
        }
        else if (instance.activeSave.data.Count < 5)
        {
            instance.activeSave.data.Add(data);
            instance.activeSave.data.Sort((x, y) => y.Gold.CompareTo(x.Gold));
        }


        string dataPath = Application.persistentDataPath;

        var serializer = new XmlSerializer(typeof(SaveFile));
        var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();

        Debug.Log("Saved");

    }

    public void Load()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + activeSave.saveName + ".save")) 
        {
            var serializer = new XmlSerializer(typeof(SaveFile));
            var stream = new FileStream(dataPath + "/" + activeSave.saveName + ".save", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveFile;
            stream.Close();

            Debug.Log("Loaded");
            hasLoaded = true;
        }
    }

    public void DeleteSaveData()
    {
        string dataPath = Application.persistentDataPath;

        if (File.Exists(dataPath + "/" + activeSave.saveName + ".save"))
        {
            File.Delete(dataPath + "/" + activeSave.saveName + ".save");
            Debug.Log("Deleted");
            hasLoaded = false;
        }
    }

}

[System.Serializable]
public class SaveFile
{
    public string saveName;
    public List<SaveData> data;
}

[System.Serializable]
public class SaveData
{
    public float Gold;
    public int dayCount;

    public SaveData(float gold, int days)
    {
        Gold = gold;
        dayCount = days;
    }

    public SaveData()
    {

    }
}
