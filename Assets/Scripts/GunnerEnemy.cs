using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerEnemy : Unit, IKillable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int _health = 100;
    [SerializeField] private GameObject _gun;
    protected Bullet _bullet;

    override public void TakeDamage(int damage)
    {
        this._health -= damage;
        Debug.Log(_health);
        if (_health <= 0)
            Kill();

    }
    override public void Kill()
    {
        PointCounter.AddPoints(_bullet.multiplier);
        Debug.Log(PlayerData.levelPoints);
        Debug.Log("Я маслину поймал!");
        Destroy(gameObject);
    }

    public override void Move(float movementspeed)
    {
        
    }
}
