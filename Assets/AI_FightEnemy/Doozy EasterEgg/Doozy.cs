using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doozy : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    private GameObject spellCircle;
    [SerializeField]
    private GameObject spell;
    [SerializeField]
    private GameObject spellCollider;
    [SerializeField]
    private CapsuleCollider capsuleCollider;

    [SerializeField]
    private float spellTimer;
    private float timer;

    private bool usedSpell;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Arrow"))
        {
            if(!usedSpell)
            {
                usedSpell = true;
                anim.SetTrigger("Spell");
                spell.SetActive(true);
                spellCircle.SetActive(true);
                spellCollider.SetActive(true);
                capsuleCollider.center = new Vector3(0,2,0);
                capsuleCollider.radius = 3;
                capsuleCollider.height = 10;
            }
            else
            {
                Destroy(other.gameObject);
            }
        }
    }
}
