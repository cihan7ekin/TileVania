using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemiesScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D enemiesRigiBody;
    Animator enemiesAnimation;
    CapsuleCollider2D enemiesCapsuleCollider;
    BoxCollider2D enemiesBoxCollider;

    void Start()
    {
        enemiesRigiBody = GetComponent<Rigidbody2D>();
        enemiesAnimation = GetComponent<Animator>();
        enemiesCapsuleCollider = GetComponent<CapsuleCollider2D>();
        enemiesBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        enemiesRigiBody.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Math.Sign(moveSpeed)),1f);
    }
}
