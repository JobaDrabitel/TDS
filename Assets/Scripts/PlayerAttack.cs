using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour, IMeleeAttack
{
    [SerializeField] Player playerData;
    public void Attack()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(playerData.AttackPoint.position, playerData.attackRange, playerData.defaultLayer);
        if (enemiesInRange.Length > 0)
        foreach (Collider2D enemy in enemiesInRange)
            {
                enemy.GetComponent<Unit>().TakeDamage(playerData.attackDamage);
            }
        
    }
}
