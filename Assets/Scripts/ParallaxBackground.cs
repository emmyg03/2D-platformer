using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private Transform theCam;
    public Transform sky, treeLine;
    [Range(0f, 1f)]
    public float parallaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // We don't want to modify the position of the camera in the z plane, so keep it sky.position.z,  rather than following the main camera
        sky.position = new Vector3(theCam.position.x, theCam.position.y, sky.position.z);

        // Move the treeline horizontally at a fraction of the camera's speed so it is slightly slower than the sky
        treeLine.position = new Vector3(theCam.position.x * parallaxSpeed, theCam.position.y, treeLine.position.z);
    }
}
