using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class MoveToPositionLeaf : Leaf
{
    [SerializeField, Min(0)]
    private float acceptanceRange;
    [SerializeField]
    private string positionBlackboardKey;

    [Header("range of random walk")]
    [SerializeField] private float MinRange;
    [SerializeField] private float MaxRange;
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        Vector3 position = new Vector3(Random.Range(MinRange, MaxRange)
            * Mathf.Sign(Random.Range(-1, 1)), 0, Random.Range(MinRange, MaxRange)
            * Mathf.Sign(Random.Range(-1, 1)));


        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }

        if (agent.GetComponent<EnemyMonster>().hasTarget)
        {
            return Outcome.FAIL;
        }

        agent.GetComponent<Animator>().SetFloat("Walking", 1f);
        navMeshAgent.SetDestination(position);
        navMeshAgent.isStopped = false;

        while(Vector3.Distance(agent.transform.position, position) > acceptanceRange)
        {
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
        agent.GetComponent<Animator>().SetFloat("Walking", 0f);
        navMeshAgent.isStopped = true;
        return Outcome.SUCCESS;
    }
}