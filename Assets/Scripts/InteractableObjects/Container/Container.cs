using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UniRx.Triggers;
using UniRx;

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
        sceneMassage = new SceneMessage(Trigger.transform);
        sceneMassage.SetHeadMessage(Name);
        stateMachine = new StateMachine<Container, SaleState>(this);
        //Trigger.OnTriggerStayAsObservable().Subscribe(collider =>TriggerStay(collider)).AddTo(_disposables);
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
