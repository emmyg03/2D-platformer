using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public bool freezeVertical, freezeHorizontal;
    private Vector3 positionStore;

    public bool clampPosition;
    // clampMin = lower left corner of screen (as far down and left as you want the camera to reach during the level)
    // clampMax = upper right corner of screen (as far up and right as you want the camera to get)
    public Transform clampMin, clampMax;
    private float halfWidth, halfHeight;
    public Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        // positionStore = whatever location the camera is in at the start
        positionStore = transform.position;

        // For the sake of organization in Unity, clampMin and clampMax are children of the Main Camera
        // We need to change this so they don't act like children otherwise they will move around in relation to the
        // camera movement and defeat the purpose of the clamp movement functionality.
        clampMin.SetParent(null); 
        clampMax.SetParent(null);

        halfHeight = theCamera.orthographicSize;
        halfWidth = theCamera.orthographicSize * theCamera.aspect;
    }

    // Update is called once per frame
    // After update is called on every component of the scene, then LateUpdate is called. This fixes any potential
    // jerky camera movement in case the camera script file is executed before the player controller script file,
    // resulting in the camera moving before the player does.
    void LateUpdate()
    {
        // Move camera to where player is
        // Wherever our z position currently is, remain there. Follow player via x axis and y axis
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        if (freezeVertical)
        {
            // Freeze y axis position of camera to be the initial position
            transform.position = new Vector3(transform.position.x, positionStore.y, transform.position.z);
        }

        if (freezeHorizontal)
        {
            transform.position = new Vector3(positionStore.x, transform.position.y, transform.position.z);
        }

        if (clampPosition)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampMin.position.x + halfWidth, clampMax.position.x - halfWidth), 
                Mathf.Clamp(transform.position.y, clampMin.position.y + halfHeight, clampMax.position.y - halfHeight),
                transform.position.z);
        }
    }

    private void OnDrawGizmos()
    {
        if (clampPosition)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMin.position.x, clampMax.position.y, 0));
            Gizmos.DrawLine(clampMin.position, new Vector3(clampMax.position.x, clampMin.position.y, 0));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMin.position.x, clampMax.position.y, 0));
            Gizmos.DrawLine(clampMax.position, new Vector3(clampMax.position.x, clampMin.position.y, 0));
        }
    }
}
