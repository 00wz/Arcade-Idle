using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRootInstance : MonoBehaviour
{
    [SerializeField]
    public NavMashUpdater NavMashUpdater;
    
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
