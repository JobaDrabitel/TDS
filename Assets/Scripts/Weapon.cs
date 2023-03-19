using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract int Ammo { get; }
    public abstract bool IsEquiped { get; }
    public abstract void Attack();
    public abstract void HighlightWeapon();
    public abstract void SetWeaponEquiped();
}
