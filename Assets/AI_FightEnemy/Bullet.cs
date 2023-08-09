using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    public int damage;
    [SerializeField]
    private GameObject hitParticles;

    AudioSource audioSource;
    public AudioClip hitClip;

    public bool isPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, 3);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Enemy"))
        if(other.gameObject.layer == 13)
        {
            if(isPlayer)
            {
                if (other.gameObject.GetComponentInParent<EnemyMonster>().currentHealth <= 0)
                {
                    GameObject part1 = Instantiate(hitParticles, transform.position, Quaternion.identity);
                    return;
                }

                other.gameObject.GetComponent<MonsterHitPoint>().ReceiveDamageOnPoint(damage);
                Debug.Log(damage);
                audioSource.PlayOneShot(hitClip);
                GameObject part = Instantiate(hitParticles, transform.position, Quaternion.identity);
            }
        }
    }
}
