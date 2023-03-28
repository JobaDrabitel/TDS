using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSlayer : MeleeWeapon
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
    [SerializeField] private Animator _animator;
    public override float AttackRange => _attackRange;

    public override int Ammo => _ammo;
    public override int AttackDamage => _attackDamage;

    public override float AttackDelay => _attackDelay;

    public override bool ReadyToAttack { get => _readyToAttack; set => _readyToAttack = value; }

    public override Animator Animator => _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }
    public override Vector3 GetOffset(Transform transform)
    {
        Vector3 offset = transform.up * 0.1f; // вычисляем вектор смещения
        offset += transform.right * 0.1f;
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
        base.MeleeAttack(firepoint, range, damage, delay);
        _animator.Play("GUTSANDBLOODS");
        _animator.SetBool("isAttaking", true);
        _animator.SetBool("isAttaking", false);
        StartCoroutine(AnimationDelay());
    }
    private IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(0.4f);
    }
}
