using UnityEngine;
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

        private void Awake()
        {
            _uiLinq = GetComponent<UILinqAuthentication>();

            ValidNullReferenceException.Execute(_uiLinq.GetType(), nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.Enter.GetType(), nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.Exit.GetType(), nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.LoginField.GetType(), nameof(AuthenticationCoreAPI.Awake));
            ValidNullReferenceException.Execute(_uiLinq.PasswordField.GetType(), nameof(AuthenticationCoreAPI.Awake));
        }

        private void Start()
        {
            _network = FindObjectOfType<ModelViewConnection>();

            _uiLinq.Enter.AddListener(CallButtonForEnterInSystem, clearOldListener: _clearAllListener);
            _uiLinq.Exit.AddListener(CallButtonForExitWithGame, clearOldListener: _clearAllListener);
        }

        private void CallButtonForEnterInSystem()
        {
            bool result = _uiLinq.LoginField.text.Valid().CheckErrorCode();

            if (result)
                _network.SendPacket(SendPacketTryAuthentication.ToPacket(_uiLinq.LoginField.text, _uiLinq.PasswordField.text));
        }

        private void CallButtonForExitWithGame()
        {
            Application.Quit();
        }
    }
}