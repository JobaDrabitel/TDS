using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : Unit
{
    private LayerMask _layerMask;
    public abstract float VisionRange { get; }
    public abstract float AttackRange { get;  }
    public virtual Transform GetTarget(Collider2D[] collisions)
    {
        _layerMask = LayerMask.GetMask("Player", "Obstacle");
        foreach (Collider2D collision in collisions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (collision.transform.position - transform.position).normalized, VisionRange, _layerMask);
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Player>() != null)
                return collision.transform;
        }
        return null;
    }
    public virtual void AimToTarget(Transform target)
    {
        _layerMask = LayerMask.GetMask("Player", "Obstacle");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (target.position - transform.position).normalized, VisionRange, _layerMask);
        if (hit.collider != null && hit.collider.gameObject.GetComponent<Player>() != null)
        {
            LookAtTarget();
            AttackTarget();
        }
        else
            LookForward();

    }
    public abstract void LookAtTarget();
    public abstract void LookForward();
    public abstract void AttackTarget();

}
