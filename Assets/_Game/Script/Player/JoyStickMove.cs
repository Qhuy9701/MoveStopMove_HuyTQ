using UnityEngine;

public class JoyStickMove : MonoBehaviour
{
    public DynamicJoystick joystick;  // reference to the dynamic joystick component
    public float speed = 0.5f;  // character movement speed

    private void Start()
    {
        if(joystick==null)
        {
            joystick = FindObjectOfType<DynamicJoystick>();
        }
    }
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
        Vector3 movement = new Vector3(direction.x, 0f, direction.y) * (speed*10) * Time.deltaTime;
        transform.position += movement;

        // Look at movement direction
        if (movement.magnitude > 0f)
        {
            transform.LookAt(transform.position + movement);
        }
    }

}
