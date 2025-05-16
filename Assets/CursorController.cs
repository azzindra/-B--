using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform monitorBounds;
    public float speed = 200f;

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 newPosition = cursor.localPosition + (Vector3)(input * speed * Time.deltaTime);

        // Clamp ke monitor area
        Vector3 minBounds = monitorBounds.rect.min;
        Vector3 maxBounds = monitorBounds.rect.max;

        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);
        cursor.localPosition = newPosition;
    }
}
