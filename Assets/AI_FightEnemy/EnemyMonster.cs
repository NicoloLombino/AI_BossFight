using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMonster : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    public Animator animatorBase;
    public Animator animatorRage;

    [SerializeField]
    public Transform target;

    public bool hasTarget;
    public bool isTargetInRange;

    [Header("Stats")]
    [SerializeField]
    private float maxHealth;
    public float currentHealth;
    public bool hasRageMode;
    public int rageValue;
    public bool shouldMove;
    public Transform positionToReach;
    public bool isAttacking;

    [Header("Personalize")]
    [SerializeField]
    private float percentageToRunAway;
    public float healthToRunAway;
    public float attackRange;
    public int maxRageValue;
    public int rageValueIncrementWhenHit;
    [SerializeField]
    private float MaxRageTimer;
    [SerializeField]
    private GameObject rageLight;
    [SerializeField]
    private Material material;


    private float rageTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        healthToRunAway = maxHealth * (percentageToRunAway / 100);
        anim = animatorBase;
    }

    void Update()
    {
        if(hasRageMode)
        {
            rageTimer += Time.deltaTime;
            if(rageTimer >= MaxRageTimer)
            {
                hasRageMode = false;
                rageValue = 0;
                rageTimer = 0;
                rageValueIncrementWhenHit += 2;
                material.color = Color.white;
                rageLight.SetActive(false);
                anim = animatorBase;
            }
        }
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if(!hasRageMode)
        {
            rageValue += rageValueIncrementWhenHit;
            rageValue = Mathf.Min(rageValue, maxRageValue);
            if(rageValue >= maxRageValue)
            {
                hasRageMode = true;
                rageLight.SetActive(true);
                material.color = Color.red;
                anim = animatorRage;
            }
        }
    }

    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    public void ChargeAttack(Vector3 position)
    {
        StartCoroutine(ChargeAttackCoroutine(position));
    }

    private IEnumerator ChargeAttackCoroutine(Vector3 position)
    {
        LookAtTarget();
        isAttacking = true;
        agent.SetDestination(position);
        while(Vector3.Distance(transform.position, position) > 2)
        {
            agent.transform.position += transform.forward * agent.speed * Time.deltaTime;
            Debug.Log("coro = " +  Vector3.Distance(transform.position, position));
            yield return null;
        }
        isAttacking = false;
    }

    public void JumpAway()
    {
        StartCoroutine(JumpAwayCoroutine());
    }

    private IEnumerator JumpAwayCoroutine()
    {
        float percentage = 0;
        while (percentage < 1)
        {
            percentage += Time.deltaTime / 2;
            Debug.Log("percentage =" + percentage);
            agent.transform.position = Vector3.Lerp(agent.transform.position, agent.transform.position + transform.up * 5, percentage);
            yield return null;
        }
        transform.position = new Vector3(230, 0, 0);
        hasTarget = false;
        hasRageMode = false;
    }
}
