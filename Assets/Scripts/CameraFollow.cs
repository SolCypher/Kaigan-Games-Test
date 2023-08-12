using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public InterceptionPoint interceptPoint;
    public GameObject objToFollow;
    public Vector3 camOffset = new Vector3(-5f, 5, 3);
    public float smoothTime = 0.25f;
    Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        if(interceptPoint.camFollowTank){
            Vector3 targetPos = objToFollow.transform.position + camOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        }
        
    }

}
