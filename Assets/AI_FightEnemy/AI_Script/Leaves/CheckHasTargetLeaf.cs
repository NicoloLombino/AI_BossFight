using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CheckHasTargetLeaf : Leaf
{
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        if (agent.GetComponent<EnemyMonster>().hasTarget)
        {
            return Outcome.SUCCESS;
        }

        await Task.Delay((int)(1 * 1000));
        return Outcome.FAIL;
    }
}
