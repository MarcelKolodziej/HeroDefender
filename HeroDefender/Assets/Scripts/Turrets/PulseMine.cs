using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseMine : BasicTurret
{
    public override void Update()
    {
        if (currentDeployTick < DeploySpeed)
        {
            currentDeployTick += Time.deltaTime;

            if (currentDeployTick > DeploySpeed)
            {
                TurretBarrel.gameObject.SetActive(true);
            }
        }
        else
        {
            CurrentFireTick += Time.deltaTime;

            if (Enemies.Any())
            {
                if (CurrentFireTick >= FireSpeed)
                {
                    TurretBarrel.gameObject.SetActive(false);
                    CurrentFireTick = 0f;
                    currentDeployTick = 0f;
                    Fire();
                }
            }
            else
            {
                TurretBarrel.gameObject.SetActive(false);
            }
        }
    }
    public override void Fire()
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].TakeDamage(TurretDamage);
        }
    }
}
