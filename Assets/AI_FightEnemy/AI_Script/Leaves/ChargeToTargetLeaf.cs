

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
    public float maxRunTimer;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        Debug.Log("START CHARGE TO TARGET");

        Vector3 position = agent.GetComponent<EnemyMonster>().target.position;


        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }

        navMeshAgent.speed = 30 * agent.GetComponent<EnemyMonster>().RageSpeedIncrement;
        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Charge");
        navMeshAgent.SetDestination(position);
        navMeshAgent.isStopped = false;
        chargeCollider.SetActive(true);

        while (Vector3.Distance(agent.transform.position, position) > acceptanceRange)
        {
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
        }
        chargeCollider.SetActive(false);
        navMeshAgent.speed = 3.5f * agent.GetComponent<EnemyMonster>().RageSpeedIncrement;
        navMeshAgent.isStopped = true;
        Debug.Log("END CHARGE TO TARGET");
        agent.GetComponent<Animator>().SetTrigger("StopCharge");
        return Outcome.SUCCESS;
    }
}