using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal"); // A = -1, D = +1
        transform.Translate(Vector2.right * move * moveSpeed * Time.deltaTime);
    }
}
