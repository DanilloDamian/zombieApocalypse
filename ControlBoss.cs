using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ControlBoss : MonoBehaviour,IKillable
{
    public NavMeshAgent agent;
    private Status statusBoss;
    private CharacterAnimation animationBoss;
    private CharacterMovement movementBoss;
    public GameObject MedicalKitPrefab;
    public Slider SliderBossLife;
    public Image ImageSlider;
    public Color MaximumColorLife, MinimalColorLife;
    public GameObject BossBloodParticle;
    [HideInInspector]
    public Transform player;
    

    private void Start()
    {
        player = GameObject.FindWithTag(Tags.Player).transform;
        agent =  GetComponent<NavMeshAgent>();
        statusBoss = GetComponent<Status>();
        animationBoss = GetComponent<CharacterAnimation>();
        movementBoss = GetComponent<CharacterMovement>();
        agent.speed = statusBoss.Speed;
        SliderBossLife.maxValue = statusBoss.InitialLife;
        UpdateInterface();
    }

    void Update()
    {
        agent.SetDestination(player.position);
        animationBoss.AnimateMotion(agent.velocity.magnitude);
        bool amICloseToThePlayer = agent.remainingDistance <= agent.stoppingDistance;
        if(agent.hasPath == true){        
            if(amICloseToThePlayer)
            {
                animationBoss.Attack(true);
                Vector3 direction = player.position - transform.position;
                movementBoss.Rotate(direction);
            }else
            {
                animationBoss.Attack(false);
            }
        }
    }

    void UpdateInterface()
    {
        SliderBossLife.value = statusBoss.Life;
        float percentageOfLife = (float)statusBoss.Life/statusBoss.InitialLife;
        Color lifeColor = Color.Lerp(MinimalColorLife,MaximumColorLife,percentageOfLife);
        ImageSlider.color = lifeColor;
    }

    void AttackPlayer()
    {
        int damage = Random.Range(30,40);
        player.GetComponent<ControlPlayer>().TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        statusBoss.Life -= (float)damage;
        UpdateInterface();
        if(statusBoss.Life <= 0)
        {
            Die();
        }
    }

    public void BloodParticle(Vector3 position, Quaternion rotation)
    {
        Instantiate(BossBloodParticle, position, rotation);
    }

    public void Die()
    {        
        animationBoss.Die();        
        movementBoss.Die();
        this.enabled = false;
        Instantiate(MedicalKitPrefab,transform.position, Quaternion.identity);        
        StartCoroutine(SetActiveDie());       
    }
     IEnumerator SetActiveDie()
    {
        yield return new WaitForSeconds(4f);
        this.gameObject.SetActive(false);        
        agent.enabled = false;
    }
}
