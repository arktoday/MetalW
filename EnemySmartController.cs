using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmartController : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    EnemyDumbController enemyDumbController;
    int lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        enemyDumbController = GetComponent<EnemyDumbController>();
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = enemyDumbController.GetLookDirection();
        Vector2 position = rigidBody2D.position;
        RaycastHit2D hit = Physics2D.Raycast(position + new Vector2(lookDirection, 0) * 0.5f + Vector2.down * 0.7f, new Vector2(lookDirection, 1), 0.1f);
        if (hit.collider == null) enemyDumbController.SetLookDirection(-lookDirection);
    }
}
