using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    public int damage;

    public bool isPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(isPlayer)
            {
                other.gameObject.GetComponent<EnemyMonster>().ReceiveDamage(damage);
                Debug.Log(damage);
            }
        }
    }
}
