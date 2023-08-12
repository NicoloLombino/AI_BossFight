using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMonster : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;

    [SerializeField]
    public Transform target;

    public bool hasTarget;
    public bool isTargetInRange;
    [SerializeField]
    private SkinnedMeshRenderer mesh;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject BT;

    [Header("Stats")]
    [SerializeField]
    private float maxHealth;
    public float currentHealth;
    public bool hasRageMode;
    public int rageValue;
    public bool shouldMove;
    public Transform positionToReach;
    public bool isAttacking;
    [SerializeField]
    private int numberOfRage;

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
    private Material materialBase;
    [SerializeField]
    private Material materialRage;
    [SerializeField]
    private GameObject winPanel;


    public int areaWhereIsThisMonster;


    private float rageTimer;

    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        healthToRunAway = Mathf.RoundToInt(maxHealth * (percentageToRunAway / 100));
        //material.color = Color.white;
        mesh.material = materialBase;
    }

    void Update()
    {
        if(hasRageMode && numberOfRage <= 4)
        {
            rageTimer += Time.deltaTime;
            if(rageTimer >= MaxRageTimer)
            {
                hasRageMode = false;
                rageValue = 0;
                rageTimer = 0;
                rageValueIncrementWhenHit += 2;
                mesh.material = materialBase;
                rageLight.SetActive(false);
                anim.speed = 1;
                RageSpeedIncrement = 1;
                leftHand.transform.localScale = new Vector3(1,1,1);
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
                mesh.material = materialRage;
                anim.speed = 2;
                RageSpeedIncrement = 1.5f;
                numberOfRage++;
                //anim.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.
                //    Instantiate(Resources.Load("Animation/MutantRage Animator.controller", typeof(RuntimeAnimatorController)));
                if (numberOfRage >= 2)
                {
                    leftHand.transform.localScale = new Vector3(2, 2, 2);
                }
            }
        }

        if(currentHealth <= 0)
        {
            anim.SetTrigger("Death");
            agent.enabled = false;
            gameObject.GetComponent<MonsterEyes>().enabled = false;
            SetFindTarget(false);
            gameObject.tag = "Untagged";
            gameObject.layer = 0;
            winPanel.SetActive(true);
            BT.SetActive(false);
            enabled = false;
        }

        if (!hasTarget)
        {
            SetFindTarget(true);
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
        yield return new WaitForSecondsRealtime(0.7f);
        float percentage = 0;
        while (percentage < 1)
        {
            percentage += Time.deltaTime / 4;
            agent.transform.position = Vector3.Lerp(agent.transform.position, agent.transform.position + transform.up * 80, percentage);
            yield return null;
        }

        int rnd = 0;
        bool canGo = false;
        while(!canGo)
        {
            rnd = Random.Range(0, 4);
            if(rnd != areaWhereIsThisMonster)
            {
                canGo = true;
            }
        }

        switch(rnd)
        {
            case 0: transform.position = new Vector3(15, 0, -135);
                break;
            case 1:
                transform.position = new Vector3(125, 0, -130);
                break;
            case 2:
                transform.position = new Vector3(130, 0, -15);
                break;
            case 3:
                transform.position = new Vector3(5, 0, -5);
                break;
        }
        gameObject.GetComponent<EnemyMonster>().SetFindTarget(false);
        hasRageMode = false;
        healthToRunAway = Mathf.RoundToInt(currentHealth * (percentageToRunAway / 100));
    }

    public void SetFindTarget(bool find)
    {
        hasTarget = find; 
        target.gameObject.GetComponent<PlayerArcher>().enemyEyeWhenSeePlayer.SetActive(find);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Area"))
        {
            areaWhereIsThisMonster = other.gameObject.GetComponent<AreaTrigger>().areaNumber;
        }
    }
}
