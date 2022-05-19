using System;
using Client.Contracts;
using Client.Exceptions;
using Client.UI.SearchServers;
using System.Collections.Generic;

namespace Client.Utilite
{
    public static class ServerBuildView
    {
        public static void ExecuteBuild(ServerInfo serverInfo, ServerRoom serverRoom)
        {
            serverInfo.ServerId = serverRoom.Id;
            serverInfo.ServerName.text = serverRoom.Name;
            serverInfo.ServerDescription.text = serverRoom.Description;
            serverInfo.CountPlayers.text = $"{serverRoom.CurrentPlayer}/{serverRoom.MaxPlayer}";
        }

        public static void Select<TSource>(this IEnumerable<TSource> sources, Action<TSource> action)
        {
            if (sources is null)
                ExceptionHandler.Execute(new NullReferenceException(), nameof(ServerBuildView.Select));

            foreach (TSource element in sources)
                action(element);
        }
    }
}