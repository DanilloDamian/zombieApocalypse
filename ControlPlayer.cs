using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour,ICurable,IKillable
{
    private Vector3 direction;
    private PlayerMovement playerMovement;
    private CharacterAnimation animationPlayer;
    public LayerMask FloorMask;  
    public ControlInterface ScriptControlInterface;
    public AudioClip DamageSound;
    public Status Status;


    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animationPlayer = GetComponent<CharacterAnimation>();
        Status = GetComponent<Status>();
    }

    void Update()
    {
        float axisX = Input.GetAxis("Vertical");
        float axisZ = Input.GetAxis("Horizontal");
        direction = new Vector3(axisX,0,axisZ);
        animationPlayer.AnimateMotion(direction.magnitude);
    }

    void FixedUpdate()
    {
        playerMovement.Move(direction, Status.Speed);
        playerMovement.RotatePlayer(FloorMask);
    }

    public void TakeDamage(int damage)
    {
        Status.Life -= (float)damage;
        ScriptControlInterface.UpdateSliderPlayerLiFe();
        ControlAudio.instance.PlayOneShot(DamageSound);
        if(Status.Life <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ScriptControlInterface.GameOver();
    }

    public void HealLife(int amountOfHealing)
    {
        Status.Life += amountOfHealing;
        if(Status.Life > Status.InitialLife)
        {
            Status.Life = Status.InitialLife;
        }
        ScriptControlInterface.UpdateSliderPlayerLiFe();
    }
}
