using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public class SelectorComposite : Composite
{
    public override async Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        foreach(var child in children)
        {
            if(await child.Run(agent, blackboard) == Outcome.SUCCESS)
            {
                return Outcome.SUCCESS;
            }
        }

        return Outcome.FAIL;
    }
}