using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack2Leaf : Leaf
{
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        //if (!agent.GetComponent<EnemyMonster>().isTargetInRange)
        //{
        //    return Outcome.FAIL;
        //}

        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Attack2");
        Debug.Log("ATTACK 2");
        await Task.Delay((int)(3 * 1000));
        Debug.Log("ATTACK 2 FINE AWAIT");
        return Outcome.SUCCESS;
    }
}
