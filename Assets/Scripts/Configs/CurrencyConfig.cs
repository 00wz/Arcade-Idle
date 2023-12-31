using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class CurrencyConfig
#if UNITY_EDITOR
    : ScriptableSingleton<CurrencyConfig>
#endif
{
    [Serializable]
    public struct CurrencyClass
    {
        public string Name;
        public Sprite Icon;
        public GameObject Prefab;
    }

    [SerializeField]
    private List<CurrencyClass> CurrencyList = new List<CurrencyClass>();

    public ReadOnlyCollection<CurrencyClass> currencyList => CurrencyList.AsReadOnly();

    public CurrencyClass GetCurrencyStruct(Currency currency)
    {
        var name = nameof(currency);
        for(int i = 0; i < CurrencyList.Count; i++)
        {
            if (CurrencyList[i].Name == name)
                return CurrencyList[i];
        }

        throw new Exception($"Error currency: {name}. Check CurrencyConfig and CurrencyEnum!");
    }

#if UNITY_EDITOR

    private const string ENUM_PATH ="Configs/CurrencyEnum.cs";
    private const string HEADER_ENUM =
        "//Do not edit! Automatically updated by Configs/CurrencyConfig.asset\n" +
        "public enum Currency\n" +
        "{\n";
    private const string FOOTER_ENUM =
        "}";

    private void OnValidate()
    {
        var fullPath=Path.Combine(Application.dataPath, ENUM_PATH);
        UpdateCurrencyEnum(fullPath);
    }
    
    private void UpdateCurrencyEnum(string path)
    {
        using (var fs = new FileStream(path, FileMode.Truncate))
        using (var sw = new StreamWriter(fs))
        {
            sw.Write(HEADER_ENUM);
            for (int i = 0; i < CurrencyList.Count; i++)
            {
                sw.Write("  "+CurrencyList[i].Name + ",\n");
            }
            sw.Write(FOOTER_ENUM);
            sw.Flush();
        }
    }

#endif
}
