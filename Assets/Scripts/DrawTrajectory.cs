using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    public LineRenderer lineRenderer;

    [SerializeField]
    public int lineSegmentCount = 20;

    public Material redMaterial, greenMaterial;

    private List<Vector3> linePoints = new List<Vector3>();

    #region Singleton

    public static DrawTrajectory Instance;

    private void Start()
    {
        lineRenderer.material = redMaterial;
    }

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        float FlightDurationToGround = Mathf.Sqrt((velocity.y * velocity.y) / (Physics.gravity.y * Physics.gravity.y) - (2 * startingPoint.y / Physics.gravity.y));

        float FlightDuration = ((2 * velocity.y) / Physics.gravity.y) - FlightDurationToGround;

        float stepTime = (FlightDuration / lineSegmentCount);
        linePoints.Clear();

        for (int i = 0; i <= lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed);

            RaycastHit hit;


            if (Physics.Raycast(startingPoint, -MovementVector, out hit, MovementVector.magnitude))
            {
                if(hit.collider.name == "Plane")
                {
                    lineRenderer.material = redMaterial;
                }
                else if(hit.collider.name != "Plane")
                {
                    lineRenderer.material = greenMaterial;
                }
                break;
            }
            linePoints.Add(-MovementVector + startingPoint);
        }

        Vector3 secondBouncingStartPoint = linePoints[linePoints.Count - 1];
        /////// SECOND BOUNCING

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed);

            RaycastHit hit;

            if (Physics.Raycast(secondBouncingStartPoint, -MovementVector, out hit, MovementVector.magnitude))
            {
                if (hit.collider.name == "Plane")
                {
                    lineRenderer.material = redMaterial;
                }
                else if (hit.collider.name != "Plane")
                {
                    lineRenderer.material = greenMaterial;
                }
                break;
            }

            linePoints.Add(-MovementVector + secondBouncingStartPoint);
        }
        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }
    public void HideLine()
    {
        lineRenderer.positionCount = 0;
    }
}
