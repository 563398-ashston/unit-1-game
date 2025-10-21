using UnityEngine;

public class EnemyMoveScript : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    bool isGrounded;
    public PlayerMovement playerScript;
    public LayerMask groundLayerMask;
    Animator anim;
    Rigidbody2D rb;
    float xvel;

    public void Die()
    {
        Destroy(gameObject);
    }

    public void HitPlayer(Transform playerTransform)
    {
        //FindObjectOfType<HealthScript>.TakeDamage;
    }

    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        xvel = 3;
    }

    // Update is called once per frame
    void Update()
    { 
        if (xvel < 0)
        {
            print("I am moving left");
            //anim.SetBool("isWalking", true);

            if (ExtendedRayCollisionCheck(-1, 0) == false)
            {
                xvel = 3;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (xvel > 0)
        {
            print("I am moving right");
            //anim.SetBool("isWalking", true);

            if (ExtendedRayCollisionCheck(1, 0) == false)
            {
                xvel = -3;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        rb.linearVelocity = new Vector2(xvel, 0);
    }


    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f; // length of raycast 
        bool hitSomething = false;

        //convert x and y offset into a Vector 3
        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        //cast a ray downwards 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.white;

        if (hit.collider != null)
        {
            print("player has collided with ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }

        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }
}