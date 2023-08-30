using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReserveMedicalKit : MonoBehaviour
{
    public GameObject MedicalKitPrefab;
    public int initialNumberList= 20;
    public List<MedicalKit> ListMedicalKit;
    
    void Start()
    {
        InitList();
    }

    void InitList()
    {
        for(int i=0;i<initialNumberList;i++)
        {
            GameObject obj = Instantiate(MedicalKitPrefab,transform.position, Quaternion.identity);
            obj.SetActive(false);
            ListMedicalKit.Add(obj.GetComponent<MedicalKit>());
        }
    }
    public void PullMedicalKit(Vector3 position)
    {
        StartCoroutine(GenerateMedicalKit(position));

    }
    IEnumerator GenerateMedicalKit(Vector3 position)
    {
        yield return null;
        int i = Random.Range(0,ListMedicalKit.Count);
        while(true)
        {
            var kit = ListMedicalKit[i];
            if(!kit.gameObject.activeInHierarchy)
            {
                kit.gameObject.SetActive(true);
                kit.transform.position = position;
                break;
            }else{
                i = Random.Range(0,ListMedicalKit.Count);
            }
        }
    }

    
}
