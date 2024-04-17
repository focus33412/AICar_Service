using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        Moving,
        Lerping
    }

    public MovementType Type = MovementType.Moving;
    public MovementPath MyPath;
    public float speed = 1f;
    public float maxDistance = 0.1f;
    public float rotationSpeed = 5f;

    private IEnumerator<Transform> pointInPath;

    private void Start()
    {
        if (MyPath == null)
        {
            Debug.Log("Примени путь");
            return;
        }

        pointInPath = MyPath.GetNextPathPoint();
        pointInPath.MoveNext();
        transform.position = pointInPath.Current.position;  // Стартовая позиция
    }

    private void Update()
    {
        if (pointInPath == null || pointInPath.Current == null || MyPath.transitionCondition < 4)
        {
            return;
        }

        RotateTowards(pointInPath.Current.position);

        if (Type == MovementType.Moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }
        else if (Type == MovementType.Lerping)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        float distanceSquare = (transform.position - pointInPath.Current.position).sqrMagnitude;

        if (distanceSquare < maxDistance * maxDistance && MyPath.movingTo < MyPath.PathElements.Length - 1)
        {
            pointInPath.MoveNext();
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
