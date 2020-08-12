using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    void Update()
    {
        if(transform.position.y < -250) Destroy(gameObject);
    }

    
}
