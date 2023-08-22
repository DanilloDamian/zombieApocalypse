using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [HideInInspector]
    public float Life;
    public float Speed = 5;
    public float InitialLife = 100;

    void Awake()
    {
        Life = InitialLife;
    }
}
