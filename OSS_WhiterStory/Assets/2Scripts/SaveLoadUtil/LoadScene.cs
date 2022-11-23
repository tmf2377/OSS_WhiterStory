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
        SceneManager.LoadScene("0_StartStage");

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        saveManager.AttachDataToPlayer();
    }

    public void EnterNewGame()
    {
        saveManager.ClearData();
        saveManager.ReadALL();
        SceneManager.LoadScene("0_StartStage");
    }
}
