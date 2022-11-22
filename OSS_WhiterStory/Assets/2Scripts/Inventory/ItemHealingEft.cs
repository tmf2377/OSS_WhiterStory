using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemEft/Consumable/Health")]
public class ItemHealingEft : ItemEffect
{
    public Player player;
    public int healingPoint = 0;
    public override bool ExecuteRole()
    {
        Debug.Log("PlayerHp Add:" + healingPoint);

        player = GameObject.Find("Player").GetComponent<Player>();
        player.health += healingPoint;
        if (player.health > player.maxHealth)
            player.health = player.maxHealth;

        return true;
    }
}