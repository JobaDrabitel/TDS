using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public abstract float AttackRange { get; }
    public abstract int AttackDamage { get; }
    public abstract float AttackDelay { get; }
    public abstract bool ReadyToAttack { get; set; }
    public override void Attack(Transform[] firepoint)
    {
        MeleeAttack(firepoint, AttackRange, AttackDamage, AttackDelay);
    }
    public virtual void MeleeAttack(Transform[] firepoint, float range, int damage, float delay)
    {
        if (ReadyToAttack)
        {
            Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(firepoint[0].position, range);
            if (unitsInRange.Length > 0)
                foreach (Collider2D unit in unitsInRange)
                {
                    if (unit.GetComponent<Unit>() != null && gameObject.transform.parent.gameObject != unit.gameObject)
                    {
                        unit.GetComponent<Unit>().Die();
                        ReadyToAttack = false;
                        Debug.Log("Õ€¿");
                    }
                }
            StartCoroutine(DelayAttack(delay));
        }
        else
            return;
        
    }
    public IEnumerator DelayAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReadyToAttack = true;
    }
}
