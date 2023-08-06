using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[DisallowMultipleComponent]
public class IsNearTargetDecorator : Decorator
{
    public override async Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        float distance = Vector3.Distance(transform.position, agent.GetComponent<EnemyMonster>().target.position);
        if(distance < agent.GetComponent<EnemyMonster>().attackRange)
        {
            return Outcome.SUCCESS;
        }

        return await child.Run(agent, blackboard);
    }
}