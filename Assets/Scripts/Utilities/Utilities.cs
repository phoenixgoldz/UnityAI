using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Utilities : MonoBehaviour
{

    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        if (result.x > max.x) { result.x = min.x; }
        else if (result.x < min.x) { result.x = max.x; }

        if (result.y > max.y) { result.y = min.y; }
        else if (result.y < min.y) { result.y = max.y; }

        if (result.z > max.z) { result.z = min.z; }
        else if (result.z < min.z) { result.z = max.z; }

        return result;
    }
    void Update()
    {
        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }
}
