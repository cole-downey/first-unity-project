using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    private GameManager gm;
    
    public int MoveSpeed = 500;

    public float JumpAmount = 10;
    public bool IsGrounded = false;
    public float TerminalVelocity = 100;
    public float MagVelo;

    // Update is called once per frame
    // used fixedupdate if using physics

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey("w")) rb.AddForce(0, 0, MoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        if (Input.GetKey("s")) rb.AddForce(0, 0, -MoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        if (Input.GetKey("a")) rb.AddForce(-MoveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        if (Input.GetKey("d")) rb.AddForce(MoveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        if (Input.GetKey(KeyCode.Space)) Jump();
        float grav = (float) -9.8 * 2;
        if(!IsGrounded) rb.AddForce(0, grav, 0);
        if(rb.position.y < -40) gm.GameOver("Fall");

        /*
        var CurrentVelocity = rb.velocity;
        if (rb.velocity.z >= TerminalVelocity)
        {
            CurrentVelocity.z = TerminalVelocity;
            rb.velocity = CurrentVelocity;
        }
        else if (rb.velocity.z <= -TerminalVelocity)
        {
            CurrentVelocity.z = -TerminalVelocity;
            rb.velocity = CurrentVelocity;
        }
        CurrentVelocity = rb.velocity;
        if (rb.velocity.x >= TerminalVelocity)
        {
            CurrentVelocity.x = TerminalVelocity;
            rb.velocity = CurrentVelocity;
        }
        else if (rb.velocity.x <= -TerminalVelocity)
        {
            CurrentVelocity.x = -TerminalVelocity;
            rb.velocity = CurrentVelocity;
        }
        */
        MagVelo = rb.velocity.magnitude;
    }
    
    void Jump()
    {
        //if (IsGrounded) rb.AddForce(0, JumpAmount, 0, ForceMode.VelocityChange);
        
        var JumpVector = rb.velocity;
        JumpVector.y = JumpAmount;
        if (IsGrounded)
        {
            rb.velocity = JumpVector;
        }

    }
}
