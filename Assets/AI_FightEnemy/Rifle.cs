using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;
    [SerializeField]
    private Transform shotTransform;
    [SerializeField]
    internal GameObject arrowInBow;
    public float aimRange;

    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if(hit.collider)
            {
                lr.SetPosition(1, new Vector3(0,0, hit.distance));
            }
            else
            {
                lr.SetPosition(1, new Vector3(0, 0, aimRange));
            }
        }
    }
    public void Shoot(int charge)
    {
        Bullet bullet = Instantiate(bulletPrefab, shotTransform.position, shotTransform.rotation);
        bullet.damage += charge;
    }
}
