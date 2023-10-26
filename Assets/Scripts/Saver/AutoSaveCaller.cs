using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSaveCaller : MonoBehaviour
{
    private Saver _saver;

    private void Start()
    {
        _saver= new Saver(SceneManager.GetActiveScene().name);
        _saver.Load();
    }

    public void Save()
    {
        _saver.Save();
    }

    private void OnApplicationQuit()
    {
        _saver.Save();
    }
}
