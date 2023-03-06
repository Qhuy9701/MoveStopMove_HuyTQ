public class PlayerController : CharacterController
{
    public JoyStickMove joyStickMove;  // reference to the JoyStickMove script


    public void Start()
    {
        if(joyStickMove == null)
        {
            //find the joystick
            joyStickMove = FindObjectOfType<JoyStickMove>();
        }
    }
    public override void OnInit()
    {
        base.OnInit();
    }

    
}

