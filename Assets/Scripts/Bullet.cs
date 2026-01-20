using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    float xSpeed;

    Rigidbody2D myRigidBody;
    PlayerMovement playerMovement;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        xSpeed = playerMovement.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidBody.linearVelocity = new Vector2(xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
