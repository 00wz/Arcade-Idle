using UnityEngine;

public class GameRootInstance : MonoBehaviour
{
    [SerializeField]
    public NavMashUpdater NavMashUpdater;

    [SerializeField]
    public Canvas Canvas;

    [SerializeField]
    public MainInventory MainInventory;
    
    static private GameRootInstance _instance;
    static public GameRootInstance Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = UnityEngine.Object.FindObjectOfType<GameRootInstance>();
            }
            return _instance;
        }
    }

}
