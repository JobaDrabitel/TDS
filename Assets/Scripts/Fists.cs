using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fists : MeleeWeapon
{
    [SerializeField] private Sprite spriteEquiped;
    [SerializeField] private Sprite spriteOnGround;
    private SpriteRenderer _spriteRenderer;
    private GameObject _target;
    private float _attackRange = 0.5f;
    private int _attackDamage = 100;
    private float _attackDelay = 1.5f;
    private bool _readyToAttack = true;
    private readonly int _ammo = 1;
   private float _attackDuration = 0.2f;
    public override float AttackDuration => _attackDuration; 

    public override float AttackRange => _attackRange;

    public override int Ammo => _ammo;
    public override int AttackDamage => _attackDamage;

    public override float AttackDelay => _attackDelay;

    public override bool ReadyToAttack { get => _readyToAttack; set => _readyToAttack = value; }

    public override Animator Animator => throw new System.NotImplementedException();


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }
    public override Vector3 GetOffset(Transform transform)
    {
        Vector3 offset = transform.up * 0.5f; // вычисляем вектор смещения
        offset += transform.right * 0.4f;
        return offset;
    }
    public override void SetSprite()
    {
        if (GetComponentInParent<Enemy>() || GetComponentInParent<Player>() != null)
            _spriteRenderer.sprite = spriteEquiped;
        else
            _spriteRenderer.sprite = spriteOnGround;

    }
    public override void MeleeAttack(Transform[] firepoint, float range, int damage, float delay)
    {
        if (ReadyToAttack)
        {
            Collider2D[] unitsInRange = Physics2D.OverlapCircleAll(firepoint[0].position, range);
            if (unitsInRange.Length > 0)
                foreach (Collider2D unit in unitsInRange)
                {
                    if (unit.GetComponent<StunnedUnit>() != null && gameObject.transform.parent.gameObject != unit.gameObject)
                    {
                        unit.GetComponent<StunnedUnit>().SetStunned(15f);
                        ReadyToAttack = false;
                        Debug.Log("НЫА");
                    }
                }
            StartCoroutine(DelayAttack(delay));
        }
        else
            return;
    }
}
