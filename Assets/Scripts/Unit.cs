using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour, IKillable, IMovable
{

    virtual public void Die()
    {
        Destroy(gameObject);
    }

    public abstract void Move(float movementspeed);
}
