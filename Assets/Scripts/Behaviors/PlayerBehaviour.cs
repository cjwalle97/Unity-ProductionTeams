using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    #region //MEMEBER VARIABLES
    public Player _player;
    public float PlayerHealth = 100;
    public float PlayerMaxHealth = 100;
    public float PlayerDamage = 25;
    public GameObject Ammunition;
    public float MovementSpeed = 20;
    public float LookSpeed = 10;
    public float BulletSpeed = 20;
    private Transform _bulletspawn;
    private Animator _animator;
    private float deathCounter = 0;
    private int deathCount = 0;
    
    [System.Serializable]
    public class OnPlayerHealthChange : UnityEvent<float> { }
    public OnPlayerHealthChange onPlayerHealthChange;
    #endregion

    Vector3 LookAround()
    {
        //RIGHT JOYSTICK CONTROLL
        var _hori = Input.GetAxis("HorizontalRightJoy");
        var _vert = Input.GetAxis("VerticalRightJoy");

        //ARROW CONTROLLS
        //var _hori = Input.GetAxis("HorizontalArrow");
        //var _vert = Input.GetAxis("VerticalArrow");

        _animator.SetFloat("AimMovement", Mathf.Abs(_hori) + Mathf.Abs(_vert));

        Vector3 _direction = new Vector3(_hori, 0, _vert);
        return _direction.normalized;
    }

    Vector3 MoveAround()
    {
        //LEFT JOYSTICK CONTROLL
        var h = Input.GetAxis("HorizontalLeftJoy");
        var v = Input.GetAxis("VerticalLeftJoy");

        //WSAD CONTROLL
        //var h = Input.GetAxis("Horizontal");
        //var v = Input.GetAxis("Vertical");
        
        _animator.SetFloat("WalkMovement", Mathf.Abs(h) + Mathf.Abs(v));

        Vector3 _direction = new Vector3(h, 0, v);
        return _direction.normalized;
    }
    
    void Shoot()
    {
        _animator.SetBool(AIMSHOOT, true);
    }

    public int AIMSHOOT = Animator.StringToHash("Shooting");
    void Death()
    {
        SceneManager.LoadScene("4.gameover");     
    }

    bool CheckIfAlive()
    {
        if(_player.Health <= 0.0f)
        {
            _player.Alive = false;
            deathCounter += Time.deltaTime;
            return false;
        }
        return true;
    }
    
    bool canshoot, shooting;
    public void ShotFire()
    {
        var _bullet = Instantiate(Ammunition, _bulletspawn.position, _bulletspawn.rotation);
        _bullet.GetComponent<Rigidbody>().velocity += _bulletspawn.forward * BulletSpeed;
        Destroy(_bullet, 2.0f);
    }

    void Awake()
    {
        _player = ScriptableObject.CreateInstance<Player>();
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _bulletspawn = GetComponentInChildren<PlayerBulletSpawnBehaviour>().Spawn;
        _player.MaxHealth = PlayerMaxHealth;
        _player.Health = PlayerHealth;
        _player.Damage = PlayerDamage;
        _player.Alive = true;        
    }
    
    void Update()
    {   
        if(CheckIfAlive() == false)
        {
            if(deathCount <= 0)
            {
                _animator.SetBool("Alive", false);
            }

            deathCount += 1;

            if (deathCounter >= 3.0f)
            {
                Death();
            }
        }

        _animator.SetBool("Alive", _player.Alive);
        onPlayerHealthChange.Invoke(_player.Health);
    }

    void FixedUpdate()
    {
        if (_player.Alive == true)
        {
            var _rightTrigger = Input.GetAxis("JoyFire");

            var ylimit = transform.position.y;
            if (ylimit > 0.0f)
            {
                Vector3 sitdown = transform.position;
                sitdown.y = 0.0f;
                transform.position = sitdown;
            }

            _animator.SetBool("Shooting", shooting);
            
            canshoot = _rightTrigger >= .5f;
            if (canshoot && !shooting)
            {
                Shoot();
            }

            if (LookAround() != Vector3.zero && canshoot)
            {
                Shoot();
            }

            if (MoveAround() != Vector3.zero) //CHECK IF THE PLAYER IS MOVING
            {
                if (LookAround() == Vector3.zero) //CHECK IF THE PLAYER IS AIMING
                {
                    MovementSpeed = 20f;
                    transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(MoveAround()), Time.deltaTime * LookSpeed);
                }
                else
                    MovementSpeed = 2f;
            }

            if (LookAround() != Vector3.zero) // CHECKING IF THE ARROW INPUTS ARE ZERO, WILL LOCK PLAYER ROTATION ON RELEASE
            {
                transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(LookAround()), Time.deltaTime * LookSpeed);
            }

            transform.localPosition += MoveAround() * MovementSpeed * Time.deltaTime;            
        }
    }
}