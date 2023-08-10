using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Attack1Leaf : Leaf
{
    public GameObject[] colliders;
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        float distance = Vector3.Distance(transform.position, agent.GetComponent<EnemyMonster>().target.position);
        if (distance > agent.GetComponent<EnemyMonster>().attackRange)
        {
            return Outcome.SUCCESS;
        }

        int fixSpeedOnRage = 1;
        if (agent.GetComponent<EnemyMonster>().hasRageMode)
        {
            fixSpeedOnRage = 2;
        }
        agent.GetComponent<EnemyMonster>().LookAtTarget();
        agent.GetComponent<Animator>().SetTrigger("Attack1");
        await Task.Delay((int)((1 / fixSpeedOnRage) * 1000));
        foreach (GameObject col in colliders)
        {
            col.SetActive(true);
        }
        await Task.Delay((int)((2f / fixSpeedOnRage) * 1000));
        foreach (GameObject col in colliders)
        {
            col.SetActive(false);
        }
        await Task.Delay(1 * 1000);
        return Outcome.SUCCESS;
    }
}
