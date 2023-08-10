using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoozySpellCollider : MonoBehaviour
{
    private float playerTimer;
    private float monsterTimer;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerArcher>().health <= 0)
            {
                return;
            }
            else
            {
                playerTimer += Time.deltaTime;
                if (playerTimer >= 1)
                {
                    other.gameObject.GetComponent<PlayerArcher>().ReceiveDamage(10, 1f);
                    playerTimer = 0;
                }
            }
        }

        if (other.gameObject.layer == 13)
        {
            if (other.gameObject.GetComponentInParent<EnemyMonster>().currentHealth <= 0)
            {
                return;
            }
            else
            {
                monsterTimer += Time.deltaTime;
                if (monsterTimer >= 1)
                {
                    other.gameObject.GetComponent<MonsterHitPoint>().ReceiveDamageOnPoint(5);
                    monsterTimer = 0;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTimer = 0;
        }

        if (other.gameObject.layer == 13)
        {
            monsterTimer = 0;
        }
    }
}
