using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GunnerEnemy : Unit, IKillable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int _health = 100;
    [SerializeField] private Weapon _gun;
    [SerializeField] private Transform aim;
    private float visionRange = 50f;
    protected Bullet _bullet;
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
        PointCounter.AddPoints(_bullet.multiplier);
        Debug.Log(PlayerData.levelPoints);
        Debug.Log("Я маслину поймал!");
        Destroy(gameObject);
    }

    public override void Move(float movementspeed)
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, visionRange).GetComponent<Player>()!= null)
        {
            _target = collision.transform;

        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, visionRange).GetComponent<Player>() != null)
            _gun.Attack(aim);
    }
    private void FaceDirection()
    {
        _lookDirection = _aiPath.desiredVelocity;
        transform.right = _lookDirection;
        aim.right = _lookDirection;
    }
    private void Update()
    {
        if (_target != null)
            FaceDirection();
    }
}
