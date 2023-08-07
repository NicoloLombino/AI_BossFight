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

    private float runTimer;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        Debug.Log("AAA");
        Vector3 position = new Vector3(transform.position.x + Random.Range(MinRange, MaxRange)
            * Mathf.Sign(Random.Range(-1, 1)), 0, transform.position.z + Random.Range(MinRange, MaxRange)
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
        //agent.transform.LookAt(position);
        while(Vector3.Distance(agent.transform.position, position) > acceptanceRange && !agent.GetComponent<EnemyMonster>().hasTarget)
        {
            runTimer += Time.deltaTime;
            if (Physics.Raycast(transform.position + Vector3.up * 1, position, out RaycastHit hit, 5, LayerMask.GetMask("Default")))
            {
                break;
            }
            if(runTimer > 1 && Physics.Raycast(transform.position + Vector3.up * 1, transform.forward, out RaycastHit hitF, 5, LayerMask.GetMask("Default")))
            {
                break;
            }
            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
        agent.GetComponent<Animator>().SetFloat("Walking", 0f);
        navMeshAgent.isStopped = true;
        return Outcome.SUCCESS;
    }
}