using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterMovement controller;
	public Animator animator;
	public Enemy enemy;

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

	AudioSource jumpSound;
	AudioSource hitSound;

	void Start()
	{
		FindObjectOfType<AudioManager>().Play("backgroundSound");
		attack = false;
		attackCollider.enabled = false;
		enemy = GetComponent<Enemy>();
		animator = GetComponent<Animator>();
		playerHealth = GetComponent<PlayerHealth>();
		//jumpSound = GetComponent<AudioSource>();
		//hitSound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			FindObjectOfType<AudioManager>().Play("jumpSound");
			//jumpSound.Play();
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

		FindObjectOfType<AudioManager>().Play("walkSound");


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
			playerHealth.TakeDamage(100);
		}
		if (other.gameObject.tag == "Enemy" && attack)
		{
			Enemy enemyHealthAmount = other.transform.GetComponent<Enemy>();
			if (enemyHealthAmount)
			{
				enemyHealthAmount.TakeEnemyDamage(3);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Potion"))
		{
			playerHealth.IncreaseHealth(playerHealth.maxHealth - playerHealth.currentHealth);
			Destroy(other.gameObject);
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
			//hitSound.Play();
			FindObjectOfType<AudioManager>().Play("SwordSound");
		}

		if (attack)
		{
			if (attackTimer > 0)
			{

				attackTimer -= Time.deltaTime;
			}

			else
			{
				FindObjectOfType<AudioManager>().Play("playerHurt");
				attack = false;
				attackCollider.enabled = false;
			}
		}
		//hitSound.Play();
	}

}
