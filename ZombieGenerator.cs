using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public GameObject Zombie;
    public LayerMask LayerZombie;
    private GameObject player;
    private float meterTime = 0;
    public float TimeGenerateZombie = 1;
    private float generationDistance = 2;
    private float distanceFromPlayerToGeneration = 5;
    private int maxAmountOfZombiesLive = 2;
    private int amountOfZombiesAlive;
    private float timeNextIncreasedDifficulty = 15;
    private float counterIncreaseDifficulty;
    public List<ControlEnemy> listZombies;
    public int initialNumberList= 10;

    void Start()
    {
        player = GameObject.FindWithTag(Tags.Player);
        counterIncreaseDifficulty = timeNextIncreasedDifficulty;
        InitList();
        for (int i = 0; i < maxAmountOfZombiesLive; i++)
        {
            StartCoroutine(GenerateNewZombie());
        }
    }
    void InitList()
    {
        for(int i=0;i<=initialNumberList;i++)
        {
            InitZombie();
        }

    }
    void InitZombie()
    {
        GameObject obj = Instantiate(Zombie,transform.position, Quaternion.identity);
        obj.SetActive(false);
        obj.GetComponent<ControlEnemy>().myGenerator = this;
        listZombies.Add(obj.GetComponent<ControlEnemy>());
    }
    void Update()
    {
        bool canISpawnZombiesOverDistance = Vector3.Distance(transform.position,player.transform.position) > distanceFromPlayerToGeneration;
        bool canISpawnZombiesByQuantity = amountOfZombiesAlive < maxAmountOfZombiesLive;

        if(canISpawnZombiesOverDistance && canISpawnZombiesByQuantity)
        {
            meterTime += Time.deltaTime;
            if(meterTime >= TimeGenerateZombie)
            {
                List<ControlEnemy> noActiveZombies = new List<ControlEnemy>();
                foreach(ControlEnemy zombie in listZombies)
                { 
                    if(zombie.gameObject.activeSelf == false) noActiveZombies.Add(zombie);
                }
                if(noActiveZombies.Count == 0 ) InitZombie();             
                
                StartCoroutine(GenerateNewZombie());
                meterTime =0;
            }
        }
        if(Time.timeSinceLevelLoad > counterIncreaseDifficulty)
        {
            counterIncreaseDifficulty = Time.timeSinceLevelLoad + timeNextIncreasedDifficulty;
            maxAmountOfZombiesLive++;
        }
         
    }

    IEnumerator GenerateNewZombie()
    {
        Vector3 positionOfCreation = RandomizePosition();
        Collider[] colliders = Physics.OverlapSphere(positionOfCreation,1,LayerZombie);
        while (colliders.Length > 0)
        {
            positionOfCreation = RandomizePosition();
            colliders = Physics.OverlapSphere(positionOfCreation,1,LayerZombie);
            yield return null;
        }
        int i = Random.Range(0,listZombies.Count);
        while(true){
            var zombie = listZombies[i];            
            if(!zombie.gameObject.activeInHierarchy)
            {
                zombie.gameObject.SetActive(true);
                zombie.enabled = true;
                zombie.transform.position = transform.position;

                var zombieRB = zombie.GetComponent<Rigidbody>();              
                zombieRB.isKinematic = true;               
                zombieRB.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

                zombie.GetComponent<Collider>().enabled = true;
                amountOfZombiesAlive++;
                break;
            }else{
                i = Random.Range(0,listZombies.Count);
            }

        }
    }

    Vector3 RandomizePosition()
    {
        Vector3 position = Random.insideUnitSphere*generationDistance;
        position += transform.position;
        position.y= 0;
        return position;
    }

    public void DecreaseAmountOfLivingZombies()
    {
        amountOfZombiesAlive--;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,generationDistance);
    }
}
