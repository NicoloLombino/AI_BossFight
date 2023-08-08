using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack2Leaf : Leaf
{
    public GameObject[] colliders;
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        //if (!agent.GetComponent<EnemyMonster>().isTargetInRange)
        //{
        //    return Outcome.FAIL;
        //}
        int fixSpeedOnRage = 1;
        if(agent.GetComponent<EnemyMonster>().hasRageMode)
        {
            fixSpeedOnRage = 2;
        }
        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Attack2");
        await Task.Delay((int)((1 / fixSpeedOnRage) * 1000));
        foreach (GameObject col in colliders)
        {
            col.SetActive(true);
        }
        await Task.Delay((int)(1.5  * 1000));
        foreach (GameObject col in colliders)
        {
            col.SetActive(false);
        }
        await Task.Delay((int)(1 * 1000));
        return Outcome.SUCCESS;
    }
}
