using System;

namespace Client.Exceptions
{
    public static class ValidNullReferenceException
    {
        public static void Execute(object type, string methodName)
        {
            if (type is null)
                ExceptionHandler.Execute(new NullReferenceException(), methodName);
        }
    }
}