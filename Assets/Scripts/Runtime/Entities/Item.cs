using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
   
   public void OnSelected()
   {
         SoundManager.Instance.PlaySound(GameSoundType.Touch);
         MoverManager.Instance.OnInputTaken();
   }
}
