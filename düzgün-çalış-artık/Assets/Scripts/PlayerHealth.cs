using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 90;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("playerHurt");
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    public void IncreaseHealth(int increase)
    {
        currentHealth += increase;

        healthBar.SetHealth(currentHealth);
    }
}
