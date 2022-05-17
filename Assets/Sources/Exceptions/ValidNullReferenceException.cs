using System;

namespace Client.Exceptions
{
    public static class ValidNullReferenceException
    {
        public static void Execute(Type type, string methodName)
        {
            if (type == null)
                ExceptionHandler.Execute(new NullReferenceException(), methodName);
        }
    }
}