using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private static float _bulletSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    private static bool _isPlayer = false;
    private static int multiplier = 1;
    public static int Multiplier { get => multiplier; set => multiplier = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Awake()
    {
        Destroy(gameObject,5);
     }
    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.GetComponent<Enemy>() != null || collider.gameObject.GetComponent<Player>() != null)
        {
            collider.gameObject.GetComponent<Unit>().Die();
            Destroy(gameObject);
        }
        if (collider.gameObject.GetComponent<Bullet>()!=null || (collider.gameObject.CompareTag("Obstacle")))
            Ricochet(collider, rb);
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
