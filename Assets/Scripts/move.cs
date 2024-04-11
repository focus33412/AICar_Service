using UnityEngine;

public class MouseDragCamera : MonoBehaviour
{
    public float minYPosition = 1f;          // ћинимальна€ допустима€ позици€ камеры по оси Y
    public float maxYPosition = 10f;         // ћаксимальна€ допустима€ позици€ камеры по оси Y
    public float movementSensitivity = 0.1f; // „увствительность перемещени€ камеры

    private Vector3 dragOrigin;        // Ќачальна€ позици€ мыши при начале перетаскивани€
    private Vector3 originalPosition;  // »сходна€ позици€ камеры при начале перетаскивани€

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // «апоминаем начальную позицию мыши и позицию камеры при нажатии левой кнопки мыши
            dragOrigin = Input.mousePosition;
            originalPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            // ¬ычисл€ем вектор перемещени€ в плоскости экрана (X, Y)
            Vector3 offset = (Input.mousePosition - dragOrigin) * movementSensitivity;

            // ѕреобразуем вектор перемещени€ из плоскости экрана в мировое пространство (X, Z)
            Vector3 move = new Vector3(-offset.x, 0, -offset.y);

            // ѕримен€ем вектор перемещени€ к позиции камеры
            Vector3 newPosition = originalPosition + move;
            newPosition.y = Mathf.Clamp(newPosition.y, minYPosition, maxYPosition); // ќграничиваем положение по оси Y
            transform.position = newPosition;
        }
    }
}
