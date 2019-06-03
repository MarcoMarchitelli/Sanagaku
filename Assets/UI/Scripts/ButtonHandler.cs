using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Sangaku
{
    [RequireComponent(typeof(Button))]
    public class ButtonHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        [SerializeField] UnityVoidEvent OnButtonSelected;
        [SerializeField] UnityVoidEvent OnButtonDeselect;

        Button button;

        public void OnSelect(BaseEventData eventData)
        {
            OnButtonSelected.Invoke();
        }

        public void OnDeselect(BaseEventData eventData)
        {
            OnButtonDeselect.Invoke();
        }

        void Start()
        {
            button = GetComponent<Button>();
        }
    }
}