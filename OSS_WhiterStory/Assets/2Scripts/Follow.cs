using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        target = Player.instance.transform;
        transform.position = target.position + offset;
    }
}
