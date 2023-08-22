using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public void RotatePlayer(LayerMask FloorMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin,(ray.direction*100));
        RaycastHit impact;
        if(Physics.Raycast(ray,out impact,100, FloorMask))
        {
            Vector3 playerCrosshairPosition = impact.point - transform.position;
            playerCrosshairPosition.y = transform.position.y;
            Rotate(playerCrosshairPosition);
        }
    }    
}
