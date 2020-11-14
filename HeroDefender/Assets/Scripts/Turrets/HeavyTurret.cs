using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyTurret : BasicTurret
{
    public override void Update()
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
                // This Turret cannot rotate towards a target whilst reloading
                if (CurrentFireTick >= FireSpeed)
                {
                    targetDirection = (Enemies[0].transform.position - TurretBarrel.transform.position).normalized;
                    targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);

                    TurretBarrel.rotation = Quaternion.RotateTowards(TurretBarrel.rotation, targetRotation, Time.deltaTime * RotateSpeed);

                    if (TurretBarrel.rotation == targetRotation)
                    {
                        CurrentFireTick = 0f;
                        Fire();
                    }
                }
            }
        }
    }
}
