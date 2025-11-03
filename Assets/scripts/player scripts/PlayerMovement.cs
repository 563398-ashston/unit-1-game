using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rb;
    Animator anim;
    HelperScript helper;
    public LayerMask groundLayerMask;
    bool isGrounded;
    public GameObject weapon;
    public CoinManager cm;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundLayerMask = LayerMask.GetMask("Ground");
        helper = gameObject.AddComponent<HelperScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float xvel, yvel;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;



        if (Input.GetKey("d"))
        {
            xvel = 5;
        }
        else
        {
            xvel = 0;
        }

        if (Input.GetKey("a"))
        {
            xvel = -5;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            yvel = 10;
            print("player jumped");
            anim.SetBool("isJumping", true);
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        
        if (xvel < 0)
        {
            helper.DoFlipObject(true);
        }

        if (xvel > 0)
        {
            helper.DoFlipObject(false);
        }
        

      
        if (xvel >= 0.1f || xvel <= -0.1f)
        {
            anim.SetBool("isWalking", true);
            print("walking");
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        //check for landing back on the ground 
        if (yvel <0 && isGrounded)
        {
            anim.SetBool ("isJumping", false);
        }


        if (ExtendedRayCollisionCheck(0, 0.5f) == true)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;

        }

        if (Input.GetKeyDown("w"))
        {
            GameObject clone;
            clone = Instantiate(weapon, transform.position, Quaternion.identity);

            Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

            rb.linearVelocity = new Vector2(15, 0);

            rb.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 2, transform.position.z);
        }


        print("isgrounded=" + isGrounded + "  yvel=" + yvel );


    }


    void OnCollisionEnter2D(Collision2D col)
    {
        print("tag=" + col.gameObject.tag);

        if (col.gameObject.tag == "projectile")
        {
            print("I've been hit by a pojectile");
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        }
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
