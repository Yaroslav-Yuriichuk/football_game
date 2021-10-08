using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static float fieldWidth = 8.0f;
    public static float fieldLength = 12.0f;
    public static float gatesWidth = 4.0f;
    public static float borderThickness = 1.0f;
    /*private GameObject ball;
    private GameObject player;
    private GameObject enemy;*/

    public static int PlayerScore;
    public static int EnemyScore;
    // Start is called before the first frame update
    void Awake()
    {
        /*ball = GameObject.Find("Ball");
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");*/

        PlayerScore = 0;
        EnemyScore = 0;
    }

    // Update is called once per frame
    public static void UpdateScore()
    {
        Text txt = GameObject.Find("ScoreBoard").GetComponent<Text>();
        txt.text = $"You    {PlayerScore}:{EnemyScore}    Enemy";
    }
}
