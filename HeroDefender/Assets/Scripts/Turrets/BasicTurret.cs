using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] public int TurretDamage = 2;
    [SerializeField] public float FireSpeed = 1f;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private Transform TurretBarrel;

    private List<BasicEnemy> Enemies = new List<BasicEnemy>();
    private Vector3 targetDirection;
    private Quaternion targetRotation;
    private float CurrentFireTick = 0f;


    public void Update()
    {
        CurrentFireTick += Time.deltaTime;

        if (Enemies.Any())
        {
            targetDirection = (Enemies[0].transform.position - TurretBarrel.transform.position).normalized;
            targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

            TurretBarrel.rotation = Quaternion.RotateTowards(TurretBarrel.rotation, targetRotation, Time.deltaTime * RotateSpeed);

            if (TurretBarrel.rotation == targetRotation)
            {
                if (CurrentFireTick >= FireSpeed)
                {
                    CurrentFireTick = 0f;
                    Fire();
                }
            }
        }
    }
    
    public void Fire()
    {
        Debug.Log("Fire");

        if (Enemies[0].TakeDamage(TurretDamage))
        {
            Debug.Log("Enemy Killed");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter: " + collision.gameObject.name);

        if (collision.gameObject.layer == 10)
        {
            Enemies.Add(collision.gameObject.GetComponent<BasicEnemy>());
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Trigger Exit: " + collision.gameObject.name);

        if (collision.gameObject.layer == 10)
        {
            Enemies.Remove(collision.gameObject.GetComponent<BasicEnemy>());
        }
    }
}
