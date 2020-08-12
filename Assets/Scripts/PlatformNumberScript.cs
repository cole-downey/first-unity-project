using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformNumberScript : MonoBehaviour
{
    public GameManager gm;
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = $"Level: {gm.PlatNumber}";
    }
}
