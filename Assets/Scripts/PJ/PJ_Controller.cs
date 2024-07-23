using UnityEngine;

public class PJ_Controller 
{
    PJ_Detective player;
    string horizontalAxis = "Horizontal";
    string fireButton = "Fire1";

    private float movementInput;
    public PJ_Controller(PJ_Detective p)
    {
        player = p;
    }
    public void OnUpdate()
    {
        movementInput = Input.GetAxis(horizontalAxis);
        if (movementInput != 0)
        {
            player.Movement(movementInput, false);
        }
        else
        {
            player.Movement(movementInput, true);
        }
        if (Input.GetButtonDown(fireButton)) 
        {
            player.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.Reload();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.RepairBarrier();
        }
    }
}
