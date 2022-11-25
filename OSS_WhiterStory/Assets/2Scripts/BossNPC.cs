using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossNPC : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform uiGroup1;
    public RectTransform uiGroup2;
    public RectTransform uiGroup3;
    public RectTransform BossAliveGroup;
    public RectTransform GameClearGroup;
    public Text killBossText;
    public Animator anim;
    public int killBoss;
    public GameManager manager;

    bool IsPause;

    Player enterPlayer;

    void Start()
    {
        IsPause = false;
    }

    public void Awake()
    {
        killBossText.text = killBoss.ToString();
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
        Time.timeScale = 1;
        IsPause = false;
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 4133;
        uiGroup1.anchoredPosition = Vector3.down * 4652;
        uiGroup2.anchoredPosition = Vector3.down * 5177;
    }
    public void NpcTalk()
    {
        uiGroup.anchoredPosition = Vector3.down * 4133;
        uiGroup1.anchoredPosition = Vector3.zero;
    }
    public void NpcTalk1()
    {
        uiGroup1.anchoredPosition = Vector3.down * 4652;
        uiGroup2.anchoredPosition = Vector3.zero;
    }
    public void NpcTalk2()
    {
        uiGroup2.anchoredPosition = Vector3.down * 5177;
        uiGroup3.anchoredPosition = Vector3.zero;
    }
    public void GameStart()
    {
        anim.SetTrigger("doHello");
        uiGroup3.anchoredPosition = Vector3.down * 5699;
        Time.timeScale = 1;
        IsPause = false;
    }

    public void BossquestExit()
    {
        BossAliveGroup.anchoredPosition = Vector3.down * 6086;
    }

    public void GameClearExit()
    {
        GameClearGroup.anchoredPosition = Vector3.down * 6351;
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
            uiGroup3.anchoredPosition = Vector3.down * 5699;
            //게임 클리어 UI
            GameClearGroup.anchoredPosition = Vector3.zero;
        }
        else //못 죽였으면
        {
            BossAliveGroup.anchoredPosition = Vector3.zero;
        }
    }
}
