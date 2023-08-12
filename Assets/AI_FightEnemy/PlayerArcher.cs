using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerArcher : MonoBehaviour
{
    CharacterController cc;
    Animator anim;
    AudioSource audioSource;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float rollingSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform cameraTransForm;

    [Header("Equip")]
    [SerializeField]
    private Rifle bow;
    [SerializeField]
    private int potionNumber;

    [Header("values")]
    public bool canShoot = true;
    public bool isRunning;
    [SerializeField]
    private int potionHeal;

    [Header("Stats")]
    public float health = 100;
    public float currentStamina;
    public float currentBowCharge;
    public bool isReceivingDamage;

    [Header("Personalize")]
    public float healthMax = 100;
    public float staminaMax = 5;
    public float maxBowCharge = 5;
    [SerializeField]
    private float rollHeight;
    [SerializeField]
    private float rollingStaminaUsed;
    [SerializeField]
    private GameObject healParticles;

    [Header("UI elements")]
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider staminaSlider;
    [SerializeField]
    private Slider bowChargeSlider;
    [SerializeField]
    private TextMeshProUGUI potionText;
    [SerializeField]
    private GameObject deathPanel;
    public GameObject enemyEyeWhenSeePlayer;

    [Header("audio clips")]
    [SerializeField] AudioClip walkClip;
    [SerializeField] AudioClip runClip;
    [SerializeField] AudioClip rollClip;
    [SerializeField] AudioClip bowClip;
    [SerializeField] AudioClip damageClip;

    private Vector3 playerVelocityY = new Vector3(0,-10,0);
    private Vector3 direction;
    private bool isRolling;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (isReceivingDamage) return;

        ReadMovement();

        if (Gamepad.current.buttonNorth.wasPressedThisFrame && canShoot && !isRunning && !isRolling)
        {
            //StartCoroutine(ShootCoroutine());
            StartShoot();
        }
        else if ((Gamepad.current.buttonNorth.wasReleasedThisFrame && !canShoot && !isRunning && currentBowCharge > 1f) || (currentBowCharge >= maxBowCharge))
        {
            if(isReceivingDamage)
            {
                bow.arrowInBow.SetActive(false);
                canShoot = true;
                currentBowCharge = 0;
                return;
            }
            EndShoot();
        }

        if (Gamepad.current.buttonWest.wasPressedThisFrame && canShoot && !isRunning && !isRolling) 
        {
            RunMode(true);
        }
        else if(Gamepad.current.buttonWest.wasPressedThisFrame && canShoot && isRunning && !isRolling)
        {
            RunMode(false);
        }

        if(Gamepad.current.buttonEast.wasPressedThisFrame && canShoot && !isRunning && !isRolling)
        {
            UsePotion();
        }
    }

    private void ReadMovement()
    {
        Vector2 moveIS = Gamepad.current.leftStick.ReadValue().normalized;
        if (!isRolling)
        {
            direction = transform.forward * moveIS.y + transform.right * moveIS.x;
        }
        
        cc.Move(direction.normalized * speed * Time.deltaTime);
        //transform.LookAt(transform.position + direction, Vector3.up);
        if(!isRolling)
        {
            anim.SetFloat("Horizontal", moveIS.x);
            anim.SetFloat("Vertical", moveIS.y);
            
            if(direction.magnitude > 0f)
            {
                if(!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.Stop();
            }
        }

        if(!Input.GetMouseButton(1) && !isRolling)
        {
            Vector2 moveCam = Gamepad.current.rightStick.ReadValue();
            transform.Rotate(Vector3.up, moveCam.x * Time.deltaTime * rotationSpeed);
        }

        cc.Move(playerVelocityY * Time.deltaTime);
        if(direction.magnitude > 0f && isRunning)
        {
            currentStamina -= Time.deltaTime;
            UpdateStaminaBar();
            if(currentStamina <= 0)
            {
                currentStamina = 0; 
                RunMode(false);
            }
        }
        else
        {
            if(!isRolling)
            {
                currentStamina += Time.deltaTime * 2;
                currentStamina = Mathf.Min(currentStamina, staminaMax);
                UpdateStaminaBar();
            }
        }

        if(Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Roll();
        }
    }

    private void StartShoot()
    {
        canShoot = false;
        anim.SetBool("isAim", true);
        audioSource.PlayOneShot(bowClip);
        bow.arrowInBow.SetActive(true);
        StartCoroutine(BowChargeCoroutine());
    }

    private void EndShoot()
    {
        anim.SetBool("isAim", false);
        anim.SetTrigger("Shot");
        bow.Shoot(Mathf.RoundToInt(currentBowCharge * 2));
        bow.arrowInBow.SetActive(false);
        canShoot = true;
        currentBowCharge = 0;
        bowChargeSlider.value = 0;
        StopCoroutine(BowChargeCoroutine());
    }

    private IEnumerator BowChargeCoroutine()
    {
        while (!canShoot)
        {
            currentBowCharge += Time.deltaTime;
            bowChargeSlider.value = currentBowCharge / maxBowCharge;
            yield return null;
        }
    }

    //private IEnumerator ShootCoroutine()
    //{
    //    canShoot = false;
    //    anim.SetBool("isAim", true);
    //    yield return new WaitForSecondsRealtime(0.2f);
    //    bow.arrowInBow.SetActive(true);
    //    while(!Input.GetMouseButtonUp(0) && currentBowCharge < maxBowCharge)
    //    {
    //        currentBowCharge += Time.deltaTime;
    //        yield return null;
    //    }
    //    currentBowCharge = 0;
    //    anim.SetBool("isAim", false);
    //    anim.SetTrigger("Shot");
    //    bow.Shoot(Mathf.RoundToInt(currentBowCharge));
    //    bow.arrowInBow.SetActive(false);
    //    yield return new WaitForSecondsRealtime(0.5f);
    //    canShoot = true;
    //}

    private void RunMode(bool isRun)
    {
        isRunning = isRun;
        anim.SetBool("isRunning", isRun);
        speed = isRun ? speed + 2 : speed - 2;
        audioSource.clip = isRun ? runClip : walkClip;
    }

    public void ReceiveDamage(int damage, float timerToDisable)
    {
        health -= damage;
        UpdateHealthBar();
        audioSource.PlayOneShot(damageClip);
        currentBowCharge = 0;
        StopCoroutine(BowChargeCoroutine());

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            isReceivingDamage = true;
            Physics.IgnoreLayerCollision(11, 12, true);
            gameObject.tag = "Untagged";
            deathPanel.SetActive(true);
            enabled = false;
        }
        else
        {
            anim.SetTrigger("isHurt");
            StartCoroutine(ReceiveDamageCoroutine(timerToDisable));
        }
    }

    public IEnumerator ReceiveDamageCoroutine(float disableTimer)
    {
        isReceivingDamage = true;
        canShoot = true;
        isRunning = false;
        yield return new WaitForSecondsRealtime(disableTimer);
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        anim.SetBool("isAim", false);
        anim.SetBool("isRunning", false);
        //anim.SetFloat("BLend", 1);
        isReceivingDamage = false;
    }

    private void Roll()
    {
        if (currentStamina <= 0 || !canShoot || isRolling)
            return;

        cameraTransForm.GetComponentInParent<CameraParent>().isLocked = true;
        audioSource.PlayOneShot(rollClip);
        Physics.IgnoreLayerCollision(11, 12, true);     
        currentStamina -= rollingStaminaUsed;
        currentStamina = Mathf.Max(currentStamina, 0);       
        transform.forward = direction;
        speed += rollingSpeed;
        isRolling = true;
        //playerVelocityY.y = Mathf.Sqrt(rollHeight * -1.0f * -10);
        anim.SetTrigger("Roll");
        UpdateStaminaBar();
        StartCoroutine(EndRolling());
    }

    private IEnumerator EndRolling()
    {       
        yield return new WaitForSecondsRealtime(0.5f);
        //playerVelocityY = new Vector3(0, -10, 0);
        Physics.IgnoreLayerCollision(11, 12, false);      
        yield return new WaitForSecondsRealtime(1f);
        speed -= rollingSpeed;
        transform.forward = cameraTransForm.forward;
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        isRolling = false;
        cameraTransForm.GetComponentInParent<CameraParent>().isLocked = false;
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = health / healthMax;
    }

    private void UpdateStaminaBar()
    {
        staminaSlider.value = currentStamina / staminaMax;
    }

    private void UsePotion()
    {
        if (potionNumber <= 0) return;

        isReceivingDamage = true;
        potionNumber--;
        potionText.text = potionNumber.ToString();
        health += potionHeal;
        health = Mathf.Min(health, healthMax);
        anim.SetTrigger("Potion");
        healParticles.SetActive(true);
        UpdateHealthBar();
        StartCoroutine(UsingPotionCoroutine());
    }

    private IEnumerator UsingPotionCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        isReceivingDamage = false;
        healParticles.SetActive(false);
    }
}

