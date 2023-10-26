using TMPro;
using UnityEngine;

public class SceneMessageView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Head;

    [SerializeField]
    private TMP_Text Body;
    
    public void SetHeadMessage(string massage)
    {
        Head.text = massage;
    }

    public void SetBodyMessage(string massage)
    {
        Body.text = massage;
    }
}
