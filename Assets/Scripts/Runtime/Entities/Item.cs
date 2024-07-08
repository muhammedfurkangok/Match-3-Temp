using System.Collections;
using System.Collections.Generic;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
   [SerializeField] private bool isWinningItem;
   public void OnSelected()
   {
         if (isWinningItem)
         {
            GameManager.Instance.SetGameStateLevelComplete();
         }
         else
         {
            GameManager.Instance.SetGameStateLevelFail();
         }
   }
}
