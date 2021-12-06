using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 desiredVelocity;

    public float rotationSpeed;
    
    public void OnUpdate()
    {
        velocity = desiredVelocity;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(velocity), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;
    }
}
