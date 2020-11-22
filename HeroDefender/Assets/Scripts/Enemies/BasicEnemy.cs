using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private SpriteRenderer BodyRenderer;
    [SerializeField] private SpriteRenderer HealthBar;
    [SerializeField] private Vector3 DefaultHealthBarScale = new Vector3(1, 0.16f, 1);
    [SerializeField] private int MaxHealth = 5;
    [SerializeField] private int AttackDamage = 3;

    [Header("Sprite Settings")]
    [SerializeField] private Sprite[] MovingSprites;
    [SerializeField] private float AnimationSpeed;

    [Header("Movement Settings")]
    public int CurrentLocationIndex = 0;
    public float MovementSpeed = 0.05f;
    public float RotateSpeed = 40;

    private int CurrentHealth;
    private float SpawnTime;


    public void RespawnEnemy(Vector3 Direction)
    {
        BodyRenderer.flipX = (Direction == Vector3.right ? true : false);

        HealthBar.transform.localScale = DefaultHealthBarScale;
        CurrentLocationIndex = 0;
        CurrentHealth = MaxHealth;
        SpawnTime = Time.time;
        gameObject.SetActive(true);
    }

    public void UpdateSprite()
    {
        BodyRenderer.sprite = MovingSprites[GetCurrentAnimationFrame()];
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

    private int GetCurrentAnimationFrame()
    {
        Debug.Log((int)(Time.time * AnimationSpeed) % (MovingSprites.Length));
        return (int)(Time.time * AnimationSpeed) % (MovingSprites.Length);
    }
}