using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public int CurrentLocationIndex = 0;
    public float MovementSpeed = 0.05f;
    [SerializeField] private int MaxHealth = 5;
    private int CurrentHealth;


    public void RespawnEnemy()
    {
        CurrentLocationIndex = 0;
        CurrentHealth = MaxHealth;
        gameObject.SetActive(true);
    }

    public bool TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }
}
