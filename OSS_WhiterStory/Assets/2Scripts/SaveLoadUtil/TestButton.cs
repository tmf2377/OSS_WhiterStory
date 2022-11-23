using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    SaveManager saveManager;
    public void SAVE()
    {
        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();
        saveManager.WriteALL();
    }
}
