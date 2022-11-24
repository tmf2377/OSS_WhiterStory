using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossNPC : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform BossAliveGroup;
    public Text killBossText;
    public Animator anim;
    public int killBoss;
    public GameManager manager;

    Player enterPlayer;

    public void Awake()
    {
        killBossText.text = killBoss.ToString();
    }
    public void Enter(Player player) //입장
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void NpcExit() //일반 대화 퇴장
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 2546;
    }

    public void BossquestExit()
    {
        BossAliveGroup.anchoredPosition = Vector3.down * 3241;
    }

    public void CatchMonster() //보스 몬스터가 죽는 함수에 삽입
    {
        killBoss++;
        killBossText.text = killBoss.ToString();
    }
    public void GameClear()
    {
        if (killBossText.text == "1") //보스 몬스터를 죽였으면
        {
            uiGroup.anchoredPosition = Vector3.down * 2546;
            //게임 클리어 UI
        }
        else //못 죽였으면
        {
            BossAliveGroup.anchoredPosition = Vector3.zero;
        }
    }
}
