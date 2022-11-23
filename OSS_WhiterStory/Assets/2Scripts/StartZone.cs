using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartZone : MonoBehaviour
{
    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (manager.stage)
            {
                case 1: SceneManager.LoadScene("1_Stage1"); break;
                case 2: SceneManager.LoadScene("2_Stage2"); break;
                case 3: SceneManager.LoadScene("3_Stage3"); break;
                default: SceneManager.LoadScene("1_Stage1"); break;
            }
            //SceneManager.LoadScene("1_Stage1");
            Player.instance.transform.position = Vector3.up * 0.8f;
        }
    }
}
