using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public abstract int AttackRange { get; }
    public override void Attack(Transform attackPoint)
    {
        MeleeAttack(AttackRange);
    }
    public abstract void MeleeAttack(int value);
}
