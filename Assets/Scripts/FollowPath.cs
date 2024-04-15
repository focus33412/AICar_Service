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
    public float speed = 1;
    public float maxDistance = .1f;
    public float rotationSpeed = 5f; // скорость поворота

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

        if (pointInPath.Current == null)
        {
            Debug.Log("Нужны точки");
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    private void Update()
    {
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        }



        if (Type == MovementType.Moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }
        else if (Type == MovementType.Lerping)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        var distanceSqure = (transform.position - pointInPath.Current.position).sqrMagnitude;

        if (distanceSqure < maxDistance * maxDistance)
        {
            pointInPath.MoveNext();
        }
    }

    // Метод для поворота объекта в сторону цели

}