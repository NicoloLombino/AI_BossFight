using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float rotationSpeed;

    public bool isLocked;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = player.position;

        if(!isLocked)
        {
            transform.eulerAngles = new Vector3(0, player.eulerAngles.y, 0);
        }
        
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed);
        }
    }
}
