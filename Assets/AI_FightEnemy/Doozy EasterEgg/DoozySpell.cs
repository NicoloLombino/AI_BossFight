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

    private void Start()
    {
        Destroy(spellcollider.gameObject, spellLifeTime);
        Destroy(gameObject, spellLifeTime + 0.5f);
    }
    void Update()
    {
        if(timer < spellTimerToComplete)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(200, 200, 200), timer / spellTimerToComplete);
            //transform.position = Vector3.Lerp(new Vector3(0, 10, 0), new Vector3(0, 100, 0), timer / spellTimerToComplete);
            spellcollider.localScale = Vector3.Lerp(Vector3.one, new Vector3(100, 100, 100), timer / spellTimerToComplete);
        }
    }
}
