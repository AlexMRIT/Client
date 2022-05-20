using Client.Models;
using System.Collections.Concurrent;

namespace Client.Network
{
    public sealed class PoolCharacters
    {
        public readonly ConcurrentDictionary<int, Character> _characters;

        public PoolCharacters()
        {
            _characters = new ConcurrentDictionary<int, Character>();
        }

        public bool Add(int id, Character character)
        {
            if (_characters.Count >= ApplicationConfig.MaxPlayersInRoom)
                return false;

            return _characters.TryAdd(id, character);
        }

        public bool Remove(int id)
        {
            return _characters.TryRemove(id, out _);
        }
    }
}