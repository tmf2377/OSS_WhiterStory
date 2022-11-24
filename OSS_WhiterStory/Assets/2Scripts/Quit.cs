using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public GameObject quitPanel;
    bool IsPause;

    void Start()
    {
        IsPause = false;
    }

    public void isQuit()
    {
        // 게임 일시정지 코드
        if(IsPause == false)
        {
            quitPanel.SetActive(true);
            Time.timeScale = 0;
            IsPause = true;
        }
    }

    public void YesQuit()
    {
      //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void NoQuit()
    {
        // 게임 다시 진행하는 코드
        quitPanel.SetActive(false);
        Time.timeScale = 1;
        IsPause = false;
    }
}
