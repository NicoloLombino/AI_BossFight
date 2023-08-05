using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Composite : Node
{
    public List<Node> children;

    private void Awake()
    {
        children = new List<Node>();
        foreach (Transform child in transform) {
            if(!child.gameObject.TryGetComponent<Node>(out var childNode))
            {
                continue;
            }

            children.Add(childNode);
        }
    }
}