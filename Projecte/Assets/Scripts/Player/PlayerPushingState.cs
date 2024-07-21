using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushingState : PlayerState
{
    private Collider collider;
    public PlayerPushingState(PlayerController controller) : base(controller)
    {
        collider = controller.GetComponent<Collider>();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        Animator animator = playerController.GetComponent<Animator>();
        animator.SetTrigger("Push");
    }

    // Se llama a push con un trigger en mitad de la animacion de push para que cuadre mas con el timing visual
    public override void Push()
    {
        RaycastHit hit;
        if (Physics.Raycast(collider.bounds.center, playerController.transform.forward, out hit, 1f) && hit.collider.tag == "Pushable")
        {
            GameObject pushableObject = hit.collider.gameObject;
            PushableObject pushableObjectScript = pushableObject.GetComponent<PushableObject>();

            Vector3 pushDirection = CalculatePushDirection(pushableObject);
            if (!pushableObjectScript.GetPushing() && pushableObjectScript.CanPushObject(pushDirection))
            {
                pushableObjectScript.StartPushing(pushDirection);
                return;
            }
        } 
    }

    public override void Exit()
    {
        PlayerState newState = new PlayerIdleState(playerController);
        ChangeState(newState);
    }

    private Vector3 CalculatePushDirection(GameObject pushableObject)
    {
        Vector3 pushDirection = new Vector3();

        Collider pushableCollider = pushableObject.GetComponent<Collider>();
        float xDiference = pushableCollider.bounds.center.x - collider.bounds.center.x;
        float zDiference = pushableCollider.bounds.center.z - collider.bounds.center.z;

        if (Mathf.Abs(xDiference) > Mathf.Abs(zDiference)) 
        {
            pushDirection.x = xDiference;
        }
        else
        {
            pushDirection.z = zDiference;
        }
        pushDirection.Normalize();

        return pushDirection;
    }
}
