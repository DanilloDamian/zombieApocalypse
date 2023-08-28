using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerator : MonoBehaviour
{
    public GameObject BossPrefab;
    public ControlInterface scriptControlInterface;
    public Transform[] PossiblePositionsOfGeneration;
    private Transform player;
    private float timeForNextGeneration = 0;
    private float timeBetweenGenerations = 20;
    public List<ControlBoss> listBoss;
    public int initialNumberBoss = 4;


    private void Start()
    {
        timeForNextGeneration = timeBetweenGenerations;
        scriptControlInterface = GameObject.FindObjectOfType(typeof(ControlInterface)) as ControlInterface;
        player = GameObject.FindWithTag(Tags.Player).transform;
        InitListBoss();
    }
    
    private void Update()
    {
        if(Time.timeSinceLevelLoad > timeForNextGeneration)
        {
            List<ControlBoss> noActiveBoss = new List<ControlBoss>();
            foreach(ControlBoss boss in listBoss)
            {
                if(boss.gameObject.activeSelf == false) noActiveBoss.Add(boss);
            }
            if(noActiveBoss.Count == 0)
            {
                InitBoss();
            }
            else{
                Vector3 positionOfCreation = CalculateMostDistantPossiblePositionFromPlayer();
                StartCoroutine(GenerateNewBoss(positionOfCreation));
                scriptControlInterface.AppearCreatedBossText();
                timeForNextGeneration = Time.timeSinceLevelLoad +timeBetweenGenerations;
            } 
        }
    }
    void InitListBoss()
    {
        for(int i=0;i<initialNumberBoss;i++)
        {
            InitBoss();
        }        
    }
    void InitBoss()
    {
        GameObject obj = Instantiate(BossPrefab,transform.position,Quaternion.identity);
        obj.SetActive(false);
        listBoss.Add(obj.GetComponent<ControlBoss>());
    }

    IEnumerator GenerateNewBoss(Vector3 positionStart)
    {
        yield return null;
        int i = Random.Range(0,listBoss.Count);
        while(true){
            var boss = listBoss[i];            
            if(!boss.gameObject.activeInHierarchy)
            {
                boss.gameObject.GetComponent<Status>().Life = boss.gameObject.GetComponent<Status>().InitialLife;
                boss.gameObject.SetActive(true);
                boss.enabled = true;
                boss.transform.position = positionStart;
                boss.agent.enabled = true;

                var bossRB = boss.GetComponent<Rigidbody>();              
                bossRB.isKinematic = true;               
                bossRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

                boss.GetComponent<Collider>().enabled = true;
                break;
            }else{
                i = Random.Range(0,listBoss.Count);
            }
        }
    }

    Vector3 CalculateMostDistantPossiblePositionFromPlayer()
    {
        Vector3 positionOfGreatestDistance = Vector3.zero;
        float greaterDistance = 0;
        foreach (Transform position in PossiblePositionsOfGeneration)
        {
            float distancePositionPlayer = Vector3.Distance(player.position, position.position);
            if(distancePositionPlayer > greaterDistance)
            {
                greaterDistance = distancePositionPlayer;
                positionOfGreatestDistance = position.position;
            }
        }
        return positionOfGreatestDistance;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,3);
    }
}
