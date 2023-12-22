using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;

public class GunnerEnemy : Enemy, IKillable
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform[] aim;
    private SpriteRenderer _spriteRenderer;
    private Sprite _sprite;
    private float _visionRange = 50f;
    public override float VisionRange => _visionRange;
    private float _attackRange;
    public override float AttackRange => _attackRange;

    public override Sprite Sprite => _sprite;

    public override SpriteRenderer SpriteRenderer => _spriteRenderer;

    private int _bullets = 10;
    private AIDestinationSetter _AI;
    public AIDestinationSetter AI;
    [SerializeField] private AIPath _aiPath;
    private float _speed = 5f;
    private Vector2 _lookDirection;

    private StunnedUnit _stunnedUnit;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sprite = _spriteRenderer.sprite;
        _stunnedUnit = GetComponent<StunnedUnit>();
        _attackRange = _visionRange;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _AI = gameObject.GetComponent<AIDestinationSetter>();
     }
    override public void Die()
    {
        Debug.Log("Я маслину поймал!");
        _weapon.SetNoParentForWeapon(_weapon.gameObject);
        gameObject.SetActive(false);
    }

    public override void Move(float movementspeed)
    {
        if(!_stunnedUnit.IsStunned)
        transform.position = Vector3.MoveTowards(transform.position, _AI.target.transform.position, _speed * Time.deltaTime);
    }
  
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!_stunnedUnit.IsStunned)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _visionRange);
            _AI.target = GetTarget(colliders);
            if (_AI.target != null)
                AimToTarget(_AI.target);
        }
    }
    public override void LookForward()
    {
        _lookDirection = _aiPath.desiredVelocity;
        float lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = lookAngle;
    }

    public override void LookAtTarget()
    {
        _lookDirection = (_AI.target.transform.position - transform.position).normalized;
        float lookAngle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = lookAngle;
    }
    public override void AttackTarget()
    {
        _weapon.Attack(aim);
        if (_weapon.Ammo <= 0)
            _bullets = _weapon.GetComponent<Gun>().Reload(_bullets);
    }
}
