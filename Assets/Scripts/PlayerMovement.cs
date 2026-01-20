using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using FirstGearGames.SmoothCameraShaker;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] ShakeData explosionShakeData;

    [SerializeField] float speedAmount;
    [SerializeField] float jumpAmount;
    [SerializeField] Color32 deathColor;
    [SerializeField] Vector2 deathKick = new Vector2(10f,20f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    // [SerializeField] float climbingAmount;

    // Attack Area 
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemiesLayer;
    // End of Attack Area

    Vector2 moveInput;

    Rigidbody2D myRigibody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool doubleJump;
    
    bool isAlive = true;
    SpriteRenderer mySpriteRenderer;

    void Start()
    {
        myRigibody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnFire(InputValue value)
    {
        if(!isAlive){ return;};
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void Update()
    {
        if(!isAlive){ return;};
        Run();
        FlipSprite();
        Attack();
        Die();
        // Climbing();
    }

    void OnMove(InputValue value)
    {
        if(!isAlive){ return;}
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(!isAlive){ return;}
        if(isTouchingGround())
        {
            myRigibody.linearVelocity = new Vector2(0f, jumpAmount);
            doubleJump = true;
        }
        else if(doubleJump)
        {
            myRigibody.linearVelocity = new Vector2(0f, jumpAmount);            
            doubleJump = false;
        }
    }

    bool isTouchingGround()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return true;}
        else{ return false;}
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speedAmount, myRigibody.linearVelocityY);
        myRigibody.linearVelocity = playerVelocity ;

        if (moveInput.x!=0)
        {
            myAnimator.SetBool("isWalking" , true);
        }
        else
        {
            myAnimator.SetBool("isWalking" , false);
        }
        
    }
    
    void FlipSprite()
    {
        // ** Alternative Way ** 

        // bool playerHasHorizontalSpeed = Mathf.Abs(myRigibody.linearVelocity.x) > Mathf.Epsilon;

        // if(playerHasHorizontalSpeed)
        // {
        //     transform.localScale = new Vector2(Mathf.Sign(myRigibody.linearVelocity.x), 1f); 
        // }

        if (moveInput.x!=0)
        {
            transform.localScale = new Vector2(Math.Sign(myRigibody.linearVelocity.x),1f);
        }
    }

    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            CameraShakerHandler.Shake(explosionShakeData);
            mySpriteRenderer.color = deathColor;
            myRigibody.linearVelocity = deathKick;
        }
    }

    void Attack()
    {
        if(Input.GetKey(KeyCode.F))
        {
            myAnimator.SetTrigger("Attack1");

            // Detect enemy
            Collider2D [] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemiesLayer);

            // Hit enemy
            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit any enemy");
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null){ return;}
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }


    // ** Climbing Function (But this game hasn't) ** 

    // void Climbing()
    // {
    //     if(myRigibody.IsTouchingLayers(LayerMask.GetMask("Climb")))
    //     {
    //         if(Input.GetKey(KeyCode.W))
    //         {
    //             myRigibody.linearVelocity = new Vector2 (0f, climbingAmount);
    //         }
    //         else if(Input.GetKey(KeyCode.S))
    //         {
    //             myRigibody.linearVelocity = new Vector2(0f, -climbingAmount);
    //         }
    //     }
    // }

}
