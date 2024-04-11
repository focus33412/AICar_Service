using UnityEngine;

public class MouseDragCamera : MonoBehaviour
{
    public float minYPosition = 1f;           // ћинимальна€ допустима€ позици€ камеры по оси Y
    public float maxYPosition = 10f;          // ћаксимальна€ допустима€ позици€ камеры по оси Y
    public float movementSensitivity = 0.1f;  // „увствительность перемещени€ камеры
    public float maxXMovement = 10f;          // ћаксимальное разрешенное перемещение камеры по оси X
    public float minXMovement = -10f;         // ћинимальное разрешенное перемещение камеры по оси X
    public float maxZMovement = 10f;          // ћаксимальное разрешенное перемещение камеры по оси Z
    public float minZMovement = -10f;         // ћинимальное разрешенное перемещение камеры по оси Z

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

            // ѕримен€ем ограничители на перемещение по ос€м X и Z
            Vector3 newPosition = originalPosition + move;
            newPosition.y = Mathf.Clamp(newPosition.y, minYPosition, maxYPosition); // ќграничиваем положение по оси Y
            newPosition.x = Mathf.Clamp(newPosition.x, minXMovement, maxXMovement); // ќграничиваем перемещение по оси X
            newPosition.z = Mathf.Clamp(newPosition.z, minZMovement, maxZMovement); // ќграничиваем перемещение по оси Z

            // ѕримен€ем новую позицию камеры
            transform.position = newPosition;

            // ¬ыводим координаты камеры в консоль после каждого перемещени€
            Debug.Log("Camera Position: " + transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            // ¬ыводим координаты камеры в консоль после отпускани€ мыши
            Debug.Log("Final Camera Position: " + transform.position);
        }
    }
}
