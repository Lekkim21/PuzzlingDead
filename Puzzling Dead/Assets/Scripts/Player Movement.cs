using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Test
    [SerializeField] private float speed;

    [SerializeField] private int playerNumber;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private int activePlayer;

    private void Awake()
    {
        //Grab refernces for rigidbody and animation
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        activePlayer = 0;
    }

    private void Update()
    {
        if(playerNumber == activePlayer) {
            float horizontalInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

            //Flips player depending on horizontal input
            if (horizontalInput > 0.01f)
                transform.localScale = new Vector3(6,6,6);
            else if (horizontalInput < -0.01f)
                transform.localScale = new Vector3(-6, 6, 6);

            if (Input.GetKey(KeyCode.Space) && grounded)
                Jump();

            // set animator parameters
            anim.SetBool("Walk", horizontalInput != 0);
            anim.SetBool("Grounded", grounded);
        }
    }

    private void Jump()
    {
        if(playerNumber == activePlayer) {

            body.velocity = new Vector2((body.velocity.x), speed);
            anim.SetTrigger("jump");
            grounded = false;
        }
    }private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") 
            grounded = true;
    }
}

