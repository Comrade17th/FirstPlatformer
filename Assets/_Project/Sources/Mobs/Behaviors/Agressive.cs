using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Agressive : MonoBehaviour
{
     [SerializeField] private Mob mob;
     private void OnTriggerEnter2D(Collider2D other)
     {
          if (other.TryGetComponent(out Player player))
          {
               mob.Attack(player);
          }
     }
}
