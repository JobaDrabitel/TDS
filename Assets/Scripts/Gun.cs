using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : Weapon, IShootable
{
    public abstract Transform[] FirePoint { get; set; }
    public abstract int MagasinSize { get; }
    public abstract float ShootSoundRadius { get; }

    public abstract void Shoot();

    public abstract void AddAmmo(int value);
    public override void Attack()
    {
        Shoot();
    }
    public virtual int Reload(int bullets)
    {
        if (bullets >= MagasinSize - Ammo)
        {
            AddAmmo(MagasinSize - Ammo);
            bullets -= MagasinSize - Ammo;
        }
        else
        {
            AddAmmo(bullets);
            bullets = 0;
        }
        Debug.Log("Reloading!");
        return bullets;
    }

}
