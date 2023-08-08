using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetLeaf : Leaf
{
    [SerializeField, Min(0)]
    private float acceptanceRange;
    [SerializeField]
    private string targetBlackboardKey;

    public float maxRunTimer;
    private float runTimer;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        Vector3 position = agent.GetComponent<EnemyMonster>().target.position;


        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }

        navMeshAgent.speed = 10;
        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetFloat("Speed", 1);
        navMeshAgent.SetDestination(position);
        navMeshAgent.isStopped = false;

        while (Vector3.Distance(agent.transform.position, position) > acceptanceRange && runTimer < maxRunTimer)
        {
            runTimer += Time.deltaTime;
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
        runTimer = 0;
        navMeshAgent.speed = 3.5f;
        navMeshAgent.isStopped = true;
        agent.GetComponent<Animator>().SetFloat("Speed", 0);
        return Outcome.SUCCESS;
    }
}