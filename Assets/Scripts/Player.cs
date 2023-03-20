using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public UnityEvent PickUpWeaponEvent;
    private Rigidbody2D _rb;
    [SerializeField] PlayerData playerData;
    private Vector2 movement;
    [SerializeField] private Camera cam;
    [SerializeField] private Joystick joystick;
    public static bool joystickDown = false;
    private int _bulletsAmount = 36;
    private int _timeSlowPoints = 100;
    private int _health = 100;
    private float _interctionRadius = 5f;
    private float _speed = 3f;
    [SerializeField] private Transform[] attackPoints;
    public float attackRange = 0.5f;
    public LayerMask defaultLayer;
    public int attackDamage = 20;
    [SerializeField] private PlayerUI playerUI;
    public int Health => _health;
    [SerializeField] private Weapon _currentWeapon;
    public int TimeSlowPoints { get => _timeSlowPoints; }
    public Transform[] AttackPoint { get => attackPoints; }
    public Weapon CurrentWeapon { get => _currentWeapon; }   
    public int Bullets { get => _bulletsAmount; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        PickUpWeaponEvent.AddListener(playerUI.SetPlayerWeapon);
        PickUpWeaponEvent.AddListener(_currentWeapon.SetWeaponEquiped);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(Bullet._bulletDamage);
            playerData.healthBar.SetHealth(_health);
        }
    }
    public override void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
            Kill();

    }
    public override void Kill()
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
        playerUI.SetBullets();
    }
    public void DecreaseTimeSlowPoints()
    {
        _timeSlowPoints--;
        playerUI.SetTimeSlowBar();
    }
    public void IncreaseTimeSlowPoints(int value)
    {
        _timeSlowPoints+=value;
        playerUI.SetTimeSlowBar();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void PickUpWeapon()
    {
        if (GetWeaponForPick() != null)
        {
            Transform weaponSpawnPoint = gameObject.transform;
            Weapon weapon = _currentWeapon;
            _currentWeapon=null;
            _currentWeapon = SetParentForWeapon(GetWeaponForPick().gameObject);
            PickUpWeaponEvent.Invoke();
            if (weapon != null)
                Instantiate(weapon, weaponSpawnPoint);
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
                weapon = collider.gameObject;
                if (weapon.GetComponentInParent<GunnerEnemy>() != null || weapon.GetComponentInParent<Player>())
                    continue;
                if (weapon != null)
                    return weapon.GetComponent<Weapon>();
            }
        }
         return null;
    }
    public Weapon SetParentForWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0f, 0f, 0.5f);
        weapon.transform.localRotation = Quaternion.identity;
        weapon.GetComponent<Collider2D>().enabled = false;
        return weapon.GetComponent<Weapon>();
    }
    public void OnPickUpButtonClick() => PickUpWeapon();
    public void OnSaveButtonClick() => SaveManagerController.SavePlayer(playerData);
    public void OnLoadButtonClick() => playerData.LoadPlayer();
}
