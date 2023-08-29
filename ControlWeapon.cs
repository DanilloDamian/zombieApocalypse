using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlWeapon : MonoBehaviour
{
    public GameObject Ammunition;
    public GameObject GunBarrel;
    public AudioClip GunshotSound;
    private int initialNNumberList = 20;
    public List<Ammunition> listAmmunition;
    
    void Start()
    {
        InitListAmmunition();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            StartCoroutine(GenerateAmmunition(GunBarrel.transform.position, GunBarrel.transform.rotation));
            ControlAudio.instance.PlayOneShot(GunshotSound);
        }        
    }

    void InitListAmmunition()
    {
        for(int i=0;i<initialNNumberList;i++)
        {
            InitAmmunition();
        }

    }
    void InitAmmunition()
    {
        GameObject obj = Instantiate(Ammunition, transform.position, transform.rotation);
        obj.SetActive(false);
        listAmmunition.Add(obj.GetComponent<Ammunition>());
    }

    IEnumerator GenerateAmmunition(Vector3 positionStart,Quaternion rotationStart)
    {
        yield return null;
        int i = Random.Range(0,listAmmunition.Count);
        while(true){
            var ammunition = listAmmunition[i];            
            if(!ammunition.gameObject.activeInHierarchy)
            {
                ammunition.gameObject.SetActive(true);                
                ammunition.transform.position = positionStart;    
                ammunition.transform.rotation = rotationStart;    

                ammunition.GetComponent<Collider>().enabled = true;
                break;
            }else{
                i = Random.Range(0,listAmmunition.Count);
            }
        }

    }


}
