using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemy : MonoBehaviour,IKillable
{
    private float counterWander;
    private float timeBetweenRandomPositions = 5;
    private float generateMedicalKitPercentage = 0.2f;
    private CharacterAnimation animationEnemy;
    private CharacterMovement movementEnemy;
    private Status status;
    private Vector3 direction;
    private Vector3 randomPosition;
    public GameObject Player ;
    public AudioClip SoundOfDeath;
    public GameObject MedicalKitPrefab;
    public ControlInterface ScriptControlInterface;
    public GameObject ZombieBloodParticle;
    [HideInInspector]
    public ZombieGenerator myGenerator;

    void Start()
    {        
        Player = GameObject.FindWithTag(Tags.Player); 
        animationEnemy = GetComponent<CharacterAnimation>();
        movementEnemy = GetComponent<CharacterMovement>();
        status = GetComponent<Status>();
        ScriptControlInterface = GameObject.FindObjectOfType(typeof(ControlInterface)) as ControlInterface;
    }

    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position,Player.transform.position);
        
        movementEnemy.Rotate(direction);
        animationEnemy.AnimateMotion(direction.magnitude);
        if(distance>8)
        {
            Wander();
            
        }else if(distance>1.5 && distance<=8)
        {
            direction = Player.transform.position - transform.position;
            movementEnemy.Move(direction,status.Speed);
            animationEnemy.Attack(false);
        }else{
            
            direction = Player.transform.position - transform.position;
            animationEnemy.Attack(true);
        }
        
    }
    
    void Wander()
    {
        counterWander -= Time.deltaTime;
        if(counterWander <= 0)
        {
            randomPosition = RandomizePosition();
            counterWander += timeBetweenRandomPositions + Random.Range(-1f,1f);
        }
        bool gotCloseEnough = Vector3.Distance(transform.position,randomPosition)<=0.6;
        
        if(!gotCloseEnough)
        {
            direction = randomPosition - transform.position;
            movementEnemy.Move(direction,status.Speed);      
                  
        }else{
            animationEnemy.AnimateMotion(0.1f);
        }
    }
    private void OnTriggerEnter(Collider collisionObject){
            animationEnemy.AnimateMotion(0.1f); 
            randomPosition = transform.position;  
            
    }

    Vector3 RandomizePosition()
    {
        Vector3 position = Random.insideUnitSphere * 5;
        position += transform.position;
        position.y = transform.position.y;
        return position;
    }

    void AttackPlayer()
    {
        int damage = Random.Range(10,15);
        Player.GetComponent<ControlPlayer>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        status.Life -= (float)damage;
        if(status.Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ControlAudio.instance.PlayOneShot(SoundOfDeath);
        animationEnemy.Die();
        movementEnemy.Die();
        this.enabled = false;
        CheckMedicalKitGeneration(generateMedicalKitPercentage);
        StartCoroutine(SetActiveDie());
        ScriptControlInterface.UpdateNumberOfZombiesKilled();
        myGenerator.DecreaseAmountOfLivingZombies();
    }
    IEnumerator SetActiveDie()
    {
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

    public void CheckMedicalKitGeneration(float generationPercentage)
    {
        if(Random.value <= generationPercentage)
        {
            Instantiate(MedicalKitPrefab,transform.position, Quaternion.identity);
        }
    }

    public void BloodParticle(Vector3 position,Quaternion rotation)
    {
        Instantiate(ZombieBloodParticle, position, rotation);
    }
}
