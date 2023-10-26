using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class Container : InteractableObject,ISaveble
{
    [SerializeField]
    private GameObject Trigger;

    [SerializeField]
    private GameObject Content;

    [SerializeField]
    public List<CurrencyAndPrice> PriceList;

    [Serializable]
    public struct CurrencyAndPrice
    {
        public Currency Currency;
        public int Price;

        public override string ToString()
        {
            return $"{Currency}: {Price}";
        }
    }

    protected override void Awake()
    {
        sceneMassage = new SceneMassage(Trigger.transform);
        sceneMassage.SetHeadMassage(Name);
        stateMachine = new StateMachine<Container, SaleState>(this);
    }

    public void HideContent()
    {
        Trigger.SetActive(true);
        Content.SetActive(false);
    }

    public void ShowContent()
    {
        Trigger.SetActive(false);
        Content.SetActive(true);
    }

    public ArrayList Save()
    {
        ArrayList SaveParam = new();
        SaveParam.Add(stateMachine.currentState.GetType());
        return SaveParam;
    }

    public void Load(ArrayList data)
    {
        stateMachine.ChangeState((Type)data[0]);
    }
}
