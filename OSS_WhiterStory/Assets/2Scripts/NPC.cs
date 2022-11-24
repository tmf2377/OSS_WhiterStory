using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public RectTransform uiGroup;
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
        uiGroup.anchoredPosition = Vector3.down * 2031;
    }
}
