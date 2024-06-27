using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class UISignals: SingletonMonoBehaviour<UISignals>
    {
        public UnityAction<byte> onSetNewLevelValue = delegate { };
        public UnityAction<int> onSetMoneyValue = delegate { };
        public UnityAction onSettingsButtonClicked = delegate { };
    }
}