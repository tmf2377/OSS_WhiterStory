﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;
    private void Awake()
    {
        instance = this;
    }

    public List<InventItem> itemDB = new List<InventItem>();
    [Space(20)]
    public GameObject fieldItemPrefab1;
    public GameObject fieldItemPrefab2;
    public GameObject fieldItemPrefab3;
    public Vector3[]pos;

    private void Start() // 아이템 랜덤 생성
    {
        /*for(int i = 0; i < pos.Length; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 3)]);
        }*/
    }
}
