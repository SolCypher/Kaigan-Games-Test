using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    enum EngineState{
        Accelerate,
        Idle,
        Brake,

    }

    [Header("Car Properties")]
    public float distanceToTarget = 0;
    public float currentSpeed;
    public float forwardAcceleration;
    public float brakeDeceleration;
    public float maxSpeed;

    float currDist;

    private void OnValidate() {
        currDist = distanceToTarget;
    }

    void Update()
    {
        if(distanceToTarget == 0){
            currentSpeed = 0;
        }

        if(DetermineEngineState(distanceToTarget, currentSpeed, forwardAcceleration, brakeDeceleration, maxSpeed) == EngineState.Accelerate){
            if(currentSpeed <= maxSpeed){
                currentSpeed += forwardAcceleration * Time.deltaTime;
                transform.position += Vector3.right * currentSpeed * Time.deltaTime;
                // Debug.Log("Accelerate");

            }

        }else if(DetermineEngineState(distanceToTarget, currentSpeed, forwardAcceleration, brakeDeceleration, maxSpeed) == EngineState.Idle){
            // Debug.Log("Car is maintaning speed at " + currentSpeed);

        }else if(DetermineEngineState(distanceToTarget, currentSpeed, forwardAcceleration, brakeDeceleration, maxSpeed) == EngineState.Brake){
            if(currentSpeed > 0){
                currentSpeed -= brakeDeceleration * Time.deltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0);
                transform.position += Vector3.right * currentSpeed * Time.deltaTime;
                // Debug.Log("Decelerate");

            }

        }
        // Debug.Log("State: " + DetermineEngineState(distanceToTarget, currentSpeed, forwardAcceleration, brakeDeceleration, maxSpeed));

        // if(currDist <= 0){
        //     transform.position = transform.position;
        // }

    }

    EngineState DetermineEngineState(float distanceToTarget, float currentSpeed, float forwardAcceleration, float brakeDeceleration, float maxSpeed){
        // Calculate the distance in which the car should stop
        float stoppingDistance = (currentSpeed * currentSpeed) / (2 * brakeDeceleration);
        // Debug.Log(stoppingDistance);
        currDist -= currentSpeed * Time.deltaTime;

        if (currDist >= stoppingDistance){
            if (currentSpeed < maxSpeed){
                return EngineState.Accelerate;

            }

        }else if(currDist < stoppingDistance){
            return EngineState.Brake;

        }

        return EngineState.Idle;
    }

}
