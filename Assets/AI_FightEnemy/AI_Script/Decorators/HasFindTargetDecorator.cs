using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


[DisallowMultipleComponent]
public class HasFindTargetDecorator : Decorator
{
    public override async Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        if(agent.GetComponent<EnemyMonster>().hasTarget)
        {
            return Outcome.SUCCESS;
        }
        else
        {

        }

        return await child.Run(agent, blackboard);
    }
}