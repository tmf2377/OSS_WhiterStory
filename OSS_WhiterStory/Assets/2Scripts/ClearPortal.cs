using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearPortal : MonoBehaviour
{
    public GameManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LoadScene.is_clear = true;
            SceneManager.LoadScene("0_StartStage");
            Player.instance.transform.position = Vector3.up * 0.8f;
        }
    }
}
