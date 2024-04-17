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
    public MovementPath NextPath;
    public GameObject car;
    private int k = 0;

    public int transitionCondition = 0;  // ”словие дл€ начала следующего пути

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
            Gizmos.DrawLine(PathElements[PathElements.Length - 1].position, PathElements[0].position);
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
                if (NextPath != null && transitionCondition >= 4)
                {
                    movingTo = 0;
                      // ѕауза перед переходом на следующий путь
                    NextPath.StartNextPath();
                    yield break;
                }
                else
                {
                    // ќжидание событи€ дл€ перехода
                    yield return null;
                }
            }
            else
            {
                movingTo += movementDirection;
            }
        }
    }

    public void StartNextPath()
    {
        if (NextPath != null && NextPath.PathElements.Length > 0 && transitionCondition >= 4)
        {
            transform.position = NextPath.PathElements[0].position;
            movingTo = 0;
            transitionCondition = 0;  // —брос услови€ дл€ следующего пути
        }
    }

    public void cond()
    {
        transitionCondition++;
    }
}
