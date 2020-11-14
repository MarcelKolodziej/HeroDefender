using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private SpriteRenderer HealthBar;
    [SerializeField] private Vector3 DefaultHealthBarScale = new Vector3(1, 0.16f, 1);
    [SerializeField] private int MaxHealth = 5;
    [SerializeField] private int AttackDamage = 3;

    public int CurrentLocationIndex = 0;
    public float MovementSpeed = 0.05f;

    private int CurrentHealth;


    public void RespawnEnemy()
    {
        HealthBar.transform.localScale = DefaultHealthBarScale;
        CurrentLocationIndex = 0;
        CurrentHealth = MaxHealth;
        gameObject.SetActive(true);
    }

    public bool TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.transform.localScale = new Vector3(Mathf.Lerp(0f, DefaultHealthBarScale.x, ((float)CurrentHealth / MaxHealth)), DefaultHealthBarScale.y, 1);

        if (CurrentHealth <= 0)
        {
            gameObject.SetActive(false);
            return true;
        }

        return false;
    }

    public int EnemyHasReachedTheBase()
    {
        gameObject.SetActive(false);
        return AttackDamage;
    }
}