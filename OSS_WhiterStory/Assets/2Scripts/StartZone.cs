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
            
            SceneManager.LoadScene((GameManager.instance.stage+1)+"_Stage"+ (GameManager.instance.stage + 1));
            Player.instance.transform.position = Vector3.up * 0.8f;
        }
    }
}
