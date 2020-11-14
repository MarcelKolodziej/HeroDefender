using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer HealthBar;
    [SerializeField] private Vector3 DefaultHealthBarScale = new Vector3(0.7f, 0.24f, 1);
    [SerializeField] private int MaxHealth = 30;
    private int CurrentHealth;

    public void Start()
    {
        CurrentHealth = MaxHealth;
        UpdatedHealthBarColour();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            TakeDamage(collision.gameObject.GetComponent<BasicEnemy>().EnemyHasReachedTheBase());
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HealthBar.transform.localScale = new Vector3(Mathf.Lerp(0f, DefaultHealthBarScale.x, ((float)CurrentHealth / MaxHealth)), DefaultHealthBarScale.y, 1);
        UpdatedHealthBarColour();

        if (CurrentHealth <= 0)
        {
            Debug.Log("You Lost");
        }
    }

    public void UpdatedHealthBarColour()
    {
        if (((float)CurrentHealth / (float)MaxHealth) > 0.5)
        {
            HealthBar.color = Color.green;
        }
        else if (((float)CurrentHealth / (float)MaxHealth) > 0.25)
        {
            HealthBar.color = Color.yellow; // they don't have orange...
        }
        else if (((float)CurrentHealth / (float)MaxHealth) < 0.25)
        {
            HealthBar.color = Color.red;
        }
    }
}
