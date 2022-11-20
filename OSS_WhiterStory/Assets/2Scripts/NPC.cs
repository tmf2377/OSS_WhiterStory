using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public RectTransform uiset;
    public Animator anim;

    Player enterPlayer;

    public void Enter(Player player) //대화 걸기
    {
        enterPlayer = player;
        uiset.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        anim.SetTrigger("doHello");
        uiset.anchoredPosition = Vector3.down * 2049;
    }
}
