using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = player.position;
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
        //if (Input.GetMouseButton(1))
        //{
            
        //}
    }
}
