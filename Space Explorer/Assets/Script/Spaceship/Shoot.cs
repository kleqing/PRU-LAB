using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [Header("Transform References")]
    [SerializeField] private Transform SpaceshipTransform;
    [SerializeField] private Transform SpaceshipShootPoint;
    
    [Header("Bullet Settings")]
    [SerializeField] private GameObject bulletPrefab;
    
    [Range(0f, 2f)]
    [SerializeField] private float fireRate;
    
    private float fireTimer;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (Mouse.current.leftButton.isPressed && fireTimer <= 0f)
        {
            Instantiate(bulletPrefab, SpaceshipShootPoint.position, SpaceshipTransform.rotation);
            fireTimer = fireRate;
        }
        fireTimer -= Time.deltaTime;
    }
}
