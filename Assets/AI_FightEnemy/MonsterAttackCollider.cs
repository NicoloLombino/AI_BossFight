using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCollider : MonoBehaviour
{

    public int damage;
    public bool canDoDamage = true; 

    void Start()
    {
        canDoDamage = true;
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        canDoDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(canDoDamage)
            {
                other.gameObject.GetComponent<PlayerArcher>().ReceiveDamage(damage);
                canDoDamage = false;
            }
        }
    }
}
