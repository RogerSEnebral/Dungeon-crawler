using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Mover
{
    static public void Move(Rigidbody rigidbody, Vector3 direction, float linearSpeed)
    {
        float horizontalSpeed = linearSpeed * direction.x;
        float ySpeed = rigidbody.velocity.y + linearSpeed * direction.y;
        float verticalSpeed = linearSpeed * direction.z;
        Vector3 velocity = new Vector3(horizontalSpeed, ySpeed, verticalSpeed);
        rigidbody.velocity = velocity;
    }

    static public void Rotate(Rigidbody rigidbody, Vector3 direction, float angularSpeed)
    {
        Quaternion from = rigidbody.rotation;
        Quaternion to = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion rotation = Quaternion.RotateTowards(from, to, angularSpeed);
        rigidbody.MoveRotation(rotation);
    }
}
