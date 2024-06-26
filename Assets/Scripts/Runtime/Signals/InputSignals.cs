using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class InputSignals : SingletonMonoBehaviour<InputSignals>
    {
        //tutorial:  public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onEnableInput = delegate { };
        public UnityAction onDisableInput = delegate { };
        public UnityAction onInputTaken = delegate { };
    }
}