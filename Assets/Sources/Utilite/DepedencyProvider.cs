using UnityEngine;
using System.Collections.Concurrent;

namespace Client.Utilite
{
    public static class DepedencyProvider
    {
        private readonly static ConcurrentDictionary<byte, GameObject> _unityObjects = new ConcurrentDictionary<byte, GameObject>();

        public static class Code
        {
            public readonly static byte ObjectAuthenticationAPI = 0x00;
            public readonly static byte ObjectClientProcessor = 0x01;
            public readonly static byte ObjectServerList = 0x02;
        }

        public static bool TryAddObject(byte code, GameObject gameObject)
        {
            return _unityObjects.TryAdd(code, gameObject);
        }

        public static bool TryRemoveObject(byte code)
        {
            return _unityObjects.TryRemove(code, out _);
        }

        public static GameObject TryGetObjectByCode(byte code)
        {
            if (_unityObjects.TryGetValue(code, out GameObject gameObject))
                return gameObject;

            return null;
        }
    }
}