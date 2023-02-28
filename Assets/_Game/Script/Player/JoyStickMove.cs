using UnityEngine;

public class JoyStickMove : MonoBehaviour
{
    public DynamicJoystick joystick;  // reference to the dynamic joystick component
    public float speed = 10f;  // character movement speed

    private void Update()
    {
        MovewithJoyStick();
    }
    public Vector2 GetDirection()
    {
        return joystick.Direction;
    }

    void MovewithJoyStick()
    {
        Vector2 direction = joystick.Direction;
        Vector3 movement = new Vector3(direction.x, 0f, direction.y) * speed * Time.deltaTime;
        transform.position += movement;
    }
}
