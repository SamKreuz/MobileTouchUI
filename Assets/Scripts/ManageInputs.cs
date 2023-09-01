using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class ManageInputs : MonoBehaviour
{
    [SerializeField]
    private GameObject graphic;
    private Dictionary<int, GameObject> circles = new();
    private LineRenderer lineRenderer;

    protected void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += Touch_onFingerDown;
        EnhancedTouch.Touch.onFingerMove += Touch_onFingerMove;
        EnhancedTouch.Touch.onFingerUp += Touch_onFingerUp;

        //lineRenderer = new LineRenderer();
        lineRenderer = this.GetComponent<LineRenderer>();
    }

    protected void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= Touch_onFingerDown;
        EnhancedTouch.Touch.onFingerMove -= Touch_onFingerMove;
        EnhancedTouch.Touch.onFingerUp -= Touch_onFingerUp;
    }

    public void Update()
    {
        if(circles.Count >= 2) 
        {
            var pos1 = circles[0].transform.position;
            var pos2 = circles[1].transform.position;
            lineRenderer.SetPosition(0, pos1);
            lineRenderer.SetPosition(1, pos2);
            Debug.Log("Positions: " + pos1 + ", " + pos2);
        }
    }

    private void Touch_onFingerDown(EnhancedTouch.Finger finger)
    {
        Debug.Log("New Finger Down: " + finger.index);

        var newCircle = Instantiate(graphic, GetScreenPosition(finger), Quaternion.identity);

        circles.Add(finger.index, newCircle);
    }

    /// <summary>
    /// Gets called on every new frame that a finger moved
    /// </summary>
    /// <param name="finger"></param>
    private void Touch_onFingerMove(EnhancedTouch.Finger finger)
    {
        Debug.Log("Finger moved: " + finger.index);

        var currentCircle = circles[finger.index];

        currentCircle.transform.position = GetScreenPosition(finger);
    }

    private void Touch_onFingerUp(EnhancedTouch.Finger finger)
    {
        var circleToRemove = circles[finger.index];
        Destroy(circleToRemove);
        circles.Remove(finger.index);

        Debug.Log($"Circles left: {circles.Count}");
    }

    private Vector3 GetScreenPosition(EnhancedTouch.Finger finger)
    {
        var fingerPosition = finger.screenPosition;
        var fingerWorldPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
        fingerWorldPosition.z = 0;

        return fingerWorldPosition;
    }
}
