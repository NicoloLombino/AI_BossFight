using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WaitLeaf : Leaf
{
    [SerializeField, Min(0)]
    private float waitTime;
    public async override Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        agent.GetComponent<Animator>().SetFloat("Speed", 0.0f);
        agent.GetComponent<Animator>().SetTrigger("Roaring");
        agent.GetComponent<AudioSource>().Play();
        await Task.Delay((int)(waitTime * 1000));
        return Outcome.SUCCESS;
    }
}
