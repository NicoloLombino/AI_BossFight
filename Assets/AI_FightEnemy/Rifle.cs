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

    public void Shoot(int charge)
    {
        Bullet bullet = Instantiate(bulletPrefab, shotTransform.position, shotTransform.rotation);
        bullet.damage += charge;
    }
}
