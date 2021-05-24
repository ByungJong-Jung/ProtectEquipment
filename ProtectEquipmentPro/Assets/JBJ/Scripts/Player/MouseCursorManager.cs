using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseCursorManager : MonoBehaviour
{
    private static MouseCursorManager instance;
    public static MouseCursorManager GetInstance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(MouseCursorManager)) as MouseCursorManager;
            if (!instance)
            {
                Debug.LogError("no one");
            }
        }
        return instance;
    }


    [Header("마우스커서")]
    [SerializeField] private SpriteRenderer cursorRenderer;
    [SerializeField] private Sprite[] mouseCursor;

    [SerializeField] private Camera mainCamera;

    public void Init_MouseCursor(Camera _mainCamera)
    {
        Cursor.visible = false;
        cursorRenderer = GetComponent<SpriteRenderer>();
        ChangeCursorImage(MOUSE_CURSOR_TYPE.DEFAULT);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void ChangeCursorImage(MOUSE_CURSOR_TYPE _cursorType)
    {
        cursorRenderer.sprite = mouseCursor[(int)_cursorType];
    }

    private void Update()
    {
        Vector2 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
