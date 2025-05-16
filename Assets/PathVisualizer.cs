using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathVisualizer : MonoBehaviour
{
    public Transform[] waypoints;

    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = waypoints.Length;

        for (int i = 0; i < waypoints.Length; i++)
        {
            line.SetPosition(i, waypoints[i].position);
        }
    }
}
