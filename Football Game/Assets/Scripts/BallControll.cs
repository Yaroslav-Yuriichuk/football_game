using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    private Vector3 direction;
    private const float speed = 10.0f;
    private const float deltaSpeed = 0.2f;
    private const float ballSize = 0.5f;

    private const float playerWidth = 1.0f;
    private const float playerThickness = 0.5f;
    
    private GameObject player;
    private GameObject enemy;
    // private GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = speed * Time.deltaTime * direction.x;
        float deltaY = speed * Time.deltaTime * direction.y;

        if (Mathf.Abs(transform.position.x + deltaX) > Game.fieldLength / 2 - ballSize / 2)
        {
            if (inGates(0, 0))
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
                        Game.PlayerScore += 1;
                    }
                    else
                    {
                        Game.EnemyScore += 1;
                    }
                    Reset();
                    Game.UpdateScore();
                }
            }
            else if (!inGates(deltaX, deltaY))
            {
                Bounce(true);
            }
        }

        if (Mathf.Abs(transform.position.y + deltaY) > Game.fieldWidth / 2 - ballSize / 2
            && !inGates(deltaX, deltaY))
        {
            Bounce(false);
        }

        collideWithPlayer(deltaX, deltaY);
        collideWithEnemy(deltaX, deltaY);

        transform.position += speed * Time.deltaTime * direction;
    }

    public void Reset()
    {
        transform.position -= transform.position;
        getInitialDirection();
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

    private void collideWithPlayer(float deltaX, float deltaY)
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

    private void collideWithEnemy(float deltaX, float deltaY)
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
            }
            else
            {
                Bounce(false);
            }

        }
    }

    private void getInitialDirection()
    {
        float initX = Random.Range(0.38268f, 0.92388f);
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

    private bool inGates(float deltaX, float deltaY)
    {
        return Mathf.Abs(transform.position.y - deltaY) < Game.gatesWidth / 2 - ballSize / 2;
    }
}
