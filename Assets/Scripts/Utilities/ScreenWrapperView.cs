using UnityEngine;

/// <summary>
/// This class is used to implement screen wrapping for the object it is attached to.
/// </summary>
public class ScreenWrapperView : MonoBehaviour
{
    private Renderer RendererComponent;

    private bool hasEnteredScreen;
    private float wrappingOffset = 0.05f;

    private void Start()
    {
        RendererComponent = GetComponent<Renderer>();
        hasEnteredScreen = false;
    }

    private void Update()
    {
        if (!hasEnteredScreen)
            CheckIfEntered();
        else if(RendererComponent.isVisible)
            WrapScreen();
    }

    /// <summary>
    /// Method to check if the object has entered the Screen.
    /// </summary>
    private void CheckIfEntered()
    {
        Vector3 viewPointPosition = Camera.main.WorldToViewportPoint(transform.position);
        bool onScreen = viewPointPosition.x > 0 && viewPointPosition.x < 1 && viewPointPosition.y > 0 && viewPointPosition.y < 1;
        if (onScreen) hasEnteredScreen = true;
    }

    /// <summary>
    /// Wraps game object on screen if it goes out of bounds.
    /// </summary>
    private void WrapScreen()
    {

        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;

        if (viewportPosition.x > 1)
        {
            newPosition.x = -(newPosition.x - wrappingOffset);
        }
        if (viewportPosition.x < 0)
        {
            newPosition.x = -(newPosition.x + wrappingOffset);
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -(newPosition.y - wrappingOffset);
        }
        if (viewportPosition.y < 0)
        {
            newPosition.y = -(newPosition.y + wrappingOffset);
        }

        transform.position = newPosition;
    }
}