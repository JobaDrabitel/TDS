using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private static float _bulletSpeed = 10f;
    private Rigidbody2D rb;
    private Vector3 lastVelocity;
    private static bool _isPlayer = false;
    private  int multiplier = 1;
    public int Multiplier { get => multiplier;}

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
        if (collider.gameObject.GetComponent<Unit>() != null)
        {
            collider.gameObject.GetComponent<Unit>().Die();
            Destroy(gameObject);
            if (_isPlayer)
                PlayerData.AddPoints(multiplier);
        }
        else if (collider.gameObject.GetComponent<Weapon>() != null && collider.gameObject.GetComponentInParent<Unit>() != null)
        {
            collider.gameObject.transform.parent.GetComponent<Unit>().Die();
            Destroy(gameObject);
            if (_isPlayer)
                PlayerData.AddPoints(multiplier);

        }
            if (collider.gameObject.GetComponent<Bullet>() !=null || (collider.gameObject.CompareTag("Obstacle")))
            Ricochet(collider);
        if (collider.gameObject.GetComponent<LootBox>()!= null)
        {
            collider.gameObject.GetComponent<LootBox>().Break();
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        lastVelocity = rb.velocity;
    }
    public void Ricochet(Collision2D obstacle)
    {
        var temporalSpeed = lastVelocity.magnitude;
        Vector2 obstacleNormal = obstacle.contacts[0].normal;
        Vector2 moveDirection = Vector2.Reflect(lastVelocity.normalized, obstacleNormal);
        moveDirection = moveDirection.normalized;
        rb.velocity = moveDirection * Mathf.Max(temporalSpeed, 0f);
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        lastVelocity = rb.velocity;
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
        lastVelocity = rb.velocity;
        _isPlayer = isPlayer;
        Debug.Log(_isPlayer);
    }
}
