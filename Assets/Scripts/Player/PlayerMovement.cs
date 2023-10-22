using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    // private Animator animator;
    // private float dirX;
    [SerializeField] private GameObject terrainGenerator;
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float linearDrag;
    [SerializeField] private float gravityForce;
    [SerializeField] private float gravityDecreaseRate;
    [SerializeField] private LayerMask forest;
    [SerializeField] private LayerMask ocean;
    [SerializeField] private LayerMask desert;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        // animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * movespeed, rb.velocity.y, 0);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector3(0, jumpForce, 0);
        }

        if (IsUnderwater())
        {
            if(rb.gravityScale > 0.0f)
            {
                var newValue = rb.gravityScale - Time.deltaTime * gravityDecreaseRate;
                rb.gravityScale = newValue < 0.0f ? 0.0f : newValue;
            }

            rb.velocity = new Vector3(rb.velocity.x, Input.GetAxisRaw("Vertical") * movespeed, 0);
        }
        else
        {
            rb.gravityScale = gravityForce;
        }

        if (IsOnQuicksand())
        {
            rb.drag = 20;
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector3(0, jumpForce, 0);
            }
        }
        else 
        {
            rb.drag = linearDrag;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            terrainGenerator.GetComponent<TerrainGenerator>().PickUpItem();
        }
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, forest);
    }

    bool IsUnderwater()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, ocean);
    }

    bool IsOnQuicksand()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, desert);
    }
}
