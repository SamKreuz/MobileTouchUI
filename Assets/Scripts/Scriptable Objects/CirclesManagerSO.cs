using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

[CreateAssetMenu(fileName = "CirclesManager", menuName = "Scriptable Objects/Circles Manager SO")]
public class CirclesManagerSO : ScriptableObject
{
    [SerializeField] public Dictionary<int, CircleManager> circles = new Dictionary<int, CircleManager>();

    [SerializeField] private GameObject circle;
    [SerializeField] private LinesManagerSO linesManager;
    
    public void CreateCircle(Finger finger)
    {
        var newCircle = Instantiate(circle, GetScreenPosition(finger), Quaternion.identity);
        var circleManager = newCircle.GetComponent<CircleManager>();
        circleManager.Setup();

        if (circles.Count >= 1)
        {
            linesManager.CreateLines(circleManager, circles);
        }

        circles.Add(finger.index, circleManager);

        UpdateCircle(finger);
    }

    public void UpdateCircle(Finger finger)
    {
        var index = finger.index;

        if(circles.ContainsKey(index))
        {
            var circle = circles[finger.index];
            circle.UpdatePosition(GetScreenPosition(finger));
            linesManager.UpdateLines(circle.Identifier);
        }
        else
        {
            Debug.Log($"Index {finger.index} not present in cicles dictionary.");
        }
    }

    public void RemoveCircle(Finger finger)
    {
        if (circles.ContainsKey(finger.index))
        {
            var circleToRemove = circles[finger.index];
            linesManager.RemoveLines(circleToRemove.Identifier);
            circleToRemove.Destroy();
            circles.Remove(finger.index);
        }
    }

    private Vector3 GetScreenPosition(Finger finger)
    {
        var fingerPosition = finger.screenPosition;
        var fingerWorldPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
        fingerWorldPosition.z = 0;

        return fingerWorldPosition;
    }
}
