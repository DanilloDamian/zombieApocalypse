using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWeapon : MonoBehaviour
{
    public GameObject Ammunition;
    public GameObject GunBarrel;
    public AudioClip GunshotSound;
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Instantiate(Ammunition, GunBarrel.transform.position, GunBarrel.transform.rotation);
            ControlAudio.instance.PlayOneShot(GunshotSound);
        }        
    }
}
