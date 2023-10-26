using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSaveCaller : MonoBehaviour
{
    private Saver _saver;

    void Start()
    {
        _saver= new Saver(SceneManager.GetActiveScene().name);
        _saver.Load();
    }

    private void OnApplicationQuit()
    {
        _saver.Save();
    }
}
