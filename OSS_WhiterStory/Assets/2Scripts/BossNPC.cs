using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossNPC : MonoBehaviour
{
    public int CntBoss;
    public int KillBoss;
    public RectTransform uiset;
    public RectTransform QuestFail;
    public Animator anim;
    public Text killBoss;
    public Text cntBoss;
    public GameManager manager;

    Player enterPlayer;

    public void Awake()
    {
        cntBoss.text = "/ " + CntBoss.ToString() + " (Boss)";
        killBoss.text = KillBoss.ToString();
    }
    public void CatchMonster() //보스 몬스터가 죽는 함수에 삽입
    {
        KillBoss++;
        killBoss.text = KillBoss.ToString();
    }

    public void Enter(Player player) //대화 걸기
    {
        enterPlayer = player;
        uiset.anchoredPosition = Vector3.zero;
    }

    public void npcExit()
    {
        anim.SetTrigger("doHello");
        uiset.anchoredPosition = Vector3.down * 2049;
    }
    public void questExit()
    {
        QuestFail.anchoredPosition = Vector3.down * 2495;
    }

    public void NextStage()
    {
        if (killBoss.text == CntBoss.ToString()) //보스 몬스터를 죽였으면
        {
            SceneManager.LoadScene(manager.stage + 1); //스테이지 번호로 조정
        }
        else //못 죽였으면
        {
            QuestFail.anchoredPosition = Vector3.zero;
        }
    }
}
