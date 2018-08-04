using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public float AutoLoadNextLevelAfter;

    private void Start()
    {
        if (AutoLoadNextLevelAfter == 0)
        {
            Debug.Log("level auto load disable");
        }
        else
        {
            Invoke("LoadNextLevel", AutoLoadNextLevelAfter);
        }
    }

    public void LoadLevel(string name)
    {
        Debug.Log("New Level Load: " + name);
        Application.LoadLevel(name);
    }
    public void QuitRequest()
    {
        Debug.Log("Quit requested");
        Application.Quit();
    }
    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
