using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static float fieldWidth = 8.0f;
    public static float fieldLength = 12.0f;
    public static float gatesWidth = 4.5f;
    public static float borderThickness = 1.0f;

    public static Game game = null;

    private int playerScore;
    private int enemyScore;

    private PlayerControl player;
    private AI enemy;
    private BallControl ball;


    void Start()
    {
        player = (PlayerControl)GameObject.Find("Player").GetComponent("PlayerControl");
        enemy = (AI)GameObject.Find("Enemy").GetComponent("AI");
        ball = (BallControl)GameObject.Find("Ball").GetComponent("BallControl");

        ResetGame(true);
    }

    private void ResetGame(bool resetBall)
    {
        playerScore = 0;
        enemyScore = 0;

        player.Reset();
        enemy.Reset();
        if (resetBall) ball.Reset();

        UpdateScore();
       ((Timer)GameObject.Find("Timer").GetComponent("Timer")).Reset();
    }

    public void AddScore(bool playerScored)
    {
        if (playerScored)
        {
            playerScore++;
        }
        else
        {
            enemyScore++;
        }

        player.Reset();
        enemy.Reset();
        ball.Reset();

        UpdateScore();

        if (playerScore > 2)
        {
            IEnumerator showResult = ShowResult(true);
            // ball.Reset();
            StartCoroutine(showResult);
        }
        else if (enemyScore > 2)
        {
            IEnumerator showResult = ShowResult(false);
            StartCoroutine(showResult);
        }
    }

    public IEnumerator ShowResult(bool playerWin)
    {
        // yield return new WaitForSeconds(1.0f);
        Text txt = GameObject.Find("ScoreBoard").GetComponent<Text>();
        txt.text = (playerWin) ? "You Win" : "Enemy Win";
        yield return new WaitForSeconds(1.0f);
        ResetGame(false);
    }

    // Update is called once per frame
    private void UpdateScore()
    {
        Text txt = GameObject.Find("ScoreBoard").GetComponent<Text>();
        txt.text = $"You    {playerScore}:{enemyScore}    Enemy";
    }
}
