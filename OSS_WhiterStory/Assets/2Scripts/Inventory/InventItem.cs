using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}


[System.Serializable]
public class InventItem
{
    public ItemType itemType; 
    public string itemName;
    public Sprite itemImage;
    public GameObject itemPotion;
    public List<ItemEffect>efts;

    public bool Use()
    {
        bool isUsed = false;
        foreach(ItemEffect eft in efts)
        {
            isUsed = eft.ExecuteRole();
        }

        return isUsed;
    }
}
