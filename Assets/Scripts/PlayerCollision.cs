using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement move;
    public Material mat;
    private bool TouchingGround = false;
    private bool TouchingBlock = false;
    private GameManager gm;

    void Start()
    {
        mat.color = Color.red;
        gm = FindObjectOfType<GameManager>();
    }
    void OnCollisionEnter(Collision collinfo)
    {
        //var purp = new Color(129, 0, 255, 255);
        if (collinfo.collider.tag == "Ground") TouchingGround = true;
        if (collinfo.collider.tag == "Obstacle")
        {
            TouchingBlock = true;
            
        }

        if (collinfo.collider.tag == "Enemy")
        {
            
            gm.GameOver();
        }

            

        move.IsGrounded = TouchingBlock | TouchingGround;
    }

    void OnCollisionExit(Collision collinfo)
    {
        if (collinfo.collider.tag == "Ground") TouchingGround = false;
        if (collinfo.collider.tag == "Obstacle") TouchingBlock = false;
        move.IsGrounded = TouchingBlock | TouchingGround;
    }
}
