using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorPress;
    [SerializeField] private Texture2D cursorReload;

    private void Start()
    {
        Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        ChangeCursor();
    }

    public void ChangeCursor()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Cursor.SetCursor(cursorPress, Vector2.zero, CursorMode.Auto);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
        }
        else if (Keyboard.current.rKey.isPressed)
        {
            Cursor.SetCursor(cursorReload, Vector2.zero, CursorMode.Auto);
        }
         else if (Keyboard.current.rKey.wasReleasedThisFrame)
        {
            Cursor.SetCursor(cursorNormal, Vector2.zero, CursorMode.Auto);
        }
    }
}
