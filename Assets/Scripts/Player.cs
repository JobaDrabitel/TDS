using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public UnityEvent PickUpWeaponEvent;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] PlayerData playerData;
    private Vector2 movement;
    [SerializeField] private Camera cam;
    [SerializeField] private Joystick joystick;
    public static bool joystickDown = false;
    private int _bulletsAmount = 36;
    private int _timeSlowPoints = 100;
    private float _interctionRadius = 2f;
    private float _speed = 3f;
    private float _attackRange;
    public float AttackRange => _attackRange;
    [SerializeField] private Transform[] attackPoints;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private Weapon _currentWeapon;
    public int TimeSlowPoints { get => _timeSlowPoints; }
    public Transform[] AttackPoint { get => attackPoints; }
    public Weapon CurrentWeapon { get => _currentWeapon; }   
    public int Bullets { get => _bulletsAmount; }

    private void Start()
    {
        playerUI.SetTimeSlowPoints(TimeSlowPoints);
        _attackRange = _currentWeapon.GetComponent<MeleeWeapon>().AttackRange;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        PickUpWeaponEvent.AddListener(playerUI.SetPlayerWeapon);
    }
    private void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        if (joystickDown)
            Move(_speed);
        LookDirection();
    }
    public void LookDirection()
    {
        Vector2 lookDirection = joystick.Direction; /*mousePosition - rb.position;*/
        float lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rb.rotation = lookAngle;
    }
    public override void Move(float movementSpeed)
    {

        _rb.MovePosition(_rb.position + movement * movementSpeed * Time.fixedDeltaTime);

    }
    public override void Die()
    {
        gameObject.SetActive(false);
        Debug.Log("Я не хочу умирать мама");
    }
    public void OnReloadButtonClick()
    {
        if (_currentWeapon.GetComponent<Gun>() != null)
        {
            if (_bulletsAmount > 0)
            {
                _bulletsAmount = _currentWeapon.GetComponent<Gun>().Reload(_bulletsAmount);
                playerUI.SetBullets();
            }
            else
                Debug.Log("No ammo at all!");
        }
    }
    public void OnShootButton()
    {
        _currentWeapon.Attack(attackPoints);
        playerUI.SetBullets();
    }
    public void SetWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _attackRange = _currentWeapon.GetComponent<MeleeWeapon>().AttackRange;
        playerUI.SetBullets();
    }
    public void DecreaseTimeSlowPoints()
    {
        _timeSlowPoints--;
        playerUI.SetTimeSlowBar();
        playerUI.SetTimeSlowPoints(_timeSlowPoints);
    }
    public void IncreaseTimeSlowPoints(int value)
    {
        _timeSlowPoints+=value;
        playerUI.SetTimeSlowBar();
    }

    private void PickUpWeapon()
    { 
        if (GetWeaponForPick() != null)
        {
            Weapon weaponForDrop = null;
            if (_currentWeapon != null)
                weaponForDrop = _currentWeapon;   
            _currentWeapon = GetWeaponForPick();
            _currentWeapon.SetParentForWeapon(_currentWeapon.gameObject, transform);
            if (weaponForDrop != null)
                  weaponForDrop.SetNoParentForWeapon(weaponForDrop.gameObject);
            PickUpWeaponEvent.Invoke();
        }
    }
    private Weapon GetWeaponForPick()
    {
        GameObject weapon = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _interctionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<Weapon>() != null)
            {
                if (collider.GetComponentInParent<Enemy>() == null && collider.GetComponentInParent<Player>() == null)
                    weapon = collider.gameObject;
                else
                    continue;
                if (weapon != null)
                    return weapon.GetComponent<Weapon>();
            }
        }
         return weapon!=null? weapon.GetComponent<Weapon>():null;
    }
   

    public void OnPickUpButtonClick() => PickUpWeapon();
    public void OnSaveButtonClick() => SaveManagerController.SavePlayer(playerData);
    public void OnLoadButtonClick() => playerData.LoadPlayer();
    public void OnThrowButtonCLick() => ThrowWeapon();
    public void ThrowWeapon()
    {
        float force = 10f;
        GameObject weapon = _currentWeapon.gameObject;
        Transform weaponSpawnPoint = attackPoints[0];
        weapon.transform.parent = null;
        weapon.GetComponent<Collider2D>().enabled = true;
        weapon.GetComponent<Collider2D>().isTrigger = false;
        Rigidbody2D rb = weapon.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.forward * force, ForceMode2D.Impulse);
        StartCoroutine(Rotation(rb));
    }
    private IEnumerator Rotation(Rigidbody2D rb)
    {
        float rotation = 50f;
        while (true)
        {
            rb.transform.Rotate(Vector3.up, rotation * Time.deltaTime);
            yield return null;
        }
    }

}
