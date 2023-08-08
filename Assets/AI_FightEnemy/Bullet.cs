using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    public int damage;

    AudioSource audioSource;
    public AudioClip hitClip;

    public bool isPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
                other.gameObject.GetComponent<MonsterHitPoint>().ReceiveDamageOnPoint(damage);
                Debug.Log(damage);
                audioSource.PlayOneShot(hitClip);
            }
        }
    }
}
