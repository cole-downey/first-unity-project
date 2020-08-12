using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 Offset;
    private Vector3 _pos;
    public bool PlayerFell;

    // Update is called once per frame
    void Start()
    {
        _pos = Player.position + Offset;
        transform.position = _pos;
        transform.LookAt(Player);
    }
    void Update()
    {
        if (PlayerFell)
        {
            _pos.x = Player.position.x + Offset.x;
            _pos.z = Player.position.z + Offset.z;
        }
        else _pos = Player.position + Offset;

        transform.position = _pos;
        if (PlayerFell) transform.LookAt(Player);
    }
}
