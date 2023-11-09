using System;
using System.Collections.Generic;
using System.Text;

public class SaleState : BaseState<Container>
{
    private List<Container.CurrencyAndPrice> priceList;
    private StringBuilder stringBuilder = new();

    public SaleState(Container context, Action<Type> changeStateCallback) : base(context, changeStateCallback)
    {
    }

    public override void Enter()
    {
        priceList = new List<Container.CurrencyAndPrice>(context.PriceList);
        context.HideContent();
        ShowCurrentBalance();
    }

    public override void Interract(ICharacter character)
    {
        bool modified = false;
        for (int i = priceList.Count-1; i >=0; i--)
        {
            if (character.inventary[priceList[i].Currency] > 0)
            {
                character.inventary[priceList[i].Currency]--;
                modified = true;
                if(priceList[i].Price-1<=0)
                {
                    priceList.RemoveAt(i);
                    continue;
                }
                priceList[i] = new Container.CurrencyAndPrice
                {
                    Currency = priceList[i].Currency,
                    Price = priceList[i].Price - 1
                };
            }
        }
        if (priceList.Count <= 0)
        {
            ChangeState<PurchasedState>();
            return;
        }
        if (modified)
        {
            ShowCurrentBalance();
        }
    }

    private void ShowCurrentBalance()
    {
        int last = priceList.Count - 1;
        if (last < 0)
        {
            return;
        }

        stringBuilder.Clear();
        for(int i = 0; i < last; i++)
        {
            stringBuilder.Append(priceList[i]);
            stringBuilder.Append("\n");
        }
        stringBuilder.Append(priceList[last]);

        context.sceneMassage.SetBodyMessage(stringBuilder.ToString());
    }
}
