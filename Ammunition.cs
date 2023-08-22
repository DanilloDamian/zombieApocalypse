using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public float Speed = 8;
    private int shootingDamage = 1;
    private Rigidbody rigidbodyAmmunition;
    public AudioClip SoundOfDeath;

    void Start()
    {
        rigidbodyAmmunition = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbodyAmmunition.MovePosition(rigidbodyAmmunition.position + (transform.forward * Speed *Time.deltaTime));
    }

    void OnTriggerEnter(Collider collisionObject)
    {
        Quaternion inversionOfRotation = Quaternion.LookRotation(-transform.forward);
        switch (collisionObject.tag)
        {
            case Tags.Zombie:
            ControlEnemy enemy = collisionObject.GetComponent<ControlEnemy>();
            enemy.TakeDamage(shootingDamage);
            enemy.BloodParticle(transform.position, inversionOfRotation);
            break;
            case Tags.Boss:
            ControlBoss boss = collisionObject.GetComponent<ControlBoss>();
            boss.TakeDamage(shootingDamage);
            boss.BloodParticle(transform.position, inversionOfRotation);
            break;           
        }
        Destroy(gameObject);
    }  
}
