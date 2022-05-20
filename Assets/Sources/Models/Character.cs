using UnityEngine;
using Client.Contracts;

namespace Client.Models
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class Character : MonoBehaviour
    {
        public CharacterSpecification GetCharacterSpecification { get; private set; }

        private bool _IsInitialise;
        private CharacterContract _characterContract;
        private CharacterController _characterController;
        private Vector3 _direction;
        private Vector3 _lookDirection;
        private bool _blockingSending = false;
        private bool _isBot = false;

        public void Init(CharacterSpecification characterSpecification, CharacterContract characterContract, bool isBot)
        {
            if (_IsInitialise)
                return;

            GetCharacterSpecification = characterSpecification;
            _characterContract = characterContract;
            _characterController = gameObject.GetComponent<CharacterController>();
            _isBot = isBot;

            _IsInitialise = true;
        }

        private void Update()
        {
            if (_isBot)
                return;

            _direction.x = Input.GetAxis("Horizontal") * _characterContract.MoveSpeed * Time.deltaTime;
            _direction.z = Input.GetAxis("Vertical") * _characterContract.MoveSpeed * Time.deltaTime;

            if ((_direction.x != 0.0f || _direction.z != 0.0f) && !_blockingSending)
            {
                Debug.Log("Send to server for movement!");
                _blockingSending = true;
            }
            else if (_direction.x == 0.0f && _direction.z == 0.0f && _blockingSending)
            {
                Debug.Log("Send to server for stoped!");
                _blockingSending = false;
            }

            if (Vector3.Angle(transform.forward, _direction) > 1.0f)
            {
                Debug.Log("Send to server for direction change");
                _lookDirection = Vector3.RotateTowards(transform.forward, _direction, _characterContract.MoveSpeed, 0.0f);
                transform.rotation = Quaternion.LookRotation(_lookDirection);
            }

            SetDirection(_direction);
        }

        private void SetDirection(Vector3 direction)
        {
            _characterController.Move(direction);
        }
    }
}