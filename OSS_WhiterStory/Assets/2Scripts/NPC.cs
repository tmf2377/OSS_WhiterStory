using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    void npcTargeting()
    {
        float ntargetRadious = 1.5F;
        float ntargetRange = 3f;

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, ntargetRadious,
           transform.forward, ntargetRange,
           LayerMask.GetMask("Player"));

        if (rayHits.Length > 0)
        {
            //대화
        }
    }
    private void FixedUpdate()
    {
        npcTargeting();
    }
}
