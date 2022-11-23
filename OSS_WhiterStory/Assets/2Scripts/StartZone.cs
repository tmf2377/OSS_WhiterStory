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
            
            SceneManager.LoadScene("1_Stage1");
            Player.instance.transform.position = Vector3.up * 0.8f;
        }
    }
}
