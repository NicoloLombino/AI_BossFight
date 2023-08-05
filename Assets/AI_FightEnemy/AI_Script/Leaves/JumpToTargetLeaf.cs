

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class JumpToTargetLeaf : Leaf
{
    [SerializeField, Min(0)]
    private float acceptanceRange;
    [SerializeField]
    private string targetBlackboardKey;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float jumpTimer;

    private float timer;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        Debug.Log("JUMP TO TARGET");
        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }

        
        Vector3 targetPosition = agent.GetComponent<EnemyMonster>().target.position;
        navMeshAgent.isStopped = false;
        //navMeshAgent.SetDestination(targetPosition);
        agent.GetComponent<Animator>().SetTrigger("Jumping");
        Vector3 startPos = transform.position;
        float percentage = 0;
        while (Vector3.Distance(agent.transform.position, targetPosition) > acceptanceRange)
        {
            float yOffset = jumpHeight * 4 * (percentage - percentage * percentage);
            agent.GetComponent<NavMeshAgent>().transform.position = Vector3.Lerp(startPos,
                targetPosition, percentage) + yOffset * Vector3.up;

            timer += Time.deltaTime;
            percentage = timer / jumpTimer;
            //transform.position = agent.transform.position;

            await Task.Delay((int)(Time.fixedDeltaTime * 1000));
        }
        timer = 0;
        navMeshAgent.isStopped = true;
        return Outcome.SUCCESS;
    }
}