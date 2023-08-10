using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoozySpell : MonoBehaviour
{
    [SerializeField]
    private float spellTimerToComplete;
    [SerializeField]
    private float spellLifeTime;
    [SerializeField]
    private Transform spellcollider;

    private float timer;

    private float playerTimer;
    private float monsterTimer;

    private void Start()
    {
        Destroy(gameObject, spellLifeTime);
    }
    void Update()
    {
        if(timer < spellTimerToComplete)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(200, 200, 200), timer / spellTimerToComplete);
            transform.position = Vector3.Lerp(new Vector3(0, 10, 0), new Vector3(0, 100, 0), timer / spellTimerToComplete);
            spellcollider.localScale = Vector3.Lerp(Vector3.one, new Vector3(100, 200, 100), timer / spellTimerToComplete);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(other.gameObject.GetComponent<PlayerArcher>().health <= 0)
            {
                return;
            }
            else
            {
                playerTimer += Time.deltaTime;
                if(playerTimer >= 1)
                {
                    other.gameObject.GetComponent<PlayerArcher>().ReceiveDamage(10, 1f);
                    playerTimer = 0;
                }
            }
        }

        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyMonster>().currentHealth <= 0)
            {
                return;
            }
            else
            {
                playerTimer += Time.deltaTime;
                if (monsterTimer >= 1)
                {
                    other.gameObject.GetComponent<EnemyMonster>().ReceiveDamage(50);
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

        if (other.CompareTag("Enemy"))
        {
            monsterTimer = 0;
        }
    }
}
