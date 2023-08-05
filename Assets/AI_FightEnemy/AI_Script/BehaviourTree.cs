using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class BehaviourTree : MonoBehaviour
{
    private Dictionary<string, object> blackboard;
    private Node root;

    private bool shouldStart = true;

    private void Awake()
    {
        if (transform.childCount <= 0)
        {
            return;
        }

        if (!transform.GetChild(0).TryGetComponent<Node>(out var childNode))
        {
            return;
        }

        root = childNode;
        blackboard = new Dictionary<string, object>();
    }

    private void Update()
    {
        if (!root || !shouldStart)
        {
            return;
        }

        RunTree();
    }

    private async void RunTree()
    {
        shouldStart = false;
        await root.Run(transform.parent.gameObject, blackboard);
        shouldStart = true;
    }
}

