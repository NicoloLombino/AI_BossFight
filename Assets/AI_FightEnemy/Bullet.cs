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
                audioSource.PlayOneShot(hitClip);
                GameObject part = Instantiate(hitParticles, transform.position, Quaternion.identity);

                // increase the scale of particles with the damage
                foreach (Transform t in part.transform)
                {
                    t.transform.localScale += Vector3.one * 0.05f * damage * other.gameObject.GetComponent<MonsterHitPoint>().weaknessMultiplier * 1.5f;

                }
                part.transform.localScale += Vector3.one * 0.2f * damage * other.gameObject.GetComponent<MonsterHitPoint>().weaknessMultiplier * 1.5f;
                Debug.Log("scale particles = " + part.transform.localScale);
                isPlayer = false;
                Destroy(gameObject, 0.1f);
            }
        }

        if(other.gameObject.layer == 0)
        {
            Destroy(gameObject);
        }
    }
}
