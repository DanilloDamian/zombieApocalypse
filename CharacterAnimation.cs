using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator myAnimator;

    void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void Attack(bool state)
    {
        myAnimator.SetBool("Attacking",state);
    }
    public void AnimateMotion(float direction)
    {
        myAnimator.SetFloat("Moving",direction);
    }
    public void Die()
    {
        myAnimator.SetTrigger("Die");
    }
}
