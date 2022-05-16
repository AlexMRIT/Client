using System;
using Client.Models;
using System.Collections.Concurrent;

namespace Client.World
{
    public static class Room
    {
        private readonly static ConcurrentDictionary<int, Character> _characterView = new ConcurrentDictionary<int, Character>();

        public static bool TryAdd(Character character, Action<Character> action)
        {
            bool resultAdd = _characterView.TryAdd(character.GetCharacterSpecification.ObjectId, character);
            action?.Invoke(character);

            return resultAdd;
        }

        public static bool TryRemove(Character character, Action<Character> action)
        {
            bool resultRemove = _characterView.TryRemove(character.GetCharacterSpecification.ObjectId, out Character outCharacter);
            action?.Invoke(outCharacter);

            return resultRemove;
        }

        public static Character TryGetCharacterById(int id)
        {
            if (!_characterView.TryGetValue(id, out Character character))
                return null;

            return character;
        }
    }
}