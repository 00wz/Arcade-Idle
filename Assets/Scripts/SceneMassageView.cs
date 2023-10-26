using TMPro;
using UnityEngine;

public class SceneMassageView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Head;

    [SerializeField]
    private TMP_Text Body;
    
    public void SetHeadMassage(string massage)
    {
        Head.text = massage;
    }

    public void SetBodyMassage(string massage)
    {
        Body.text = massage;
    }
}
