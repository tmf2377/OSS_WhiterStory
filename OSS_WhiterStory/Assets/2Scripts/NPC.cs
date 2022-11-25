using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform uiGroup1;
    public RectTransform uiGroup2;
    public RectTransform uiGroup3;
    public Animator anim;
    public GameManager manager;

    bool IsPause;

    Player enterPlayer;

    void Start()
    {
        IsPause = false;
    }

    public void Enter(Player player) //입장
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
        if (IsPause == false)
        {
            Time.timeScale = 0;
            IsPause = true;
        }
    }

    public void NpcExit() //일반 대화 퇴장
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 2049;
        uiGroup1.anchoredPosition = Vector3.down * 2573;
        uiGroup2.anchoredPosition = Vector3.down * 3087;
        Time.timeScale = 1;
        IsPause = false;
    }

    public void NpcTalk()
    {
        uiGroup.anchoredPosition = Vector3.down * 2049;
        uiGroup1.anchoredPosition = Vector3.zero;
    }
    public void NpcTalk1()
    {
        uiGroup1.anchoredPosition = Vector3.down * 2573;
        uiGroup2.anchoredPosition = Vector3.zero;
    }
    public void NpcTalk2()
    {
        uiGroup2.anchoredPosition = Vector3.down * 3087;
        uiGroup3.anchoredPosition = Vector3.zero;
    }
    public void GameStart()
    {
        anim.SetTrigger("doHello");
        uiGroup3.anchoredPosition = Vector3.down * 3613;
        Time.timeScale = 1;
        IsPause = false;
    }
}
