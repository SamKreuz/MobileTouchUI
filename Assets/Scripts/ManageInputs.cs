using UnityEngine;
using UnityEngine.InputSystem;

public class ManageInputs : MonoBehaviour
{
    [SerializeField]
    private GameObject graphic;

    private PlayerInput playerInput;
    private InputAction touchPositionAction;
    private InputAction touchPressAction;
    private InputAction touchPressureAction;

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["SingleTouchPosition"];
        touchPressAction = playerInput.actions["SingleTouchPress"];
        touchPressureAction = playerInput.actions["Test"];
    }

    public void Update()
    {
        //float pressure = touchPressureAction.ReadValue<float>();
        //Debug.Log(pressure);

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            var fingerPosition = touch.position;
            Debug.Log(fingerPosition);
            var fingerWorldPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
            fingerWorldPosition.z = 0;
            graphic.transform.position = fingerWorldPosition;
        }

    }

    private void OnEnable()
    {
        //touchPressAction.performed += TouchPressed;
    }
    private void OnDisable()
    {
        //touchPressAction.performed -= TouchPressed;
    }

    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 fingerPosition = touchPositionAction.ReadValue<Vector2>();
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(fingerPosition);
        worldPosition.z = 0;

        graphic.transform.position = worldPosition;
    }
}
