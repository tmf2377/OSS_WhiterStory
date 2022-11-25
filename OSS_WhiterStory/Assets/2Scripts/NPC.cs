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


    Player enterPlayer;

    public void Enter(Player player) //입장
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void NpcExit() //일반 대화 퇴장
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 2049;
        uiGroup1.anchoredPosition = Vector3.down * 2573;
        uiGroup2.anchoredPosition = Vector3.down * 3087;
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
    }
}
