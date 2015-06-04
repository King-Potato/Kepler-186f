using UnityEngine;
using System.Collections.Generic;

public class ZoomToFit : MonoBehaviour
{
  public float MinDistance = 5.0f;

  public float DistanceScale = 100.0f;

  public List<Transform> Targets;

  void FixedUpdate()
  {
    // Get bounds containing all targets.
    Rect bounds = new Rect();

    foreach (var t in Targets)
    {
      bounds.xMax = Mathf.Max(t.position.x, bounds.xMax);
      bounds.yMax = Mathf.Max(t.position.y, bounds.yMax);
      bounds.xMin = Mathf.Min(t.position.x, bounds.xMin);
      bounds.yMin = Mathf.Min(t.position.y, bounds.yMin);
    }

    // Get square diagonal size of bounds.
    float size = Mathf.Sqrt(bounds.width * bounds.width + bounds.height * bounds.height);
    size = Mathf.Sqrt(size / 100.0f);

    float z = -size * DistanceScale;
    if (z > -MinDistance) z = -MinDistance;

    transform.position = new Vector3(bounds.center.x, bounds.center.y, z);
  }
}
