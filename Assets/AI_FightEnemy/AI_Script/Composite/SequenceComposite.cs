using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[DisallowMultipleComponent]
public class SequenceComposite : Composite
{
    public override async Task<Outcome> Run(GameObject agent, Dictionary<string, object> blackboard)
    {
        foreach (var child in children)
        {
            if (await child.Run(agent, blackboard) == Outcome.FAIL)
            {
                return Outcome.FAIL;
            }
        }

        return Outcome.SUCCESS;
    }
}
