using System;
using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Signals
{
    public class CoreGameSignals : SingletonMonoBehaviour<CoreGameSignals>
    {
        public UnityAction<int> onLevelInitialize = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public Func<int> onGetLevelValue = delegate { return 0; };
    }
}