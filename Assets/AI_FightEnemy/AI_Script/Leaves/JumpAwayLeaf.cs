using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class JumpAwayLeaf : Leaf
{
    [SerializeField]
    private string positionBlackboardKey;

    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {

        if (!agent.TryGetComponent<NavMeshAgent>(out var navMeshAgent))
        {
            return Outcome.FAIL;
        }

        agent.GetComponent<Animator>().SetTrigger("JumpAway");
        agent.GetComponent<EnemyMonster>().JumpAway();
        await Task.Delay((int)(4 * 1000));
        return Outcome.SUCCESS;
    }
}