using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicalKit : MonoBehaviour
{
    private int amountHeals = 15;
    private int timeOfDestruction = 5;
    
    void Start()
    {
        StartCoroutine(DestroyMedicalKit());
    }

    private void OnTriggerEnter(Collider collisionObject)
    {
        if(collisionObject.tag == Tags.Player)
        {
            collisionObject.GetComponent<ControlPlayer>().HealLife(amountHeals);
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator DestroyMedicalKit()
    {
        yield return new WaitForSeconds(timeOfDestruction);
        this.gameObject.SetActive(false);
    }
}
