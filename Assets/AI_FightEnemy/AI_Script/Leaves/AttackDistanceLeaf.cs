

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class AttackDistanceLeaf : Leaf
{
    [SerializeField]
    private string targetBlackboardKey;
    [SerializeField]
    private GameObject attackDistancePrefab;
    [SerializeField]
    private Transform attackDistanceHole;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }
        
        navMeshAgent.isStopped = true;
        float waitTime = 0;
        waitTime = agent.GetComponent<EnemyMonster>().hasRageMode ? 1.5f : 3;
        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Jumping");
        await Task.Delay((int)(waitTime * 1000));
        GameObject attackDistance = Instantiate(attackDistancePrefab, attackDistanceHole.position, attackDistanceHole.rotation);
        await Task.Delay((int)((waitTime + 2) * 1000));
        return Outcome.SUCCESS;
    }
}