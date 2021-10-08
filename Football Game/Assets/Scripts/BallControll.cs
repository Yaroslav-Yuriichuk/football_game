using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    private Vector3 direction;
    private const float speed = 10.0f;
    private const float ballSize = 0.5f;

    private const float fieldWidth = 8.0f;
    private const float fieldLength = 12.0f;
    private const float gatesWidth = 4.0f;
    private const float borderThickness = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        direction = getInitialDirection();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = speed * Time.deltaTime * direction.x;
        float deltaY = speed * Time.deltaTime * direction.y;

        if (Mathf.Abs(transform.position.x + deltaX) > fieldLength / 2 - ballSize / 2)
        {
            if (inGates(0, 0))
            {
                if (Mathf.Abs(transform.position.y + deltaY) > gatesWidth / 2 - ballSize / 2)
                {
                    Bounce(false);
                }
                if (Mathf.Abs(transform.position.x + deltaX) > fieldLength / 2 + borderThickness - ballSize / 2)
                {
                    transform.position -= transform.position;
                }
            }
            else if (!inGates(deltaX, deltaY))
            {
                Bounce(true);
            }
        }

        if (Mathf.Abs(transform.position.y + deltaY) > fieldWidth / 2 - ballSize / 2
            && !inGates(deltaX, deltaY))
        {
            Bounce(false);
        }

        /*if (transform.position.x + deltaX > fieldLength / 2 - ballSize / 2)
        {
            isBounced = Bounce(true);
            float componentToVerticalX = fieldLength / 2 - ballSize / 2 - transform.position.x;
            deltaX = 2 * componentToVerticalX - deltaX;
        }
        else if (transform.position.x + deltaX < - (fieldLength / 2 - ballSize / 2))
        {
            isBounced = Bounce(true);
            float componentToVerticalX = - (fieldLength / 2 - ballSize / 2) - transform.position.x;
            deltaX = 2 * componentToVerticalX - deltaX;
        }

        if (transform.position.y + deltaY > fieldWidth / 2 - ballSize / 2)
        {
            isBounced = Bounce(false);
            float componentToVerticalY = fieldWidth / 2 - ballSize / 2 - transform.position.y;
            deltaY = 2 * componentToVerticalY - deltaY;
        }
        else if (transform.position.y + deltaY < - (fieldWidth / 2 - ballSize / 2))
        {
            isBounced = Bounce(false);
            float componentToVerticalY = - (fieldWidth / 2 - ballSize / 2) - transform.position.y;
            deltaY = 2 * componentToVerticalY - deltaY;
        }*/

        transform.position += speed * Time.deltaTime * direction;
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

    private Vector3 getInitialDirection()
    {
        float initX = Random.Range(0.38268f, 0.92388f);
        Debug.Log(initX);
        Debug.Log(Mathf.Sqrt(1 - initX * initX));
        return new Vector3(initX, Mathf.Sqrt(1 - initX * initX), 0);
    }

    private bool inGates(float deltaX, float deltaY)
    {
        return Mathf.Abs(transform.position.y - deltaY) < gatesWidth / 2 - ballSize / 2;
    }
}
