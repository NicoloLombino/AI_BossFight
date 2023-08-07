using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcher : MonoBehaviour
{
    CharacterController cc;
    Animator anim;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    [Header("Equip")]
    [SerializeField]
    private Rifle bow;

    [Header("values")]
    bool canShoot = true;
    public bool isRunning;

    [Header("Stats")]
    public float health = 100;
    public float currentStamina;
    public float currentBowCharge;
    public bool isReceivingDamage;

    [Header("Personalize")]
    public float staminaMax = 5;
    public float maxBowCharge = 5;
    [SerializeField]
    private float rollHeight;
    [SerializeField]
    private float rollingStaminaUsed;

    private Vector3 playerVelocityY = new Vector3(0,-10,0);
    private Vector3 direction;
    private bool isRolling;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (isReceivingDamage) return;

        ReadMovement();

        if (Input.GetMouseButtonDown(0) && canShoot && !isRunning && !isRolling)
        {
            //StartCoroutine(ShootCoroutine());
            StartShoot();
        }
        else if ((Input.GetMouseButtonUp(0) && !canShoot && !isRunning && currentBowCharge > 1f) || (currentBowCharge >= maxBowCharge))
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canShoot && !isRunning && !isRolling) 
        {
            RunMode(true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) && canShoot && isRunning && !isRolling)
        {
            RunMode(false);
        }
    }

    private void ReadMovement()
    {
        direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        cc.Move(direction.normalized * speed * Time.deltaTime);
        //transform.LookAt(transform.position + direction, Vector3.up);
        if(!isRolling)
        {
            anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
            anim.SetFloat("Vertical", Input.GetAxis("Vertical"));
        }
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
        cc.Move(playerVelocityY * Time.deltaTime);
        if(direction.magnitude > 0f && isRunning)
        {
            currentStamina -= Time.deltaTime;
            if(currentStamina <= 0)
            {
                currentStamina = 0; 
                RunMode(false);
            }
        }
        else
        {
            currentStamina += Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, staminaMax);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }

    private void StartShoot()
    {
        canShoot = false;
        anim.SetBool("isAim", true);
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
    }

    private IEnumerator BowChargeCoroutine()
    {
        while (!canShoot)
        {
            currentBowCharge += Time.deltaTime;
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
    }

    public void ReceiveDamage(int damage, float timerToDisable)
    {
        health -= damage;
        anim.SetTrigger("isHurt");
        StartCoroutine(ReceiveDamageCoroutine(timerToDisable));
    }

    public IEnumerator ReceiveDamageCoroutine(float disableTimer)
    {
        isReceivingDamage = true;
        yield return new WaitForSecondsRealtime(disableTimer);
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        isReceivingDamage = false;
    }

    private void Roll()
    {
        if (currentStamina <= 0 || !canShoot || isRolling)
            return;

        Physics.IgnoreLayerCollision(11, 12, true);
        currentStamina -= rollingStaminaUsed;
        currentStamina = Mathf.Min(currentStamina, 0);
        isRolling = true;
        transform.forward = direction;
        playerVelocityY.y = Mathf.Sqrt(rollHeight * -1.0f * -10);
        anim.SetTrigger("Roll");
        StartCoroutine(EndRolling());
    }

    private IEnumerator EndRolling()
    {
        playerVelocityY = new Vector3(0, -10, 0);
        yield return new WaitForSecondsRealtime(1f);
        Physics.IgnoreLayerCollision(11, 12, false);      
        yield return new WaitForSecondsRealtime(1.5f);
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        isRolling = false;
    }
}

