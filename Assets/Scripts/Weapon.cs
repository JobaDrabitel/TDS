using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract int Ammo { get; }
    public bool IsPlayer { get; private set; }
    public abstract Animator Animator { get; }
    public abstract void Attack(Transform[] firepoint);
    public virtual Weapon SetParentForWeapon(GameObject weapon, Transform transform)
    {
        Quaternion spawnRotation = Quaternion.identity;
        weapon.transform.SetParent(transform);
        Vector3 offset = GetOffset(transform);
        weapon.transform.position = transform.position + offset; // устанавливаем локальную позицию с учетом смещения
        weapon.transform.localRotation = spawnRotation;
        weapon.GetComponent<Collider2D>().isTrigger = false;
        SetSprite();
        IsPlayer = weapon.transform.parent.GetComponent<Player>() != null ? true:false ;
        return weapon.GetComponent<Weapon>();
    }
    public abstract Vector3 GetOffset(Transform transform);
    public virtual void SetNoParentForWeapon(GameObject weapon)
    {
        weapon.transform.parent = null;
        SetSprite();
        weapon.GetComponent<Collider2D>().isTrigger = true;
    }
    public abstract void SetSprite();
}
