using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public class RandomComposite : Composite
{
    public override async Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        return await children[Random.Range(0, children.Count)].Run(agent, blackboard);
    }
}
