using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float JumpForce;

    [SerializeField]
    bool isGrounded = false;
    Rigidbody2D RB;

    private void Awake()
    {
        this.RB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.isGrounded)
            {
                this.RB.AddForce(Vector2.up * this.JumpForce);
                this.isGrounded = false;
            }
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this.isGrounded = true;
        }
    }
}
