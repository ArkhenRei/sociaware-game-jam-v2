using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterMovement controller;
	public Animator animator;

	public bool attack;
	private float attackTimer;
	[SerializeField]
	private float attackCooldown;
	[SerializeField]
	private Collider2D attackCollider;

	public PlayerHealth playerHealth;
	public LevelController level;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;

	void Start()
    {
		attack = false;
		attackCollider.enabled = false;

		animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("isJumping", true);
		}

        if (playerHealth.currentHealth < 0 || playerHealth.currentHealth == 0)
        {
			LevelController.RestartLevel();
        }
		Attack();
	}

	public void OnLanding()
    {
		animator.SetBool("isJumping", false);
    }

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
		if (other.transform.CompareTag("Trap"))
		{
			playerHealth.TakeDamage(90);
		}
	}

    public void Attack()
    {
		if (Input.GetKeyDown("e") && !attack)
		{
			attack = true;
			attackTimer = attackCooldown;
			attackCollider.enabled = true;
			animator.SetTrigger("attack");
		}
        if (attack)
        {
            if (attackTimer > 0)
            {
				attackTimer -= Time.deltaTime;
            }

			else
			{
				attack = false;
				attackCollider.enabled = false;
			}
		}
    }

}
