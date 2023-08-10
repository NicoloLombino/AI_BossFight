using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitPoint : MonoBehaviour
{
    [SerializeField]
    [Min(0.1f)]
    public float weaknessMultiplier;
    [SerializeField]
    private EnemyMonster monster;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamageOnPoint(int damage)
    {
        float realDamage = damage * weaknessMultiplier;
        monster.ReceiveDamage((int)realDamage);
        Debug.Log("damage= " + realDamage);
    }
}
