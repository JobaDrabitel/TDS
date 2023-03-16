using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private static float _bulletSpeed = 4f;
    [SerializeField] public static int _bulletDamage = 40;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    private static bool _isPlayer = false;
    public int multiplier = 1;

    public void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Destroy(gameObject,5);
    }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Unit>().TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<Unit>().TakeDamage(_bulletDamage);
            Destroy(gameObject);
        }
        if (collider.gameObject.CompareTag("Bullet") || (collider.gameObject.CompareTag("Obstacle")))
        {
            Ricochet(collider, rb);
        }
    }
    private void Update()
    {
         lastVelocity = rb.velocity;
    }
    public void Ricochet(Collision2D obstacle, Rigidbody2D rb)
    {
        var temporalSpeed = lastVelocity.magnitude;
        Vector2 obstacleNormal = obstacle.contacts[0].normal;
        Vector2 moveDirection = Vector2.Reflect(lastVelocity.normalized, obstacleNormal);
        rb.velocity = moveDirection * Mathf.Max(temporalSpeed, 0f);
        if (_isPlayer)
        {
            multiplier += 1;
            Debug.Log(multiplier);
        }

    }
    public void BulletSpawn(GameObject bulletPrefab, Transform firePoint, bool isPlayer)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * _bulletSpeed;
        _isPlayer = isPlayer;
        Debug.Log(_isPlayer);
    }
}
