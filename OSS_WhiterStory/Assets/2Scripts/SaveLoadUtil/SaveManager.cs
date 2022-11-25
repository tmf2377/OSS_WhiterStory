using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string GOLD = "gold";
    const string WEAPON = "weapon";
    const string WEAPONCOUNT = "weapon_count";
    const string HP = "hp";
    const string STAGE = "stage";
    const string AMMO = "ammo";
    const string SCORE = "score";
    const string TIME = "time";
    const string GRENADE = "grenade";
    const string INVEN_COUNT = "inven_count";
    const string INVEN_INDEX_PRE = "inven_index";

    public Player player;
    public GameManager gameManager;

    public int loaded_gold;
    public int loaded_weapon_mask;
    public int loaded_weapon_count;
    public int loaded_hp;
    public int loaded_stage;
    public int loaded_ammo;
    public int loaded_score;
    public float loaded_time;
    public int loaded_grenade;
    public int loaded_item_count;
    public List<int> loaded_item;

    public void ReadALL()
    {
        loaded_gold = PlayerPrefs.GetInt(GOLD, 1000);
        loaded_weapon_mask = PlayerPrefs.GetInt(WEAPON, 0);
        loaded_weapon_count = PlayerPrefs.GetInt(WEAPONCOUNT, 3);
        loaded_hp = PlayerPrefs.GetInt(HP, 100);
        loaded_stage = PlayerPrefs.GetInt(STAGE, 0);
        loaded_ammo = PlayerPrefs.GetInt(AMMO, 0);
        loaded_score = PlayerPrefs.GetInt(SCORE, 0);
        loaded_time = PlayerPrefs.GetFloat(TIME, 0);
        loaded_grenade = PlayerPrefs.GetInt(GRENADE, 0);
        loaded_item_count = PlayerPrefs.GetInt(INVEN_COUNT, 0);
        for(int i = 0; i < loaded_item_count; i++)
        {
            int temp = PlayerPrefs.GetInt(INVEN_INDEX_PRE+i, -1);
            if(temp > -1)
            {
                loaded_item.Add(temp);
            }
        }

    }
    public void WriteALL()
    {
        player = Player.instance;//GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameManager.instance;//GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (player == null) return;
        if (gameManager == null) return;

        PlayerPrefs.SetInt(GOLD, player.coin);

        int weapon_mask = 0;
        for(int i = 0; i < player.hasWeapons.Length; i++)
        {
            if (player.hasWeapons[i])
            {
                weapon_mask += (int)Mathf.Pow(2, i);
            }
        }
        List<InventItem> items = Inventory.instance.items;
        int item_count =0;
        for (int i = 0; i < items.Count; i++)
        {
            for(int j=0;j<ItemDatabase.instance.itemDB.Count; j++)
            {
                Debug.Log("Item Save Compair) inven item name :" + items[i].itemName + " / " + "DB item name :" + ItemDatabase.instance.itemDB[j].itemName);
                if(items[i].itemName == ItemDatabase.instance.itemDB[j].itemName)
                {
                    item_count++;
                    PlayerPrefs.SetInt(INVEN_INDEX_PRE + i, j);
                }
            }
        }
        PlayerPrefs.SetInt(INVEN_COUNT, item_count);
        PlayerPrefs.SetInt(WEAPON,weapon_mask);
        PlayerPrefs.SetInt(WEAPONCOUNT, player.hasWeapons.Length);
        PlayerPrefs.SetInt(HP,player.health);
        PlayerPrefs.SetInt(STAGE,gameManager.stage);
        PlayerPrefs.SetInt(AMMO,player.ammo);
        PlayerPrefs.SetInt(SCORE,player.score);
        PlayerPrefs.SetFloat(TIME,gameManager.playTime);
        PlayerPrefs.SetInt(GRENADE,player.hasGrenades);
        PlayerPrefs.Save();
    }

    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
    public void AttachDataToPlayer()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        if (player == null) return ;
        if (gameManager == null) return ;
        player.coin = loaded_gold;
        player.health = loaded_hp;
        gameManager.stage = loaded_stage;
        player.ammo = loaded_ammo;
        player.score = loaded_score;
        gameManager.playTime = loaded_time;
        player.hasGrenades = loaded_grenade;

        int masktemp = loaded_weapon_mask;
        int i = 0;
        while (loaded_weapon_count>i)
        {
            if ((masktemp & 1)>0)
            {
                player.hasWeapons[i] = true;
            }
            else
            {
                player.hasWeapons[i] = false;
            }
            masktemp = masktemp >> 1;
            i++;
        }
        
        StartCoroutine("waitforInven");

    }
    // Update is called once per frame
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    IEnumerator waitforInven()
    {
        
        for (int i = 0; i < loaded_item.Count; i++)
        {
            while (Inventory.instance.AddItem(ItemDatabase.instance.itemDB[loaded_item[i]]) == false)
            {
                yield return new WaitForSeconds(0.01f);
            }
            if (loaded_item.Count > Inventory.instance.SlotCnt)
            {
                Inventory.instance.SlotCnt = (loaded_item.Count + (4 - loaded_item.Count % 4));
            }
        }
    }
}
