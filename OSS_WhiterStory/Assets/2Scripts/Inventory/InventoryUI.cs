﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    public GameObject inventoryPanel;
    bool activeInventory = false;
    bool IsPause;

    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
        inven.onSlotCountChange += SlotChange;
        inven.onChangeItem += RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
        IsPause = false;
    }

    private void SlotChange(int val)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
            if(i < inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void OpenInventory()
    {
        activeInventory = true;
        inventoryPanel.SetActive(activeInventory);
        if (IsPause == false)
        {
            Time.timeScale = 0;
            IsPause = true;
        }
    }

    public void CloseInventory()
    {
        Time.timeScale = 1;
        IsPause = false;
        activeInventory = false;
        inventoryPanel.SetActive(activeInventory);
    }

    public void AddSlot()
    {
        inven.SlotCnt += 4;
    }

    void RedrawSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
