using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
   [SerializeField] private int _money = 0;
   [SerializeField] private ItemPicker _picker;

   private void OnEnable()
   {
      Debug.Log("Subscribed");
      _picker.PickedCoin += CollectCoins;
   }

   private void OnDisable()
   {
      _picker.PickedCoin -= CollectCoins;
   }

   private void CollectCoins(int amount)
   {
      Debug.Log("Coin try added");
      if(amount <= 0)
         return;
      
      _money += amount;
      Debug.Log(_money);
   }
}
