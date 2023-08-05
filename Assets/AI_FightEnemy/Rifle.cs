using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform shotTransform;

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotTransform.position, shotTransform.rotation);
    }
}
