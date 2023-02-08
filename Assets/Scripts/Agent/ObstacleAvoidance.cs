using UnityEngine;

public class ObstacleAvoidance : MonoBehaviour
{
    public Transform raycastTransform;
    [Range(1, 40)] public float distance = 1;
    [Range(0, 180)] public float maxAngle = 45;
    [Range(2, 50)] public int numRaycast = 2;

    public LayerMask layerMask;

    private Ray ray;

    /*    private void Start()
        {
            distance = Mathf.Clamp(distance, 1, 40);
            maxAngle = Mathf.Clamp(maxAngle, 0, 180);
            numRaycast = Mathf.Clamp(numRaycast, 2, 50);
            ray = new Ray(transform.position, transform.forward);
        }*/

    public bool IsObstacleInFront()
    {
        Ray ray = new Ray(raycastTransform.position, raycastTransform.forward);

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.green);

        // check if object is in front of agent 
        return Physics.SphereCast(ray, 2, distance, layerMask);
    }

    public Vector3 GetOpenDirection()
    {

        Vector3[] directions = Utilities.GetDirectionsInCircle(numRaycast, maxAngle);
        foreach (var direction in directions)
        {
            // cast ray from transform position towards direction 
            Ray ray = new Ray(raycastTransform.position, raycastTransform.rotation * direction);

            if (!Physics.SphereCast(ray, 2, distance, layerMask))
            {
                return ray.direction;
            }
        }
        return transform.forward;
    }

}
