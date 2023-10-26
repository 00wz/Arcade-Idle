using UnityEngine;
using TMPro;
using UniRx;
using System.Text;

public class StatisticUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text StatisticText;
    public CompositeDisposable _disposables = new();
    StringBuilder _stringBuilder = new();

    private void Awake()
    {
        RefreshStatistic();
        GameRootInstance.Instance.MainInventory.Inventory.ObserveAdd()
            .Subscribe(_ => RefreshStatistic());
        GameRootInstance.Instance.MainInventory.Inventory.ObserveReplace()
            .Subscribe(_ => RefreshStatistic());
    }

    private void RefreshStatistic()
    {
        _stringBuilder.Clear();
        foreach(var curr in GameRootInstance.Instance.MainInventory.Inventory)
        {
            _stringBuilder.Append(curr.Key);
            _stringBuilder.Append(" x ");
            _stringBuilder.Append(curr.Value);
            _stringBuilder.Append("\n");
        }
        StatisticText.text = _stringBuilder.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RefreshStatistic();
        }
    }

    private void OnDestroy()
    {
        _disposables.Clear();
    }
}
