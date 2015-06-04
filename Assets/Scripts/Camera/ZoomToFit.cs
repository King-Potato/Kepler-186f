﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ZoomToFit : MonoBehaviour
{
  public float MinDistance = 5.0f;

  public float DistanceScale = 100.0f;

  public List<Transform> Targets;

  void FixedUpdate()
  {
    // Get bounds containing all targets.
    Rect bounds = new Rect();
    bounds.xMin = Targets.Min(t => t.position.x);
    bounds.yMin = Targets.Min(t => t.position.y);
    bounds.xMax = Targets.Max(t => t.position.x);
    bounds.yMax = Targets.Max(t => t.position.y);
    
    // Get square diagonal size of bounds.
    float size = Mathf.Sqrt(bounds.width * bounds.width + bounds.height * bounds.height);
    size = Mathf.Sqrt(size / 100.0f);

    float z = -size * DistanceScale;
    if (z > -MinDistance) z = -MinDistance;

    transform.position = new Vector3(bounds.center.x, bounds.center.y, z);

    Debug.DrawLine(new Vector3(bounds.xMin, bounds.yMin), new Vector3(bounds.xMin, bounds.yMax));
    Debug.DrawLine(new Vector3(bounds.xMax, bounds.yMin), new Vector3(bounds.xMax, bounds.yMax));
    Debug.DrawLine(new Vector3(bounds.xMin, bounds.yMin), new Vector3(bounds.xMax, bounds.yMin));
    Debug.DrawLine(new Vector3(bounds.xMin, bounds.yMax), new Vector3(bounds.xMax, bounds.yMax));
  }
}
