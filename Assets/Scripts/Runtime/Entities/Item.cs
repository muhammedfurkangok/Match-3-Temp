using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    private Vector2Int gridPosition;
    private Color itemColor;
    
    public void Init(Vector2Int position, Color color)
    {
        gridPosition = position;
        itemColor = color;

       
        // Material material = GameMaterialManager.Instance.GetMaterialByColor(itemColor);
        // GetComponent<Renderer>().material = material;
    }
   public void OnSelected()
   {
         SoundManager.Instance.PlaySound(GameSoundType.Touch);
         MoverManager.Instance.OnInputTaken();
   }
}

