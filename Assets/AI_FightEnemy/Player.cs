using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float rotationSpeed;

    [Header("Equip")]
    [SerializeField]
    private Rifle rifle;
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        ReadMovement();

        if(Input.GetMouseButtonDown(0))
        {
            rifle.Shoot(3);
        }
    }

    private void ReadMovement()
    {
        Vector3 direction = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime + transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        cc.Move(direction * speed);
        //transform.LookAt(transform.position + direction, Vector3.up);

        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
        cc.Move(Vector3.down * 10 * Time.deltaTime);
    }
}

