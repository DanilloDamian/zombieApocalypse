using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    private int amountHeals = 15;
    private int timeOfDestruction = 5;
    
    void Start()
    {
        Destroy(gameObject,timeOfDestruction);
    }

    private void OnTriggerEnter(Collider collisionObject)
    {
        if(collisionObject.tag == Tags.Player)
        {
            collisionObject.GetComponent<ControlPlayer>().HealLife(amountHeals);
            Destroy(gameObject);
        }
    }
}
