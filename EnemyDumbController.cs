using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDumbController : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public int lookDirection;

    int health;
    Rigidbody2D rigidBody2D;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position;
        position = rigidBody2D.position;
        position.x += lookDirection * 3.0f * Time.deltaTime;
        rigidBody2D.position = position;
        position = rigidBody2D.position;
        RaycastHit2D hit = Physics2D.Raycast(position+new Vector2(lookDirection,0)*0.5f + Vector2.down * 0.3f, new Vector2(lookDirection, 0), 0.1f);
        if (hit.collider != null) lookDirection = -lookDirection;
        else
        {
            hit = Physics2D.Raycast(position + new Vector2(lookDirection, 0) * 0.5f + Vector2.up * 0.3f, new Vector2(lookDirection, 0), 0.1f);
            if (hit.collider != null) lookDirection = -lookDirection;
        }
    }
    public void SetLookDirection(int lookDirection)
    {
        this.lookDirection = lookDirection;
    }
    public int GetLookDirection()
    {
        return lookDirection;
    }
    public void ChangeHealth(int amount)
    {
        health += amount;
        if (health <= 0) Destroy(gameObject);
    }
    public void OnCollisionStay2D(Collision2D other)
    {
        if(other.collider.GetComponent<PlayerController>() != null)
        {
            other.collider.GetComponent<PlayerController>().ChangeHealth(-12);
        }
    }
}
