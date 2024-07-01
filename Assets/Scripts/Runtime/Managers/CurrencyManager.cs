using Runtime.Extensions;
using UnityEngine;  

namespace Runtime.Managers
{
    public class CurrencyManager : SingletonMonoBehaviour<CurrencyManager>
    {
        public int coinAmount{ get; private set; }
        
        public void IncressCoinAmount(int amount)
        {
            coinAmount += amount;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CoinsInt, coinAmount);
        }
    }
}