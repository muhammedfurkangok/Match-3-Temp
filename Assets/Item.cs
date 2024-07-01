using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   [SerializeField] private bool isWİnningItem;
   public void OnSelected()
   {
         if (isWİnningItem)
         {
            Debug.Log("You Win!");
         }
         else
         {
            Debug.Log("You Lose!");
         }
   }
}
