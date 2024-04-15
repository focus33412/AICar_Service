using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes
    {
        Linear,
        Loop
    }

    public PathTypes PathType;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathElements;
    public MovementPath NextPath; // Следующий путь
    public GameObject car;
    private int k = 0;
    public void OnDrawGizmos()
    {
        if (PathElements == null || PathElements.Length < 2)
            return;

        for (int i = 1; i < PathElements.Length; i++)
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position);
        }

        if (PathType == PathTypes.Loop)
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (PathElements == null || PathElements.Length < 1)
            yield break;

        while (true)
        {
            yield return PathElements[movingTo];

            if (movingTo >= PathElements.Length - 1)
            {
                // Переход на следующий путь, если текущий завершен
                if (NextPath != null)
                {
                    movingTo = 0; // Установка на начало нового пути
                    yield return null; // Для паузы между путями, если требуется
                    NextPath.StartNextPath(); // Запуск следующего пути
                    yield break;
                }
                else
                {
                    yield break; // Завершение перемещения, если нет следующего пути
                }
            }

            movingTo += movementDirection;
        }
    }
    public void krra()
    {
        k++;
    }
    private void Update()
    {
        if (k > 0)
        {
            car.transform.Translate(transform.up*1);
        car.transform.Rotate(0, 50, 0);
        }
        
    }
    // Запуск следующего пути
    public void StartNextPath()
    {
        if (NextPath != null && NextPath.PathElements.Length > 0)
        {
            // Установка объекта на начало следующего пути
            transform.position = NextPath.PathElements[0].position;
            movingTo = 0;
        }
    }
}