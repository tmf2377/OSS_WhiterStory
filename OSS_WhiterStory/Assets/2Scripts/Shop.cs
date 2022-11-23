using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;
    public Text talkText;
    Player enterPlayer;
    // Start is called before the first frame update

    
    public void Enter(Player player)
    {
        if (uiGroup == null)
        {
            if (GameObject.Find("Item Shop").GetComponentInChildren<Shop>() == this)
            {
                uiGroup = GameObject.Find("Item Shop Group").GetComponent<RectTransform>();

                if (talkText == null)
                {
                    for (int i = 0; i < uiGroup.childCount; i++)
                    {
                        if (uiGroup.GetChild(i).name == "Talk Text")
                        {
                            talkText = uiGroup.GetChild(i).GetComponent<Text>();
                        }
                    }
                }
            }
            else if (GameObject.Find("Weapon Shop").GetComponentInChildren<Shop>() == this)
            {
                uiGroup = GameObject.Find("Weapon Shop Group").GetComponent<RectTransform>();

                if (talkText == null)
                {
                    for (int i = 0; i < uiGroup.childCount; i++)
                    {
                        if (uiGroup.GetChild(i).name == "Talk Text")
                        {
                            talkText = uiGroup.GetChild(i).GetComponent<Text>();
                        }
                    }
                }
            }
        }
        enterPlayer = player;
        Debug.Log("a");
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    public void Exit()
    {
        anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down *1000;
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }
        enterPlayer.coin -= price;
        Vector3 ranVec =  Vector3.right * Random.Range(-3,3) + Vector3.forward * Random.Range(-3,3);
        Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);
    }
    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2.0f);
        talkText.text = talkData[0];
    }
}
