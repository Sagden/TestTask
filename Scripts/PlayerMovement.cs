using Mirror;
using UnityEngine;

// Движение игрока вперед по кнопке!
public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GyroscopeController gyroscopeController;
    [SerializeField] private GameObject rotationContainer;

    private void Start()
    {
        if (isLocalPlayer)
            UIController.instance.ForwardMoveButton.onHold += MoveButtonHold;
    }

    private void OnDestroy()
    {
        if (isLocalPlayer)
            UIController.instance.ForwardMoveButton.onHold -= MoveButtonHold;
    }

    private void MoveButtonHold()
    {
        Vector3 forward = rotationContainer.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        transform.Translate(forward * speed * Time.deltaTime, Space.World);
    }
}
