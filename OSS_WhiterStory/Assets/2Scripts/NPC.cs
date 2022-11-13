using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Player player;
    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if (player.score == 0)
                Debug.Log("대화창: 몬스터 10마리를 잡아와라");

            else if (player.score == 10)
                Debug.Log("대화창: 다음 스테이지로");
            else
                Debug.Log("대화창: 더 잡아라");
        }
    }
}
