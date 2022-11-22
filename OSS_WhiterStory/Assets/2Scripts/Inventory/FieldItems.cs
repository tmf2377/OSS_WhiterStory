﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public InventItem item;
    //public SpriteRenderer image;
    public Sprite image;
    public GameObject potion;

    public void SetItem(InventItem _item)
    {
        item.itemName = _item.itemName;
        item.itemImage = _item.itemImage;
        item.itemPotion = _item.itemPotion;
        item.itemType = _item.itemType;
        item.efts = _item.efts;

        image = item.itemImage;
       
    }

    public InventItem GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
