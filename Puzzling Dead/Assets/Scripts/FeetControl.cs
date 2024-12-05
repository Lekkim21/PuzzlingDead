using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetControl : MonoBehaviour
{
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool isGrounded()
    {
        return grounded;
    }

    public void startJump() {
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") 
            grounded = true;
    }
}
