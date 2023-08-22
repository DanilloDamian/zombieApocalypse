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
    private float timeBetweenGenerations = 30;


    private void Start()
    {
        timeForNextGeneration = timeBetweenGenerations;
        scriptControlInterface = GameObject.FindObjectOfType(typeof(ControlInterface)) as ControlInterface;
        player = GameObject.FindWithTag(Tags.Player).transform;
    }
    
    private void Update()
    {
        if(Time.timeSinceLevelLoad > timeForNextGeneration)
        {
            Vector3 positionOfCreation = CalculateMostDistantPossiblePositionFromPlayer();
            Instantiate(BossPrefab,positionOfCreation,Quaternion.identity);
            scriptControlInterface.AppearCreatedBossText();
            timeForNextGeneration = Time.timeSinceLevelLoad +timeBetweenGenerations;
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
