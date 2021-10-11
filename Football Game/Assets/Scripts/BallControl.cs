using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    private Vector3 direction;
    private const float speed = 11.0f;
    private const float deltaSpeed = 0.2f;
    private const float ballSize = 0.5f;

    private const float playerWidth = 1.0f;
    private const float playerThickness = 0.5f;
    
    private GameObject player;
    private GameObject enemy;
    private AI enemyAI;
    private Game game;

    private Vector3 animationDelta;
    // private GameObject game;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        enemyAI = (AI)enemy.GetComponent("AI");
        game = (Game)GameObject.Find("Main Camera").GetComponent("Game");
        animationDelta = new Vector3(0.01f, 0.01f, 0.0f);

        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = speed * Time.deltaTime * direction.x;
        float deltaY = speed * Time.deltaTime * direction.y;

        if (Mathf.Abs(transform.position.x + deltaX) > Game.fieldLength / 2 - ballSize / 2)
        {
            if (InGates(0, 0))
            {
                if (Mathf.Abs(transform.position.y + deltaY) > Game.gatesWidth / 2 - ballSize / 2)
                {
                    Bounce(false);
                }
                if (Mathf.Abs(transform.position.x + deltaX)
                    > Game.fieldLength / 2 + Game.borderThickness - ballSize / 2)
                {
                    if (transform.position.x > 0)
                    {
                        game.AddScore(true);
                    }
                    else
                    {
                        game.AddScore(false);
                    }
                }
            }
            else if (!InGates(deltaX, deltaY))
            {
                Bounce(true);
            }
        }

        if (Mathf.Abs(transform.position.y + deltaY) > Game.fieldWidth / 2 - ballSize / 2
            && !InGates(deltaX, deltaY))
        {
            Bounce(false);
        }

        CollideWithPlayer(deltaX, deltaY);
        CollideWithEnemy(deltaX, deltaY);

        transform.position += speed * Time.deltaTime * direction;
    }

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public void Reset()
    {
        transform.position -= transform.position;
        direction -= direction;
        StartCoroutine("AnimateAppearing");
    }
    
    private IEnumerator AnimateAppearing()
    {
        transform.localScale -= transform.localScale;
        // transform.localScale.y -= transform.localScale;
        while (transform.localScale.x < 0.5f)
        {
            transform.localScale += animationDelta;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        // transform.localScale = new Vector3(0.5f, 0.5f, 0.0f);
        SetInitialDirection();
    }

    public void Bounce(bool isFromVertical)
    {
        if (isFromVertical)
        {
            direction.x *= -1;
        }
        else
        {
            direction.y *= -1;
        }
    }

    private void CollideWithPlayer(float deltaX, float deltaY)
    {
        bool inHorizontalIntersection = Mathf.Abs(player.transform.position.y - transform.position.y - deltaY)
                                < playerWidth / 2 + ballSize / 2;
        bool inVerticalIntersection = Mathf.Abs(player.transform.position.x - transform.position.x - deltaX)
                                      < playerThickness / 2 + ballSize / 2;

        if (transform.position.x < 0  && inHorizontalIntersection && inVerticalIntersection)
        {
            float dx1 = (transform.position.x - player.transform.position.x - playerThickness / 2 - ballSize / 2) / (- direction.x);
            if (Mathf.Abs(transform.position.y + dx1 * direction.y - player.transform.position.y)
                < playerWidth / 2 + ballSize / 2
                && direction.x < 0)
            {
                Bounce(true);
                if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < PlayerControl.allowedMoveDistance)
                {
                    direction.y += deltaSpeed;
                    if (SmallerThanIdentityVector()) direction = CalculateIdentityVector(direction.x, direction.y < 0);
                }
                if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > - PlayerControl.allowedMoveDistance)
                {
                    direction.y -= deltaSpeed;
                    if (SmallerThanIdentityVector()) direction = CalculateIdentityVector(direction.x, direction.y < 0);
                }
            }
            else
            {
                Bounce(false);
            }

        }
    }

    private void CollideWithEnemy(float deltaX, float deltaY)
    {
        bool inHorizontalIntersection = Mathf.Abs(enemy.transform.position.y - transform.position.y - deltaY)
                                < playerWidth / 2 + ballSize / 2;
        bool inVerticalIntersection = Mathf.Abs(enemy.transform.position.x - transform.position.x - deltaX)
                                      < playerThickness / 2 + ballSize / 2;

        if (transform.position.x > 0 && inHorizontalIntersection && inVerticalIntersection)
        {
            float dx1 = (enemy.transform.position.x - transform.position.x - playerThickness / 2 - ballSize / 2) / direction.x;
            if (Mathf.Abs(transform.position.y + dx1 * direction.y - enemy.transform.position.y)
                < playerWidth / 2 + ballSize / 2
                && direction.x > 0)
            {
                Bounce(true);
                if (enemyAI.DirectionY == 1)
                {
                    direction.y += deltaSpeed;
                    if (SmallerThanIdentityVector()) direction = CalculateIdentityVector(direction.x, direction.y < 0);
                }
                if (enemyAI.DirectionY == 1)
                {
                    direction.y -= deltaSpeed;
                    if (SmallerThanIdentityVector()) direction = CalculateIdentityVector(direction.x, direction.y < 0);
                }

            }
            else
            {
                Bounce(false);
            }

        }
    }

    private void SetInitialDirection()
    {
        // float initX = Random.Range(0.38268f, 0.92388f);
        float initX = Random.Range(0.6f, 0.92388f);
        direction = CalculateIdentityVector(initX, Random.Range(0.0f, 1.0f) > 0.5f);
        direction.x *= -1;
    }

    private Vector3 CalculateIdentityVector(float x, bool isMovingDown)
    {
        float coeficientY = (isMovingDown) ? -1.0f : 1.0f;
        return new Vector3(x, coeficientY * Mathf.Sqrt(1 - x * x), 0);
    }

    private bool SmallerThanIdentityVector()
    {
        return direction.x * direction.x + direction.y * direction.y < 1.0f;
    }

    private bool InGates(float deltaX, float deltaY)
    {
        return Mathf.Abs(transform.position.y - deltaY) < Game.gatesWidth / 2 - ballSize / 2;
    }
}
