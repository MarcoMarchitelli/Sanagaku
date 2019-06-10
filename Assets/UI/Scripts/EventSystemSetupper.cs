using UnityEngine;
using UnityEngine.EventSystems;

namespace  Sangaku
{
    [RequireComponent(typeof(EventSystem))]
    public class EventSystemSetupper : MonoBehaviour
    {
        private void Awake()
        {
            EventSystem eventSystem = GetComponent<EventSystem>();
            if (!CheckControllerConnection())
            {
                eventSystem.firstSelectedGameObject = null;
            }
        }

        bool CheckControllerConnection()
        {
            string[] controllerNames = Input.GetJoystickNames();

            if (controllerNames.Length == 0)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < controllerNames.Length; i++)
                {
                    if (!string.IsNullOrEmpty(controllerNames[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    } 
}
