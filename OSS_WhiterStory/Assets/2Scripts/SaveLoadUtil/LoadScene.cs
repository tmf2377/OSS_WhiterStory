using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public SaveManager saveManager;
    private void Start()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    public void EnterGame()
    {
        saveManager.ReadALL();
        
        SceneManager.LoadScene("1_StartScene");
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == "1_StartScene")
        {
            saveManager.AttachDataToPlayer();
        }
    }

    public void EnterNewGame()
    {
        saveManager.ClearData();
        saveManager.ReadALL();
        SceneManager.LoadScene("1_StartScene");
    }
}
