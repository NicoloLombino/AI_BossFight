

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class ChargeToTargetLeaf : Leaf
{
    [SerializeField, Min(0)]
    private float acceptanceRange;
    [SerializeField]
    private string targetBlackboardKey;
    [SerializeField]
    private GameObject chargeCollider;
    [SerializeField]
    private GameObject finalAttackCollider;
    [SerializeField]
    private GameObject finalAttackParticles;
    [SerializeField]
    private Transform finalAttackHole;

    public float maxRunTimer;
    private float runTimer;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        Vector3 position = agent.GetComponent<EnemyMonster>().target.position;


        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }

        navMeshAgent.speed = 40 * agent.GetComponent<EnemyMonster>().RageSpeedIncrement;
        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Charge");
        navMeshAgent.SetDestination(position);
        navMeshAgent.isStopped = false;
        chargeCollider.SetActive(true);

        while (Vector3.Distance(agent.transform.position, position) > acceptanceRange && runTimer < maxRunTimer)
        {
            runTimer += Time.deltaTime;
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
        if (agent.GetComponent<EnemyMonster>().hasRageMode)
        {
            agent.GetComponent<Animator>().SetTrigger("ChargeRageAttack");
            await Task.Delay((int)(0.5 * 1000));
            finalAttackCollider.SetActive(true);
            await Task.Delay((int)(0.5 * 1000));
            agent.transform.position += transform.forward * 2;
            finalAttackCollider.SetActive(false);
            GameObject attackDistance = Instantiate(finalAttackParticles, finalAttackHole.position, finalAttackHole.rotation);
        }
        else
        {
            if (!Physics.Raycast(transform.position + Vector3.up * 1, transform.forward, out RaycastHit hitF, 22, LayerMask.GetMask("Default")))
            {
                Debug.Log("ENTER");
                navMeshAgent.SetDestination(agent.transform.position + transform.forward * 20);
                await Task.Delay((int)(1f * 1000));
            }
        }
        runTimer = 0;
        chargeCollider.SetActive(false);
        navMeshAgent.speed = 4f * agent.GetComponent<EnemyMonster>().RageSpeedIncrement;
        navMeshAgent.isStopped = true;
        agent.GetComponent<Animator>().SetTrigger("StopCharge");
        await Task.Delay((int)(1 * 1000));
        return Outcome.SUCCESS;
    }
}