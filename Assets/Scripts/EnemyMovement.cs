using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public float Speed = 10;
    public Rigidbody rb;
    public bool Close = false;
    public bool Far;


    void Start()
    {
        Player = FindObjectOfType<PlayerMovement>().transform;
    }
    void FixedUpdate()
    {
        var p = FindObjectOfType<PlayerMovement>().transform;
        Close = Vector3.Distance(transform.position, Player.position) <= 5 ? true : false;
        Far = Vector3.Distance(transform.position, Player.position) >= 30 ? true : false;
        transform.LookAt(Player);
        float realSpeed = Speed * (Close ? 2 : 5);
        Stabilize();
        rb.AddForce(transform.forward * realSpeed * Time.deltaTime);

        //rb.velocity = transform.forward * Speed;
    }

    void Stabilize()
    {
        var dot = Vector3.Dot(transform.right, rb.velocity);
        rb.AddForce(transform.right * -1 * dot);
    }
}
