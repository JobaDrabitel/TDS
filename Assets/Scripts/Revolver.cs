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
    [SerializeField] private CircleCollider2D _shootSoundArea;

    public override int Ammo => _bulletsInClip;

    public override Transform FirePoint { get => _firePoint; set => _firePoint = value; }

    public override int MagasinSize => _magasinSize;

    private float _soundRadius = 30f;
    public override float ShootSoundRadius => _soundRadius;

    private void Start()
    {
        _bullet = bulletPrefab.GetComponent<Bullet>();
        _shootSoundArea.gameObject.SetActive(false);
        _shootSoundArea.radius = _soundRadius;
    }

    public override void Shoot(Transform firePoint)
    {
        if (_bulletsInClip > 0)
        {
            _bullet.BulletSpawn(bulletPrefab, firePoint, false);
            Debug.Log("���!");
            StartCoroutine(CreateShootSound());
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
    private IEnumerator CreateShootSound()
    {
        _shootSoundArea.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _shootSoundArea.gameObject.SetActive(false);
    }
}
