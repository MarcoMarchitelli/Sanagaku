using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Sangaku
{
    [RequireComponent(typeof(Button))]
    public class ButtonHandler : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] UnityVoidEvent OnButtonSelected;
        [SerializeField] UnityVoidEvent OnButtonDeselect;

        [SerializeField] UnityVoidEvent OnMouseEnter;
        [SerializeField] UnityVoidEvent OnMouseExit;

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnMouseEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnMouseExit.Invoke();
        }
    }
}