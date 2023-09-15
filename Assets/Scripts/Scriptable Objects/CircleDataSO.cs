using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CircleData", menuName = "Scriptable Objects/Circle Data")]
public class CircleDataSO : ScriptableObject
{
    Dictionary<int, UnityEvent> events = new Dictionary<int, UnityEvent>();

    public void OnEnable()
    {
        //var newCircle = Instantiate(graphic, GetScreenPosition(finger), Quaternion.identity);
    }

    public void RegisterFinger(int fingerIndex)
    {
        var raiseEvent = new UnityEvent();
        events.Add(fingerIndex, raiseEvent);
    }

    public void UpdatePosition(int fingerIndex, Vector2 position)
    {
        //positionChangeEvent.Invoke(finger);
    }
}
