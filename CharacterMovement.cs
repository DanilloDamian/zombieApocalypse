using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody myRigidbody;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = Vector3.zero;
    }

    public void Move(Vector3 direction, float force)
    {        
        
        myRigidbody.MovePosition(myRigidbody.position +(direction.normalized * force *Time.deltaTime));       
        
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion newRotate = Quaternion.LookRotation(direction);
        myRigidbody.MoveRotation(newRotate);
    }

    public void Die()
    {
        myRigidbody.isKinematic = false;
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.constraints = RigidbodyConstraints.None;
        GetComponent<Collider>().enabled = false;
    }
}
