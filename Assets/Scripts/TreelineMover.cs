using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreelineMover : MonoBehaviour
{
    // How far an object should be off the camera before it is moved
    public float maxDistance = 22f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = transform.position.x - Camera.main.transform.position.x;

        // if camera is over to the right, transform leftmost position
        if (distance > maxDistance)
        {
            transform.position -= new Vector3(maxDistance * 2f, 0f, 0f);
        }

        // if camera is over to the left, transform rightmost position
        if (distance < -maxDistance)
        {
            transform.position += new Vector3(maxDistance * 2f, 0f, 0f);
        }
    }
}
