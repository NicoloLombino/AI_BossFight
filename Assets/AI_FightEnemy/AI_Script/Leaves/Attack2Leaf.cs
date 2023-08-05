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

        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Attack2");
        foreach(GameObject col in colliders)
        {
            col.SetActive(true);
        }
        await Task.Delay((int)(3 * 1000));
        foreach (GameObject col in colliders)
        {
            col.SetActive(false);
        }
        return Outcome.SUCCESS;
    }
}
