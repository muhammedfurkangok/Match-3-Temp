using Runtime.Enums;
using UnityEngine;
using Color = System.Drawing.Color;

namespace Runtime.Data.ValueObject
{
    [System.Serializable]
    public struct GameColorsData
    {
        public GameColors[] gameColorArray;
        public Material[] materialColorArray;
    }
}