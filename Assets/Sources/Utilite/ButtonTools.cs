using System;
using UnityEngine.UI;
using Client.Exceptions;
using UnityEngine.Events;

namespace Client.Utilite
{
    public static class ButtonTools
    {
        public static void AddListener(this Button button, UnityAction action, bool clearOldListener = default)
        {
            if (button is null)
                ExceptionHandler.Execute(new NullReferenceException("Button is null."), nameof(ButtonTools.AddListener));

            if (action is null)
                ExceptionHandler.Execute(new NullReferenceException("Action is null."), nameof(ButtonTools.AddListener));

            if (clearOldListener)
                button.onClick.RemoveAllListeners();

            button.onClick.AddListener(action);
        }

        public static void AddListeners(this Button button, UnityAction[] actions, bool clearOldListener = default)
        {
            if (button is null)
                ExceptionHandler.Execute(new NullReferenceException("Button is null."), nameof(ButtonTools.AddListeners));

            if (actions is null)
                ExceptionHandler.Execute(new NullReferenceException("Action is null."), nameof(ButtonTools.AddListeners));

            if (clearOldListener)
                button.onClick.RemoveAllListeners();

            for (int iterator = 0; iterator < actions.Length; iterator++)
                if (!(actions[iterator] is null))
                    button.onClick.AddListener(actions[iterator]);
        }
    }
}