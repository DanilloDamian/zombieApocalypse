using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    public GameObject Player;
    private Vector3 distanceCompensate;

    void Start()
    {
        distanceCompensate = transform.position - Player.transform.position;
    }

    void Update()
    {
        transform.position = Player.transform.position + distanceCompensate;
    }
}
