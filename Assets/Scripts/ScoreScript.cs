using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public GameManager gm;

    public TextMeshProUGUI ScoreText;
   
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = $"Score: {gm.Score}";
    }
}
