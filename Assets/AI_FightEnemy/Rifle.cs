using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rifle : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Transform shotTransform;
    [SerializeField]
    internal GameObject arrowInBow;
    [SerializeField]
    internal GameObject aimObj;
    public float aimRange;
    public float rotationSpeed;

    [SerializeField]
    private PlayerArcher player;

    float xRotation = 0f;


    private void Awake()
    {

    }

    private void Update()
    {
        //transform.Rotate(Vector3.right, Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed);
        if(!player.canShoot)
        {
            Vector2 moveCam = Gamepad.current.rightStick.ReadValue().normalized;
            float _mousey = moveCam.y * rotationSpeed * Time.deltaTime;

            xRotation -= _mousey;
            xRotation = Mathf.Clamp(xRotation, -60, 0);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        //if (transform.eulerAngles.x < -60)
        //{
        //    transform.eulerAngles = new Vector3(-60, 0, 0);
        //}
        //else if(transform.eulerAngles.x > 0)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}
        aimObj.SetActive(arrowInBow.activeInHierarchy);
        AimSystem();
    }
    public void Shoot(int charge)
    {
        Bullet bullet = Instantiate(bulletPrefab, shotTransform.position, shotTransform.rotation);
        bullet.damage += charge;
    }

    private void AimSystem()
    {
        if (!arrowInBow.activeInHierarchy)
            return;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 500))
        {
            if (hit.collider && !hit.collider.isTrigger)
            {
                aimObj.transform.localPosition = new Vector3(0,0, hit.distance);
            }
            else
            {
                aimObj.transform.localPosition = new Vector3(0, 0, 10);
            }
        }
    }
}
