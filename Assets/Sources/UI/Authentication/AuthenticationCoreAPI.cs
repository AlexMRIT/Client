using UnityEngine;
using Client.Enums;
using Client.Utilite;
using Client.Exceptions;
using Client.RequestPackets;

namespace Client.UI.Authentication
{
    [RequireComponent(typeof(UILinqAuthentication))]
    public sealed class AuthenticationCoreAPI : MonoBehaviour
    {
        [SerializeField] private bool _clearAllListener = true;

        private UILinqAuthentication _uiLinq;
        private ModelViewConnection _network;

        private void OnEnable()
        {
            DepedencyProvider.TryAddObject(DepedencyProvider.Code.ObjectAuthenticationAPI, gameObject);
        }

        private void Awake()
        {
            _uiLinq = GetComponent<UILinqAuthentication>();

            ValidNullReferenceException.Execute(_uiLinq, nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.Enter, nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.Exit, nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.LoginField, nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.PasswordField, nameof(AuthenticationCoreAPI.Awake));
        }

        private void Start()
        {
            _network = FindObjectOfType<ModelViewConnection>();

            _uiLinq.Enter.AddListener(CallButtonForEnterInSystem, clearOldListener: _clearAllListener);
            _uiLinq.Exit.AddListener(CallButtonForExitWithGame, clearOldListener: _clearAllListener);
        }

        private void CallButtonForEnterInSystem()
        {
            bool successfullLogin = _uiLinq.LoginField.text.Valid(ValidType.Login).CheckErrorCode();
            bool successfullPassword = _uiLinq.PasswordField.text.Valid(ValidType.Password).CheckErrorCode();

            if (successfullLogin && successfullPassword)
            {
                _network.ChangeClientSession(authorization: true, matchSearch: false, gamePlaying: false);
                _network.SendPacket(SendPacketTryAuthentication.ToPacket(_network.GetClientSession(), _uiLinq.LoginField.text, _uiLinq.PasswordField.text));
            }
        }

        private void CallButtonForExitWithGame()
        {
            Application.Quit();
        }

        private void OnDisable()
        {
            DepedencyProvider.TryRemoveObject(DepedencyProvider.Code.ObjectAuthenticationAPI);
        }
    }
}