using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Gun
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Sprite spriteEquiped;
    [SerializeField] private Sprite spriteOnGround;
    private SpriteRenderer _spriteRenderer;
    private int _bulletsInClip = 6;
    private int _magasinSize = 6;
    private bool _readyForShoot = true;
    private float _shootDelay = 2f;
    private Bullet _bullet;
    [SerializeField] private CircleCollider2D _shootSoundArea;

    public override int Ammo => _bulletsInClip;


    public override int MagasinSize => _magasinSize;

    private float _soundRadius = 30f;
    public override float ShootSoundRadius => _soundRadius;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _bullet = bulletPrefab.GetComponent<Bullet>();
        _shootSoundArea.gameObject.SetActive(false);
        _shootSoundArea.radius = _soundRadius;
        SetSprite();
    }
    public override void Shoot(Transform[] firepoint)
    {
        if (_bulletsInClip > 0)
        {
            if (_readyForShoot)
            {
                _bullet.BulletSpawn(bulletPrefab, firepoint[0], false);
                _bulletsInClip--;
                Debug.Log("Бам!");
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

    public override Vector3 GetOffset(Transform transform)
    {
        Vector3 offset = transform.up * 0.7f; // вычисляем вектор смещения
        offset += transform.right * 0.25f;
        return offset;
    }
    public override void SetSprite()
    {
        if (GetComponentInParent<Enemy>() || GetComponentInParent<Player>() != null)
            _spriteRenderer.sprite = spriteEquiped;
        else
             _spriteRenderer.sprite = spriteOnGround;

    }
}
