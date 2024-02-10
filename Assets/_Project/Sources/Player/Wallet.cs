using UnityEngine;

public class Wallet : MonoBehaviour
{
   [SerializeField] private int _money = 0;
   [SerializeField] private ItemPicker _picker;

   private void OnEnable()
   {
      _picker.PickedCoin += CollectCoins;
   }

   private void OnDisable()
   {
      _picker.PickedCoin -= CollectCoins;
   }

   private void CollectCoins(int amount)
   {
      if(amount <= 0)
         return;
      
      _money += amount;
   }
}
