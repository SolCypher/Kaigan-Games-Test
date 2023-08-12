using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptionPoint : MonoBehaviour
{
    [Header("Camera Behaviour")]
    public CameraFollow camFollow;
    public bool camFollowTank;

    [Header("Target Tank")]
    public Transform targetPosition;
    public Vector3 targetVelocity;

    [Header("My Tank")]
    public Transform selfPosition;
    public Vector3 selfVelocity;

    [Header("My Tank's Properties")]
    public float bulletSpeed;
    public Vector3 interceptPosition;
    public KeyCode fireKey = KeyCode.Space;
    public GameObject cannonProjectile;
    public Transform firePoint;

    Rigidbody selfRb;
    Rigidbody targetRb;

    private void Update() {
        // Set each Tank's Velocity + freeze their Rotation
        selfRb = GetComponent<Rigidbody>();
        selfRb.velocity = selfVelocity;
        selfRb.freezeRotation = true;
        targetRb = targetPosition.GetComponent<Rigidbody>();
        targetRb.velocity = targetVelocity;
        targetRb.freezeRotation = true;

        if(Input.GetKeyUp(fireKey)){
           if(CalculateInterceptPosition(selfPosition.position, selfVelocity, targetPosition.position, targetVelocity, bulletSpeed, out interceptPosition)){
                ShootCannon();

            }else{
                Debug.Log("Target's too fast!");

            }

        }


    }

    void ShootCannon(){
        GameObject projectile = Instantiate(cannonProjectile, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (interceptPosition - firePoint.position).normalized * bulletSpeed;
        
    }

    bool CalculateInterceptPosition(Vector3 selfPosition, Vector3 selfVelocity, Vector3 targetPosition, Vector3 targetVelocity, float bulletSpeed, out Vector3 interceptPosition)
    {
        // // Calculate relative velocity
        // Vector3 relativeVelocity = targetVelocity - selfVelocity;

        // // Calculate time of flight for projectile to intercept
        // float timeToIntercept = Vector3.Distance(selfPosition, targetPosition) / bulletSpeed;
        // // Debug.Log(timeToIntercept);

        // // Calculate the expected position of the target when the projectile arrives
        // interceptPosition = targetPosition + relativeVelocity * timeToIntercept;

        // // Check if the intercept position is reachable within the time it would take the bullet to reach that distance
        // if (Vector3.Distance(selfPosition, interceptPosition) / bulletSpeed <= timeToIntercept)
        // {
        //     return true; // Successful interception
        // }
        // else
        // {
        //     return false; // Target is moving too fast to intercept
        // }


        // Vector3 relativePosition = targetPosition - selfPosition;
        // Vector3 relativeVelocity = targetVelocity - selfVelocity;

        // float timeToIntercept = relativePosition.magnitude / bulletSpeed;

        // interceptPosition = targetPosition + relativeVelocity * timeToIntercept;

        // Vector3 toIntercept = interceptPosition - selfPosition;
        // turretAngle = Vector3.SignedAngle(Vector3.forward, toIntercept, Vector3.up);

        // return true;
        


        // Still have Flaw (if the target is faster OR same speed but target position is ahead, will never hit)
        // if target slower, more likeky to hit but can miss too
        // Never return false i think
        Vector3 relativePosition = targetPosition - selfPosition;
        Vector3 relativeVelocity = targetVelocity - selfVelocity;

        float timeToIntercept = relativePosition.magnitude / (bulletSpeed + relativeVelocity.magnitude);
        // Debug.Log(timeToIntercept);
        
        if (timeToIntercept <= 0 || float.IsNaN(timeToIntercept) || float.IsInfinity(timeToIntercept))
        {
            interceptPosition = Vector3.zero;
            return false; // Target's too fast or invalid values
        }

        interceptPosition = targetPosition + targetVelocity * timeToIntercept;
        return true;
        




    }
}
