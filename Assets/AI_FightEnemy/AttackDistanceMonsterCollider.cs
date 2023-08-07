using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDistanceMonsterCollider : MonoBehaviour
{
    [SerializeField]
    private Collider[] colliders;
    [SerializeField]
    private float totalTime;

    void Start()
    {
        float time = totalTime / colliders.Length;
        StartCoroutine(ColliderActivation(time));
    }

    private IEnumerator ColliderActivation(float timer)
    {
        
        foreach(Collider col in colliders)
        {
            col.enabled = true;
            yield return new WaitForSecondsRealtime(timer / 2);
            col.isTrigger = false;
            yield return new WaitForSecondsRealtime(timer / 2);
        }
        Destroy(gameObject, 1);
    }
}
