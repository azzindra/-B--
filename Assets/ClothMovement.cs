using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothMovement : MonoBehaviour
{
    public Transform[] pathPoints;
    public float duration = 2f;

    void Start()
    {
        Vector3[] pathPositions = new Vector3[pathPoints.Length];
        for (int i = 0; i < pathPoints.Length; i++)
        {
            pathPositions[i] = pathPoints[i].position;
        }

        // Gerakkan objek mengikuti jalur dengan kurva halus
        transform.DOPath(pathPositions, duration, PathType.CatmullRom)
                 .SetEase(Ease.Linear);
    }
}
