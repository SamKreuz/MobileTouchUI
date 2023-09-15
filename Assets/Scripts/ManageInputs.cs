using UnityEngine;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class ManageInputs : MonoBehaviour
{
    [SerializeField] private CirclesManagerSO circlesManager;

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

    public void Start()
    {
        bool pressureSupport = Input.touchPressureSupported;
        Debug.Log($"Pressure support: {pressureSupport}");
    }

    public void Update()
    {
        // TODO SK: Delete when not needed anymore
        var touches = EnhancedTouch.Touch.activeTouches;

        foreach (var touch in touches)
        {
            var radius = touch.radius;
            var position = touch.screenPosition;

            Debug.Log($"Radius: {radius}, Position: {position}");
        }
    }

    private void Touch_onFingerDown(EnhancedTouch.Finger finger)
    {
        Debug.Log("New Finger Down: " + finger.index);

        if (EnhancedTouch.Touch.activeTouches.Count <= 5)
        {
            circlesManager.CreateCircle(finger);
        }
    }

    /// <summary>
    /// Gets called on every new frame that a finger moved
    /// </summary>
    /// <param name="finger"></param>
    private void Touch_onFingerMove(EnhancedTouch.Finger finger)
    {
        circlesManager.UpdateCircle(finger);
    }

    private void Touch_onFingerUp(EnhancedTouch.Finger finger)
    {
        circlesManager.RemoveCircle(finger);
    }
}
