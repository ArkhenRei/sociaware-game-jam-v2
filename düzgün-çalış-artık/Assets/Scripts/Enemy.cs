using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool hasEntered;
    Rigidbody2D rb;
    public static float healthAmount;
    public Animator animator;

    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = 6;
        rb = FindObjectOfType<Rigidbody2D>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        animator = GetComponentInChildren<Animator>();

        hasEntered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            animator.SetTrigger("run");
            Destroy(gameObject);
        }        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasEntered)
        {
            hasEntered = true;
            playerHealth.TakeDamage(30);
            //hasEntered = false;

            if(playerMovement.attack==true)
                healthAmount -= 3;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && hasEntered)
        {
            hasEntered = false;
            animator.SetTrigger("attack");
        }
    }

}
