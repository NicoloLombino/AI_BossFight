using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[DisallowMultipleComponent]
public class MonsterEyes : MonoBehaviour
{
    [Range(0f, 360f)]
    public float fov = 120f;
    public float range = 5;

    [SerializeField]
    public string Tag;

    private void Update()
    {
        GetTargets();
    }
    public /*List<GameObject>*/ void GetTargets()
    {
        List<GameObject> targets = new List<GameObject>();
        Vector3 pos = transform.position + Vector3.up;
        Collider[] hitColliders = Physics.OverlapSphere(pos, range);

        foreach (var e in hitColliders)
        {
            Vector3 directionTarget = (e.transform.position - transform.forward).normalized;
            if (Vector3.Dot(transform.forward, directionTarget) < Mathf.Cos(fov) || e.gameObject.tag != Tag)
                continue;

            targets.Add(e.gameObject);
            gameObject.GetComponent<EnemyMonster>().SetFindTarget(true);
            gameObject.GetComponent<EnemyMonster>().isTargetInRange = true;
        }

        gameObject.GetComponent<EnemyMonster>().isTargetInRange = targets.Count > 0 ? true : false;
        //if (targets.Count > 0)
        //{
        //    gameObject.GetComponent<EnemyMonster>().isTargetInRange = true;
        //}
        //else
        //{
        //    gameObject.GetComponent<EnemyMonster>().isTargetInRange = false;
        //}
        //return targets;
    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position + Vector3.up;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(pos, Quaternion.Euler(0, -fov / 2, 0) * transform.forward * range);
        Gizmos.DrawRay(pos, Quaternion.Euler(0, fov / 2, 0) * transform.forward * range);
        Handles.color = Color.blue;
        Handles.DrawWireArc(pos, transform.up, Quaternion.Euler(0, -fov / 2, 0) * transform.forward, fov, range);
    }
}

