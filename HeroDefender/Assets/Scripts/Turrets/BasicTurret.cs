using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    [Header("Turret Settings")]
    [SerializeField] protected int TurretDamage = 2;
    [SerializeField] protected float DeploySpeed = 0.5f;
    [SerializeField] protected float FireSpeed = 1f;
    [SerializeField] protected float RotateSpeed;
    [SerializeField] protected Transform TurretBarrel;

    protected List<BasicEnemy> Enemies = new List<BasicEnemy>();
    protected Vector3 targetDirection;
    protected Quaternion targetRotation;
    protected float currentDeployTick = 0f;
    protected float CurrentFireTick = 0f;


    public virtual void Update()
    {
        if (currentDeployTick < DeploySpeed)
        {
            currentDeployTick += Time.deltaTime;

            // If the turret is deployed and an enemey is avalible it sets the gun barrel to that transform immeditaly
            if (currentDeployTick > DeploySpeed)
            {
                TurretBarrel.gameObject.SetActive(true);

                if (Enemies.Any())
                {
                    targetDirection = (Enemies[0].transform.position - TurretBarrel.transform.position).normalized;
                    targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);
                    TurretBarrel.rotation = targetRotation;

                }
            }
        }
        else
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
    }
    
    public virtual void Fire()
    {
        // Debug.Log("Fire");
        Enemies[0].TakeDamage(TurretDamage);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Trigger Enter: " + collision.gameObject.name);

        if (collision.gameObject.layer == 10)
        {
            Enemies.Add(collision.gameObject.GetComponent<BasicEnemy>());
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("Trigger Exit: " + collision.gameObject.name);

        if (collision.gameObject.layer == 10)
        {
            Enemies.Remove(collision.gameObject.GetComponent<BasicEnemy>());
        }
    }
}
