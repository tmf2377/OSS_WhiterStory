﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool isMelee;
    public bool isRock;

    private void OnCollisionEnter(Collision collision)
    {
        if(!isRock && collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject, 3);
        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isMelee && (other.gameObject.tag == "Wall" || other.gameObject.tag == "LowWall"))
        {
            Destroy(gameObject);
        }
    }
}
