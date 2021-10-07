using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = getInitialDirection();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 getInitialDirection()
    {
        Debug.Log(Random.Range(0.0f, 1.0f));
        return new Vector3(0, 0, 0);
    }
}
