using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskFollower : MonoBehaviour
{
    public Transform target; // referensi ke Fire
    void Update()
    {
        transform.position = target.position;
    }
}
