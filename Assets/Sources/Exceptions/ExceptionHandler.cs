using System;
using UnityEngine;
using System.Net.Sockets;

namespace Client.Exceptions
{
    public static class ExceptionHandler
    {
        public static void Execute(Exception exception, string methodName)
        {
            Debug.LogError($"Exception message: {exception.Message}\n Method name: {methodName}\n Error stack: {exception.StackTrace}.");
        }

        public static void ExecuteSocketException(SocketException exception, string methodName)
        {
            Debug.LogError($"Method: {methodName}. Message: '{exception.Message}' Socket Error '{exception.ErrorCode}' (Error Number: '{exception.NativeErrorCode}')");
        }
    }
}