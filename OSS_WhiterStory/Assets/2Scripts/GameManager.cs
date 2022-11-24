using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net.Sockets;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject menuCam;
	public GameObject gameCam;
	public Boss boss;
	public GameObject itemShop;
	public GameObject weaponShop;
	public GameObject clearPortal;
	public GameObject enemyRespawnZone;
	public int stage;
	public float playTime;
	public bool isBattle;
	public int enemyCntA;
	public int enemyCntB;
	public int enemyCntC;
	public int enemyCntD;

	public GameObject[] enemyZones;
	public GameObject[] enemies;
	public List<int> enemyList;

	public GameObject menuPanel;
	public GameObject gamePanel;
	public GameObject overPanel;
	public Text maxScoreTxt;
	public Text scoreTxt;
	public Text stageTxt;
	public Text playTimeTxt;
	public Text playerHealthTxt;
	public Text playerAmmoTxt;
	public Text playerCoinTxt;
	public Image weapon1Img;
	public Image weapon2Img;
	public Image weapon3Img;
	public Image weaponRImg;
	public Text enemyATxt;
	public Text enemyBTxt;
	public Text enemyCTxt;
	public RectTransform bossHealthGroup;
	public RectTransform bossHealthBar;

	void Awake()
	{
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
        enemyList = new List<int>();
        maxScoreTxt.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore"));
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
		clearPortal.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (SceneManager.GetActiveScene().name != "0_StartStage")
			StageStart();
	}

    private void OnDisable()
    {
		SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void GameStart()
    {
		gameCam.SetActive(true);
		menuCam.SetActive(false);

		menuPanel.SetActive(false);
		gamePanel.SetActive(true);

		Player.instance.gameObject.SetActive(true);
    }

    public void GameOver()
    {
		stage--;
		gamePanel.SetActive(false);
		overPanel.SetActive(true);
    }

	public void Restart()
	{
		SceneManager.LoadScene("0_StartStage");
        gamePanel.SetActive(true);
        overPanel.SetActive(false);
		StageEnd();
    }

    public void StageStart()
	{
		isBattle = true;
		//stage = SceneManager.GetActiveScene().buildIndex;
		stage++;
		enemyZones = GameObject.FindGameObjectsWithTag("Enemy Zone");
        clearPortal = GameObject.FindGameObjectWithTag("StartZone");
        clearPortal.SetActive(false);
        foreach (GameObject zone in enemyZones)
            zone.SetActive(true);
        StartCoroutine(InBattle());
	}

	public void StageEnd()
	{
		clearPortal.SetActive(true);
        isBattle = false;

		foreach(GameObject zone in enemyZones)
			zone.SetActive(false);
	}

	IEnumerator InBattle()
	{
		for (int index = 0; index < stage; index++)
        {
            int ran = Random.Range(0, 3);
            enemyList.Add(ran);

            switch (ran)
            {
                case 0:
                    enemyCntA++;
                    break;
                case 1:
                    enemyCntB++;
                    break;
                case 2:
                    enemyCntC++;
                    break;
            }
        }

        while (enemyList.Count > 0)
        {
            int ranZone = Random.Range(0, 4);
            GameObject instantEnemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].transform.position, enemyZones[ranZone].transform.rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.manager = this;
            enemyList.RemoveAt(0);
			yield return new WaitForSeconds(10f);
        }

        while (enemyCntA + enemyCntB + enemyCntC > 0)
		{
			yield return null;
		}

        if (stage % 3 == 0)
        {
            yield return new WaitForSeconds(3f);
            enemyCntD++;
            GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].transform.position, enemyZones[0].transform.rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.target = Player.instance.transform;
            enemy.manager = this;
            boss = instantEnemy.GetComponent<Boss>();
        }

        while (enemyCntD > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(4f);
		boss = null;
		StageEnd();
	}

	private void Update()
    {
        if (isBattle)
			playTime += Time.deltaTime;
    }

	private void LateUpdate()
    {
        // 상단 UI
        scoreTxt.text = string.Format("{0:n0}", Player.instance.score);
		stageTxt.text = "STAGE " + stage;

		int hour = (int)(playTime / 3600);
		int min = (int)((playTime - hour * 3600) / 60);
		int second = (int)(playTime % 60);
		playTimeTxt.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second);

		// 플레이어 UI
		playerHealthTxt.text = Player.instance.health + " / " + Player.instance.maxHealth;
		playerCoinTxt.text = string.Format("{0:n0}", Player.instance.coin);
		if (Player.instance.equipWeapon == null)
			playerAmmoTxt.text = "- / " + Player.instance.ammo;
		else if (Player.instance.equipWeapon.type == Weapon.Type.Melee)
			playerAmmoTxt.text = "- / " + Player.instance.ammo;
		else
			playerAmmoTxt.text = Player.instance.equipWeapon.curAmmo + " / " + Player.instance.ammo;

		// 무기 UI
		weapon1Img.color = new Color(1, 1, 1, Player.instance.hasWeapons[0] ? 1 : 0);
		weapon2Img.color = new Color(1, 1, 1, Player.instance.hasWeapons[1] ? 1 : 0);
		weapon3Img.color = new Color(1, 1, 1, Player.instance.hasWeapons[2] ? 1 : 0);
		weaponRImg.color = new Color(1, 1, 1, Player.instance.hasGrenades > 0 ? 1 : 0);

		// 몬스터 숫자 UI
		enemyATxt.text = enemyCntA.ToString();
		enemyBTxt.text = enemyCntB.ToString();
		enemyCTxt.text = enemyCntC.ToString();

		// 보스체력 UI
		if(boss != null && SceneManager.GetActiveScene().name != "0_StartStage")
		{
			bossHealthGroup.anchoredPosition = Vector3.down * 30;
            bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
        }
		else
		{
			bossHealthGroup.anchoredPosition = Vector3.up * 200;
		}
	}
}
