using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class Saver
{
    private string _dataPath;
    private Dictionary<string, ISaveble> SaveList = new Dictionary<string, ISaveble>();

    public Saver(string fileName)
    {
        UpdateSaveList();
        _dataPath = Path.Combine(Application.streamingAssetsPath, fileName);
    }

    public void UpdateSaveList()
    {
        var savelist = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveble>().ToArray();

        foreach (var saveble in savelist)
        {
            MonoBehaviour savableMonoBehavior = (MonoBehaviour)saveble;
            var uniqueName = savableMonoBehavior.gameObject.name + nameof(savableMonoBehavior);
            if (SaveList.ContainsKey(uniqueName))
            {
                if (savableMonoBehavior.gameObject == ((MonoBehaviour)SaveList[uniqueName]).gameObject)
                {
                    Debug.LogError($"There are several ISaveble with the same names: " +
                        $"\"{nameof(savableMonoBehavior)}\" on the GameObject: " +
                        $"\"{savableMonoBehavior.gameObject.name}\"");
                }
                else
                {
                    Debug.LogError($"There are several objects on the scene with the " +
                        $"same names: \"{savableMonoBehavior.gameObject.name}\"");
                }
            }
            else
            {
                SaveList.Add(uniqueName, saveble);
            }
        }
    }

    public void Save()
    {
        Dictionary<string, ArrayList> SaveData = new();
        foreach (var s in SaveList)
        {
            SaveData.Add(s.Key, s.Value.Save());
        }

        Directory.CreateDirectory(Application.streamingAssetsPath);
        BinaryFormatter formatter = new BinaryFormatter();
        using (var fs = new FileStream(_dataPath, FileMode.Create))
        {
            formatter.Serialize(fs, SaveData);
        }
    }

    public void Load()
    {
        if (!File.Exists(_dataPath))
        {
            Debug.Log("savefile not found  " + _dataPath);
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        Dictionary<string, ArrayList> SaveData;
        using (var fs = new FileStream(_dataPath, FileMode.Open))
        {
            SaveData = (Dictionary<string, ArrayList>)formatter.Deserialize(fs);
        }

        foreach (var s in SaveList)
        {
            if (SaveData.ContainsKey(s.Key))
            {
                s.Value.Load(SaveData[s.Key]);
            }
        }
    }
}