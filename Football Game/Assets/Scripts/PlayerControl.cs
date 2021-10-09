using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const float speed = 7.5f;
    public static float allowedMoveDistance = 2.7f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y <= allowedMoveDistance)
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y >= - allowedMoveDistance)
        {
            transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
        }
    }

    public void Reset()
    {
        transform.position -= new Vector3(0, transform.position.y, 0);
    }
}
