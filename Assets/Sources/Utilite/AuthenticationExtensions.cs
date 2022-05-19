using System;
using UnityEngine;
using Client.Enums;
using Client.Exceptions;

namespace Client.Utilite
{
    public static class AuthenticationExtensions
    {
        private static readonly string MessageMaxLength = "Max length!";
        private static readonly string MessageMinLength = "Min length!";

        public static Tuple<bool, ErrorCode, string> Valid(this string text, ValidType type)
        {
            switch (type)
            {
                case ValidType.Login:
                    if (string.IsNullOrEmpty(text))
                        ExceptionHandler.Execute(new NullReferenceException("Login is null or empty."), nameof(AuthenticationExtensions.Valid));

                    if (text.Length > ApplicationConfig.MaxLoginLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MaxLength, MessageMaxLength);

                    if (text.Length <= ApplicationConfig.MinLoginLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MinLength, MessageMinLength);
                    break;
                case ValidType.Password:
                    if (string.IsNullOrEmpty(text))
                        ExceptionHandler.Execute(new NullReferenceException("Passwrod is null or empty."), nameof(AuthenticationExtensions.Valid));

                    if (text.Length > ApplicationConfig.MaxPasswordLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MaxLength, MessageMaxLength);

                    if (text.Length <= ApplicationConfig.MinPasswordLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MinLength, MessageMinLength);
                    break;
                case ValidType.ServerName:
                    if (string.IsNullOrEmpty(text))
                        ExceptionHandler.Execute(new NullReferenceException("Server name is null or empty."), nameof(AuthenticationExtensions.Valid));

                    if (text.Length > ApplicationConfig.MaxServerNameLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MaxLength, MessageMaxLength);

                    if (text.Length <= ApplicationConfig.MinServerNameLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MinLength, MessageMinLength);
                    break;
                case ValidType.Description:
                    if (string.IsNullOrEmpty(text))
                        ExceptionHandler.Execute(new NullReferenceException("Description is null or empty."), nameof(AuthenticationExtensions.Valid));

                    if (text.Length > ApplicationConfig.MaxServerDescription)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MaxLength, MessageMaxLength);

                    if (text.Length <= ApplicationConfig.MinServerDescriptionLength)
                        return Tuple.Create<bool, ErrorCode, string>(default, ErrorCode.MinLength, MessageMinLength);
                    break;
            }

            return Tuple.Create(true, ErrorCode.None, string.Empty);
        }

        public static bool CheckErrorCode(this Tuple<bool, ErrorCode, string> tuple)
        {
            switch (tuple.Item2)
            {
                case ErrorCode.MaxLength:
                    Debug.LogWarning(tuple.Item3);
                    return false;
                case ErrorCode.MinLength:
                    Debug.LogWarning(tuple.Item3);
                    return false;
                default: return true;
            }
        }
    }
}