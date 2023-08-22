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

    void Start()
    {
        player = GameObject.FindWithTag(Tags.Player);
        counterIncreaseDifficulty = timeNextIncreasedDifficulty;
        for (int i = 0; i < maxAmountOfZombiesLive; i++)
        {
            StartCoroutine(GenerateNewZombie());
        }
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
        ControlEnemy zombie = Instantiate(Zombie,positionOfCreation,transform.rotation).GetComponent<ControlEnemy>();
        zombie.myGenerator = this;
        amountOfZombiesAlive++;
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
