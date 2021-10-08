using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    private Vector3 direction;
    private const float speed = 10.0f;
    private const float ballSize = 0.5f;

    private const float playerWidth = 1.5f;
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

        collideWithPlayer(player, deltaX, deltaY);

        transform.position += speed * Time.deltaTime * direction;
    }

    public void Reset()
    {
        transform.position -= transform.position;
        direction = getInitialDirection();
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

    private void collideWithPlayer(GameObject player, float deltaX, float deltaY)
    {
        bool inHorizontalIntersection = Mathf.Abs(player.transform.position.y - transform.position.y - deltaY)
                                < playerWidth / 2 + ballSize / 2;
        bool inVerticalIntersection = Mathf.Abs(player.transform.position.x - transform.position.x - deltaX)
                                      < playerThickness / 2 + ballSize / 2;
        if (transform.position.x < 0 
            && inHorizontalIntersection && inVerticalIntersection && direction.x < 0)
        {
            
            Bounce(true);
            
        }

        /*if (transform.position.x < 0)
        {
            float dx1 = transform.position.x - player.transform.position.x - playerThickness / 2 - ballSize / 2;
            if (Mathf.Abs(transform.position.y + dx1 * direction.y - player.transform.position.y)
                < playerWidth / 2 + ballSize / 2)
            {
                Bounce(true);
            }
        }*/
    }

    private Vector3 getInitialDirection()
    {
        float initX = Random.Range(0.38268f, 0.92388f);
        return new Vector3(-initX, Mathf.Sqrt(1 - initX * initX), 0);
    }

    private bool inGates(float deltaX, float deltaY)
    {
        return Mathf.Abs(transform.position.y - deltaY) < Game.gatesWidth / 2 - ballSize / 2;
    }
}
