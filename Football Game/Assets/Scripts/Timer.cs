using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int seconds;
    private Text txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = this.GetComponent<Text>();
        seconds = 0;
        InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
    }

    void UpdateTimer()
    {
        if (++seconds % 60 < 10)
        {
            txt.text = $"{seconds / 60}:0{seconds % 60}";
        }
        else
        {
            txt.text = $"{seconds / 60}:{seconds % 60}";
        }
    }
}
