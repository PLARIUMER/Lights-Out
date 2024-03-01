using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Transform targetObject;
    private Vector2 cursorHotspot;

    private void Update()
    {
        cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);

        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPos.z = 0f; 

        targetObject.position = cursorPos;
    }
}
