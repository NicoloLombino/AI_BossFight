using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMonster : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator anim;

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
    public float RageSpeedIncrement = 1f;
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
        material.color = Color.white;
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
                anim.speed = 1;
                RageSpeedIncrement = 1;
                //anim.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.
                //    Instantiate(Resources.Load("Animation/Mutant Animator.controller", typeof(RuntimeAnimatorController)));
            }
        }
    }

    public void ReceiveDamage(int damage)
    {
        currentHealth -= damage;
        if(!hasRageMode)
        {
            rageValue += rageValueIncrementWhenHit + damage;
            rageValue = Mathf.Min(rageValue, maxRageValue);
            if(rageValue >= maxRageValue)
            {
                hasRageMode = true;
                rageLight.SetActive(true);
                material.color = Color.red;
                anim.speed = 2;
                RageSpeedIncrement = 1.5f;
                //anim.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.
                //    Instantiate(Resources.Load("Animation/MutantRage Animator.controller", typeof(RuntimeAnimatorController)));
            }
        }

        if(currentHealth <= 0)
        {
            anim.SetTrigger("Death");
            agent.enabled = false;
            gameObject.GetComponent<MonsterEyes>().enabled = false;
            hasTarget = false;
        }

        if (!hasTarget)
        {
            hasTarget = true;
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
            agent.transform.position = Vector3.Lerp(agent.transform.position, agent.transform.position + transform.up * 5, percentage);
            yield return null;
        }
        transform.position = new Vector3(230, 0, 0);
        hasTarget = false;
        hasRageMode = false;
    }
}
