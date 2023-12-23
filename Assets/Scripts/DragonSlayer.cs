using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSlayer : MeleeWeapon
{
	[SerializeField] private Sprite spriteEquiped;
	[SerializeField] private Sprite spriteOnGround;
	[SerializeField] private Sprite attackSprite;
	private SpriteRenderer _spriteRenderer;
	private GameObject _target;
	private float _attackRange = 3f;
	private int _attackDamage = 100;
	private float _attackDelay = 1.5f;
	private bool _readyToAttack = true;
	private readonly int _ammo = 1;
	private float _attackDuration = 0.4f;
	public override float AttackDuration => _attackDuration;
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
		_animator.enabled = false;
		_spriteRenderer = GetComponent<SpriteRenderer>();
		SetSprite();
	}
	public override Vector3 GetOffset(Transform transform)
	{
		Vector3 offset = transform.up * 0.1f; // вычисляем вектор смещения
		offset += transform.right * 0.1f;
		offset -= transform.forward * 1.01f;
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
			Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(gameObject.transform.parent.gameObject.transform.position, range);
			if (targetsInRange.Length > 0)
				foreach (Collider2D target in targetsInRange)
				{
					if (target.GetComponent<Unit>() != null && gameObject.transform.parent.gameObject != target.gameObject && !target.isTrigger)
					{
						target.GetComponent<Unit>().TakeDamage();
						ReadyToAttack = false;
						Debug.Log("НЫА");
					}
					if (target.GetComponent<Bullet>() != null)
					{
						Destroy(target.gameObject);
						ReadyToAttack = false;
					}
					if (target.GetComponent<LootBox>() != null)
						target.GetComponent<LootBox>().Break();
				}
			StartCoroutine(DelayAttack(delay));
		}
		else
			return;
		_animator.enabled = true;
		_animator.Play("GUTSANDBLOODS");
		_animator.SetBool("isAttaking", true);
		_animator.SetBool("isAttaking", false);
		SetAttackSprite();
		StartCoroutine(AnimationDelay());
	}
	public void SetAttackSprite()
	{
		gameObject.GetComponentInParent<Unit>().SetAttackSprite(null, _attackDuration);
		_spriteRenderer.sprite = attackSprite;
	}
	private IEnumerator AnimationDelay()
	{
		yield return new WaitForSeconds(_attackDuration);
		_animator.enabled = false;
		transform.localRotation = Quaternion.identity;
		_spriteRenderer.sprite = spriteEquiped;
	}
}
