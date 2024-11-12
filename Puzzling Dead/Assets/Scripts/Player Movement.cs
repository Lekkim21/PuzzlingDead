using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Test
    [SerializeField] private float speed;

    [SerializeField] private int playerNumber;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        // Grab references for Rigidbody and Animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        bool playerIsActive = playerNumber == GameManager.instance.activePlayerNr;
        float horizontalInput = 0;
        if (playerIsActive)
            horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flips player depending on horizontal input, maintaining original scale
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (Input.GetKey(KeyCode.Space) && grounded && playerIsActive)
            Jump();

        // Set animator parameters
        anim.SetBool("Walk", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);

    }

    private void Jump()
    {
        if (playerNumber == GameManager.instance.activePlayerNr)
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            anim.SetTrigger("jump");
            grounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
