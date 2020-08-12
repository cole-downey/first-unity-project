using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool SpawnEnemies = true;
    private bool _gameover = false;
    public int Score, PlatNumber;
    public float EnemySpawnTime = 3, PlatformSpawnTime = 30;
    private float EnemyPeriod = 0, PlatPeriod = 0, PlatFallPeriod = -1000;
    private readonly System.Random _rnd = new System.Random();


    public GameObject Player, CurrentPlat, PrevPlat, Camera;
    public Spawner Spawn;
   
    public void Start()
    {
        PlatNumber = 1;
        PrevPlat = null;
        Camera.GetComponent<FollowPlayer>().PlayerFell = false;
        Player.GetComponent<PlayerMovement>().enabled = true;
        Player.GetComponent<Rigidbody>().useGravity = true;
        Player.GetComponent<MeshRenderer>().material.color = Color.red;
        Score = 0;

        Spawn.SpawnPlat(PrevPlat);

    }

    public void Update()
    {
        if (!_gameover)
        {
            if (EnemyPeriod >= EnemySpawnTime)
            {
                if (SpawnEnemies) Spawn.SpawnEnemy(CurrentPlat);
                if (!_gameover) Score++;
                EnemyPeriod = 0;
            }

            EnemyPeriod += Time.deltaTime;

            if (PlatPeriod >= PlatformSpawnTime)
            {
                PrevPlat = CurrentPlat;
                CurrentPlat = Spawn.SpawnPlat(PrevPlat);
                PlatPeriod = 0;
                PlatFallPeriod = 0;
            }

            PlatPeriod += Time.deltaTime;

            if (PlatFallPeriod >= (PlatformSpawnTime / 1.5))
            {
                PrevPlat.GetComponent<PlatformScript>().Fall();
                PlatFallPeriod = -1000;
            }

            PlatFallPeriod += Time.deltaTime;
        }
    }


    public void GameOver(string source = "Enemy")
    {
        if (!_gameover)
        {
            _gameover = true;
            Player.GetComponent<MeshRenderer>().material.color = Color.gray;
            if (source == "Enemy")
            {
                Player.GetComponent<PlayerMovement>().enabled = false;
                Player.GetComponent<Rigidbody>().useGravity = false;
            }
            else if (source == "Fall") Camera.GetComponent<FollowPlayer>().PlayerFell = true;
            Invoke("RestartGame", 5);
        }

    }

    public void RestartGame()
    {
        _gameover = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
