using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public static Follow instance = null;
    public Transform target;
    public Vector3 offset;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }
    void Update()
    {
        target = Player.instance.transform;
        transform.position = target.position + offset;
    }
}
