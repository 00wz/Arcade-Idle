using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Container : InteractableObject
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
}
