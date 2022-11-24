using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public SaveManager saveManager;
    bool is_first = true;
    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    public void EnterGame()
    {
        saveManager.ReadALL();
        SceneManager.LoadScene("0_StartStage");

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "0_StartStage")
        {
            saveManager.WriteALL();
            if (is_first)
            {
                saveManager.AttachDataToPlayer();
                is_first = false;
            }
        }
    }

    public void EnterNewGame()
    {
        saveManager.ClearData();
        saveManager.ReadALL();
        SceneManager.LoadScene("0_StartStage");
    }
}
