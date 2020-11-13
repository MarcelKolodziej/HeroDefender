using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] public int TurretDamage = 2;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private Transform TurretBarrel;

    private BasicEnemy Enemy;
    private Vector3 targetDirection;
    private Quaternion targetRotation;


    public void Update()
    {
        if (Enemy != null)
        {
            targetDirection = (Enemy.transform.position - TurretBarrel.transform.position).normalized;
            targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

            TurretBarrel.rotation = Quaternion.RotateTowards(TurretBarrel.rotation, targetRotation, Time.deltaTime * RotateSpeed);

            if (TurretBarrel.rotation == targetRotation)
            {
                Fire();
            }
        }
    }
    
    public void Fire()
    {
        Debug.Log("Fire");

        if (Enemy.TakeDamage(TurretDamage))
        {
            Debug.Log("Enemy Killed");
            Enemy = null;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter: "+ collision.gameObject.name);

        if (Enemy == null)
        {
            Enemy = collision.gameObject.GetComponent<BasicEnemy>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit");

        Enemy = null;
    }
}
