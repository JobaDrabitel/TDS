using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Unit : MonoBehaviour, IKillable, IMovable
{ 
    public abstract Sprite Sprite { get; }
    public abstract SpriteRenderer SpriteRenderer { get; }
    virtual public void Die()
    {
       gameObject.SetActive(false);
    }

    public abstract void Move(float movementspeed);
    public virtual void SetAttackSprite(Sprite attackSprite, float duration)
    {
        SpriteRenderer.sprite = attackSprite;
        StartCoroutine(SpriteChangeDelay(duration));
    }
    public virtual IEnumerator SpriteChangeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpriteRenderer.sprite = Sprite;
    }
}
