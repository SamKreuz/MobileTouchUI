using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class ManageInputs : MonoBehaviour
{
    [SerializeField] private GameObject graphic;
    [SerializeField] private Material lineMaterial;

    private float lineWidth = 0.1f;
    private Dictionary<int, GameObject> circles = new();
    private List<Line> linesList = new();
    private Animator animator;

    protected void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += Touch_onFingerDown;
        EnhancedTouch.Touch.onFingerMove += Touch_onFingerMove;
        EnhancedTouch.Touch.onFingerUp += Touch_onFingerUp;
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
        // TODO SK: Delete when not needed anymore
    }

    private void Touch_onFingerDown(EnhancedTouch.Finger finger)
    {
        Debug.Log("New Finger Down: " + finger.index);
        Debug.Log("Active Lines: " + linesList.Count());

        var newCircle = Instantiate(graphic, GetScreenPosition(finger), Quaternion.identity);

        Guid newCircleId = Guid.NewGuid();
        newCircle.name = newCircleId.ToString();

        animator = newCircle.GetComponent<Animator>();
        animator.Play("CircleScaleUp");

        CreateLines(newCircle);

        circles.Add(finger.index, newCircle);
        //circlesList.Add(new Circle(finger.index, newCircle.name, newCircle));
    }

    /// <summary>
    /// Gets called on every new frame that a finger moved
    /// </summary>
    /// <param name="finger"></param>
    private void Touch_onFingerMove(EnhancedTouch.Finger finger)
    {
        var currentCircle = circles[finger.index];

        currentCircle.transform.position = GetScreenPosition(finger);

        Debug.Log("Finger moved: " + finger.index + ", Name: " + currentCircle.name);

        linesList.Where(x => x.ContainsPoint(currentCircle.name)).ToList().ForEach(x => x.UpdatePosition());
    }

    private void Touch_onFingerUp(EnhancedTouch.Finger finger)
    {
        var circleToRemove = circles[finger.index];

        Debug.Log("Finger removed: " + finger.index + ", Name: " + circleToRemove.name);

        var linesToRemove = linesList.Where(x => x.ContainsPoint(circleToRemove.name)).ToList();
        linesToRemove.ForEach(x => x.Remove());
        linesList.RemoveAll(x => x.ContainsPoint(circleToRemove.name));

        Destroy(circleToRemove);
        circles.Remove(finger.index);
        //circlesList.RemoveAll(x => x.Index == finger.index);

        Debug.Log($"Circles left: {circles.Count}");
    }

    private Vector3 GetScreenPosition(EnhancedTouch.Finger finger)
    {
        var fingerPosition = finger.screenPosition;
        var fingerWorldPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
        fingerWorldPosition.z = 0;

        return fingerWorldPosition;
    }

    private void CreateLines(GameObject newCircle)
    {
        // for each finger/circle create a new line to this newly created one
        foreach (var circle in circles.Values)
        {
            var pos1 = newCircle.transform.position;
            var pos2 = circle.transform.position;

            GameObject lineObject = new GameObject(Guid.NewGuid().ToString());

            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
            if (lineRenderer != null)
            {
                lineRenderer.SetPosition(0, pos1);
                lineRenderer.SetPosition(1, pos2);
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
                lineRenderer.material = lineMaterial;
            }
            else
            {
                Debug.LogWarning("LineRenderer could not be created.");
            }

            linesList.Add(new Line(lineObject, newCircle, circle));
        }
    }
}
