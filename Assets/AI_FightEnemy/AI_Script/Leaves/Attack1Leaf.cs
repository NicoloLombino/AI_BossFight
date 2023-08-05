using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack1Leaf : Leaf
{
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        //if (!agent.GetComponent<EnemyMonster>().isTargetInRange)
        //{
        //    return Outcome.FAIL;
        //}

        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Attack1");
        Debug.Log("ATTACK 1");
        await Task.Delay(4 * 1000);
        Debug.Log("ATTACK 1 FINE AWAIT");
        return Outcome.SUCCESS;
    }
}
