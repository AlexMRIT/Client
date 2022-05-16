using UnityEngine;

namespace Client.Models
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class Character : MonoBehaviour
    {
        public CharacterSpecification GetCharacterSpecification { get; private set; }

        private bool _IsInitialise;

        public void Init(CharacterSpecification characterSpecification)
        {
            if (_IsInitialise)
                return;

            GetCharacterSpecification = characterSpecification;

            _IsInitialise = true;
        }
    }
}