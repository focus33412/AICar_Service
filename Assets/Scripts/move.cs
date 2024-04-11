using UnityEngine;

public class MouseDragCamera : MonoBehaviour
{
    public float dragSpeed = 2f;
    public float minYPosition = 1f; // Минимальная позиция по оси Y

    private Vector3 dragOrigin;
    private float startYPosition;

    void Start()
    {
        startYPosition = transform.position.y; // Запоминаем стартовую позицию камеры по оси Y
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed, 0, -pos.y * dragSpeed);

        // Применяем перемещение камеры
        transform.Translate(move, Space.World);

        // Ограничиваем позицию камеры по оси Y
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, startYPosition, Mathf.Infinity);
        transform.position = clampedPosition;
    }
}
