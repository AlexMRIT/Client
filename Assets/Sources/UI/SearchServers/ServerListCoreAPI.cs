using UnityEngine;
using Client.Enums;
using Client.Utilite;
using Client.Contracts;
using Client.Exceptions;
using Client.RequestPackets;
using System.Collections.Generic;

#pragma warning disable IDE0044

namespace Client.UI.SearchServers
{
    [RequireComponent(typeof(UILinqServerList))]
    public sealed class ServerListCoreAPI : MonoBehaviour
    {
        [SerializeField] private bool _allCrearListener = true;

        private int SelectedServer = -1;
        private UILinqServerList _linqServerList;
        private ModelViewConnection _network;

        private void OnEnable()
        {
            DepedencyProvider.TryAddObject(DepedencyProvider.Code.ObjectServerList, gameObject);
        }

        private void Awake()
        {
            _linqServerList = GetComponent<UILinqServerList>();

            ValidNullReferenceException.Execute(_linqServerList.UpdateServerList, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.CreateServer, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.CreateServerEnd, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.EnterServer, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.ServerName, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.ServerDescription, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.ContainerServers, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.ServerElement, nameof(ServerListCoreAPI.Awake));
            ValidNullReferenceException.Execute(_linqServerList.WindowCreateServer, nameof(ServerListCoreAPI.Awake));
        }

        private void Start()
        {
            _network = FindObjectOfType<ModelViewConnection>();

            _linqServerList.UpdateServerList.AddListener(CallButtonUpdateServerLists, clearOldListener: _allCrearListener);
            _linqServerList.CreateServer.AddListener(CallButtonCreateServer, clearOldListener: _allCrearListener);
            _linqServerList.CreateServerEnd.AddListener(CallButtonCreateServerEnd, clearOldListener: _allCrearListener);
            _linqServerList.EnterServer.AddListener(CallButtonEnterInServer, clearOldListener: _allCrearListener);

            _linqServerList.WindowCreateServer.SetActive(false);
            _network.SendPacket(SendPacketTryUpdateServerList.ToPacket(_network.GetClientSession()));
        }

        public void AddServer(ServerRoom server)
        {
            _linqServerList.ServerRooms.Add(new KeyValuePair<int, ServerRoom>(server.Id, server));

            ServerInfo serverObject = Instantiate(_linqServerList.ServerElement, _linqServerList.ContainerServers).GetComponent<ServerInfo>();
            ServerBuildView.ExecuteBuild(serverObject, server);
            _linqServerList.ServersInfo.Add(new KeyValuePair<int, ServerInfo>(server.Id, serverObject));

            serverObject.SelectServer.AddListener(delegate { CallButtonSelectServer(server.Id); }, clearOldListener: _allCrearListener);
        }

        private void CallButtonUpdateServerLists()
        {
            ClearAllServers();
            SelectedServer = -1;
            _network.SendPacket(SendPacketTryUpdateServerList.ToPacket(_network.GetClientSession()));
        }

        private void CallButtonCreateServer()
        {
            _linqServerList.WindowCreateServer.SetActive(!_linqServerList.WindowCreateServer.gameObject.activeSelf);
            SelectedServer = -1;
        }

        private void CallButtonCreateServerEnd()
        {
            bool successfullName = _linqServerList.ServerName.text.Valid(ValidType.ServerName).CheckErrorCode();
            bool successfullDescription = _linqServerList.ServerDescription.text.Valid(ValidType.Description).CheckErrorCode();
            SelectedServer = -1;

            if (successfullName && successfullDescription)
            {
                _network.SendPacket(SendPacketTryCreateServer.ToPacket(_linqServerList.ServerName.text, _linqServerList.ServerDescription.text));
                _linqServerList.WindowCreateServer.SetActive(false);
                ClearAllServers();
            }
        }

        private void CallButtonEnterInServer()
        {
            if (SelectedServer == -1)
                return;

            ClearAllServers();
            _network.SendPacket(SendPacketTryEnterInRoom.ToPacket(SelectedServer, _network.GetClientSession()));
        }

        private void CallButtonSelectServer(int id)
        {
            Debug.Log($"The id server: {id}");
            SelectedServer = id;
        }

        private void ClearAllServers()
        {
            _linqServerList.ServerRooms.Clear();
            _linqServerList.ServersInfo.Select(x => Destroy(x.Value.gameObject));
            _linqServerList.ServersInfo.Clear();
        }

        private void OnDisable()
        {
            DepedencyProvider.TryRemoveObject(DepedencyProvider.Code.ObjectServerList);
        }
    }
}