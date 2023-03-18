using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private int _bulletsInClip = 6;
    private int _magasinSize = 6;
    private bool _readyForShoot = true;
    private float _shootDelay = 2f;
    private Bullet _bullet;
    [SerializeField] private Transform[] _firePoint = new Transform[1];
    [SerializeField] private CircleCollider2D _shootSoundArea;

    public override int Ammo => _bulletsInClip;

    public override Transform[] FirePoint { get => _firePoint; set => _firePoint = value; }

    public override int MagasinSize => _magasinSize;

    private float _soundRadius = 30f;
    public override float ShootSoundRadius => _soundRadius;

    private void Start()
    {
        _bullet = bulletPrefab.GetComponent<Bullet>();
        _shootSoundArea.gameObject.SetActive(false);
        _shootSoundArea.radius = _soundRadius;
    }

    public override void Shoot()
    {
        if (_bulletsInClip > 0)
        {
            if (_readyForShoot)
            {
                foreach (Transform fire in _firePoint)
                {
                    _bullet.BulletSpawn(bulletPrefab, fire, false);
                    _bulletsInClip--;
                }
                Debug.Log("���!");
                StartCoroutine(CreateShootSound());
                _readyForShoot = false;
                StartCoroutine(ShootDelay()); 
            }
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
    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(_shootDelay);
        _readyForShoot = true;
    }
    public override void HighlightWeapon()
    {
        spriteRenderer.color = Color.white;
    }
}
