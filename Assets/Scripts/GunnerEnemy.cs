using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

public class GunnerEnemy : Unit, IKillable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int _health = 100;
    [SerializeField] private Weapon _gun;
    [SerializeField] private Transform[] aim;
    private float visionRange = 50f;
    private int _bullets = 10;
    private Transform _target;
    private AIDestinationSetter AI;
    [SerializeField] private AIPath _aiPath;
    private float _speed = 5f;
    private Vector2 _lookDirection;


    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        AI = gameObject.GetComponent<AIDestinationSetter>();
        _target = AI.target;
     }
    override public void TakeDamage(int damage)
    {
        this._health -= damage;
        Debug.Log(_health);
        if (_health <= 0)
            Kill();

    }
    override public void Kill()
    {
        PointCounter.AddPoints(Bullet.Multiplier);
        Debug.Log(PlayerData.levelPoints);
        Debug.Log("Я маслину поймал!");
        Destroy(gameObject);
    }

    public override void Move(float movementspeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, visionRange).GetComponent<Player>()!= null)
        {
            _target = collision.transform;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, visionRange);
        foreach (Collider2D collider2D in collider)
            if (collider2D.gameObject.GetComponent<Player>() != null)
               _gun.Attack(aim);
        if (_gun.Ammo <= 0)
           _bullets = _gun.GetComponent<Gun>().Reload(_bullets);
    }
    private void FaceDirection()
    {
        _lookDirection = _aiPath.desiredVelocity;
        //transform.right = _lookDirection;
        //aim.right = _lookDirection;
        float lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = lookAngle;
    }
    private void Update()
    {
        if (_target != null)
            FaceDirection();
    }

}
