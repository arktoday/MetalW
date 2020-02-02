using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 99;
    public float speed = 3.0f;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    Rigidbody2D rigidBody2D;
    Animator animator;
    CharacterController2D characterController;
    Vector2 lookDirection = new Vector2(1, 0);
    bool isJump = false;
    bool isCrouch = false;
    float jumpSpeed;
    float horizontalMove = 0.0f;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        characterController = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
 //       animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        animator.SetFloat("Move X", horizontal);
        horizontalMove = horizontal * speed;

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f)||!Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
        else
        {
            isJump = false;
        }
        if (vertical < 0)
        {
            isCrouch = true;
        }
        else
        {
            isCrouch = false;
        }

        if (Input.GetButtonDown("Fire1") && vertical >= 0)
        {
            Launch();
        }
        else
        {

        }

    }
    void FixedUpdate()
    {
        characterController.Move(horizontalMove, isCrouch, isJump);
    }
    void Launch()
    {
        GameObject proj = Instantiate(projectilePrefab, rigidBody2D.position,Quaternion.identity);
        Projectile projectile = proj.GetComponent<Projectile>();
        projectile.Launch(lookDirection);
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SceneTransfer>() != null)
        {
            SceneManager.LoadScene(other.GetComponent<SceneTransfer>().SceneName);
        }
    }
}