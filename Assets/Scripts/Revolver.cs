using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject bulletPrefab;
    private int _bulletsInClip = 6;
    private int _magasinSize = 6;
    private Bullet _bullet;
    private Transform _firePoint;

    public override int Ammo => _bulletsInClip;

    public override Transform FirePoint { get => _firePoint; set => _firePoint = value; }

    public override int MagasinSize => _magasinSize;

    private void Start()
    {
       _bullet = bulletPrefab.GetComponent<Bullet>();
    }

    public override void Shoot(Transform firePoint)
    {
        if (_bulletsInClip > 0)
        {
            _bullet.BulletSpawn(bulletPrefab, firePoint, false);
            Debug.Log("Бам!");
            _bulletsInClip--;
        }
        else
        {
            Debug.Log("No ammo!");
        }
    }

    public override void AddAmmo(int value)
    {
        _bulletsInClip+=value;
    }
}
