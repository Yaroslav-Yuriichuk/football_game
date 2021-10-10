using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private BallControl ball;

    private int directionY; // 1 - Up, -1 - DOWN
    private const float speed = 4.7f;

    // Start is called before the first frame update
    void Start()
    {
        ball = (BallControl)GameObject.Find("Ball").GetComponent("BallControl");
    }

    // Update is called once per frame
    void Update()
    {
        float deltaY = speed * Time.deltaTime * ball.Direction.y * Random.Range(0.69f, 1.0f);
        if (Mathf.Abs(transform.position.y + deltaY) < PlayerControl.allowedMoveDistance)
        {
            transform.position += new Vector3(0, deltaY, 0);
            if (deltaY > 0)
            {
                directionY = 1;
            }
            else if (deltaY < 0)
            {
                directionY = -1;
            }

        }
    }

    public int DirectionY
    {
        get
        {
            return directionY;
        }
    }

    public void Reset()
    {
        transform.position -= new Vector3(0, transform.position.y, 0);
    }
}
