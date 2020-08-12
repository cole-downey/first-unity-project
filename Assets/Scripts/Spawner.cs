using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private readonly System.Random _rnd = new System.Random();

    public GameObject Enemy, Block, Player, Platform;
    public GameManager gm;
    private int _prevDirection = 0;

    public void SpawnEnemy(GameObject CurrentPlat)
    {
        Vector3 UpperBounds = new Vector3(CurrentPlat.transform.localScale.x / 2 + CurrentPlat.transform.position.x, 0,
            CurrentPlat.transform.localScale.z / 2 + CurrentPlat.transform.position.z);
        Vector3 LowerBounds = new Vector3(-CurrentPlat.transform.localScale.x / 2 + CurrentPlat.transform.position.x, 0,
            -CurrentPlat.transform.localScale.z / 2 + CurrentPlat.transform.position.z);
        int x = _rnd.Next((int)LowerBounds.x, (int)UpperBounds.x);
        int y = 20;
        int z = _rnd.Next((int)LowerBounds.z, (int)UpperBounds.z);
        Instantiate(Enemy, new Vector3(x, y, z), Quaternion.identity);

    }

    public void SpawnBlock(Vector3 LowerBounds, Vector3 UpperBounds)
    {
        Vector3 size = new Vector3(_rnd.Next(2, 10), _rnd.Next(2, 10), _rnd.Next(2, 10));
        float x = _rnd.Next((int)LowerBounds.x, (int)UpperBounds.x);
        float y = _rnd.Next(30, 45) + size.y / 2;
        float z = _rnd.Next((int)LowerBounds.z, (int)UpperBounds.z);
        var b = Instantiate(Block, new Vector3(x, y, z), Quaternion.identity);
        b.transform.localScale = size;
        b.GetComponent<Rigidbody>().mass = size.x * size.y * size.z / 4;
    }

    public GameObject SpawnPlat(GameObject PrevPlat)
    {
        GameObject CurrentPlat;
        if (PrevPlat != null)
        {
            int direction = (gm.PlatNumber == 1) ? 0 : _rnd.Next(0, 2);
            if (direction == 1)
            {
                if (_prevDirection > 0) direction = _prevDirection;
                else direction = _rnd.Next(1, 3);
            }
            
            Vector3 pos = new Vector3(0, PrevPlat.transform.position.y - 100, 0);
            switch (direction)
            {
                case 0: // straight
                    pos.x = PrevPlat.transform.position.x;
                    pos.z = PrevPlat.transform.position.z + PrevPlat.transform.localScale.z + 20;
                    break;
                case 1: // left
                    pos.x = PrevPlat.transform.position.x - PrevPlat.transform.localScale.x - 20;
                    pos.z = PrevPlat.transform.position.z;
                    break;
                case 2: // right
                    pos.x = PrevPlat.transform.position.x + PrevPlat.transform.localScale.x + 20;
                    pos.z = PrevPlat.transform.position.z;
                    break;
            }
            CurrentPlat = Instantiate(Platform, pos, Quaternion.identity);
            StartCoroutine(CurrentPlat.GetComponent<PlatformScript>().Rise(100));
            _prevDirection = direction;
        }
        else
        {
            CurrentPlat = gm.CurrentPlat;
        }
        Vector3 uBounds = new Vector3(CurrentPlat.transform.localScale.x / 2 + CurrentPlat.transform.position.x, 0,
            CurrentPlat.transform.localScale.z / 2 + CurrentPlat.transform.position.z);
        Vector3 lBounds = new Vector3(-CurrentPlat.transform.localScale.x / 2 + CurrentPlat.transform.position.x, 0,
            -CurrentPlat.transform.localScale.z / 2 + CurrentPlat.transform.position.z);
        int NumBlocks = (int) CurrentPlat.transform.localScale.x * (int) CurrentPlat.transform.localScale.z / 300;
        for (int i = 0; i < NumBlocks; i++)
        {
            SpawnBlock(lBounds, uBounds);
        }

        return CurrentPlat;
    }
}
