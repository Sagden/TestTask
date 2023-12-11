using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : Button
{
    private bool isButtonDown = false;

    public event Action onHold;

    private void Update()
    {
        if (isButtonDown)
        {
            onHold?.Invoke();
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        isButtonDown = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        isButtonDown = false;
    }
}
