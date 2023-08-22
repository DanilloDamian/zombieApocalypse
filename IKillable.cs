using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
   void TakeDamage(int damage);
   void Die();
}
