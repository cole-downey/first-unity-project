using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class PlatformScript : MonoBehaviour
{
    //public bool fall = false;
    private Rigidbody rb;
    public GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = gameObject.GetComponent<Rigidbody>();
        Fall(false);
    }

    void Update()
    {
        if(transform.position.y < -250) Destroy(gameObject);
    }

    public IEnumerator Rise(float distance)
    {
        float moved = 0;
        float totalTime = 2.5f;
        while(moved < distance)
        {
            float moveAmount = distance * Time.deltaTime / totalTime;
            if (moveAmount + moved > distance) moveAmount = distance - moved;
            transform.Translate(0, moveAmount, 0);
            moved += moveAmount;
            yield return null;
        }

        gm.PlatNumber++;
    }

    public void Fall(bool fall = true)
    {
        rb.isKinematic = !fall;
        rb.useGravity = fall;
    }
}
