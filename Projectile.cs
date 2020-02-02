using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public float existTime = 0.6f;
    // Start is called before the first frame update
    float timer;
    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        timer = existTime;
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0) Destroy(gameObject);
    }

    // Update is called once per frame
    public void Launch (Vector2 direction)
    {
        rigidBody2D.AddForce(direction * 300);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.GetComponent<EnemyDumbController>() != null)
        {
            EnemyDumbController edController = other.collider.GetComponent<EnemyDumbController>();
            edController.ChangeHealth(-1);
        }
        Destroy(gameObject);
    }
}
