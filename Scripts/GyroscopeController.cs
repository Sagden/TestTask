using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Отвечает за повороты камеры по гироскопу и блокировку крена
public class GyroscopeController : NetworkBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform container;

    private bool gyroEnabled;
    private Gyroscope gyro;

    private Quaternion rot;
    private bool allowHorizontalRotation;
    private float craneCoord;

    void Start()
    {
        if (!isLocalPlayer) return;

        UIController.instance.CraneBlockButton.onClick.AddListener(() =>
        {
            craneCoord = container.transform.eulerAngles.z;
            allowHorizontalRotation = !allowHorizontalRotation;
        });

        gyroEnabled = EnableGyro();
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        Debug.LogError("Гироскоп на устройстве не поддерживается!");
        return false;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (allowHorizontalRotation)
        {
            // Блокируем крен
            cam.eulerAngles = new Vector3(cam.eulerAngles.x, cam.eulerAngles.y, craneCoord);
        }
        else
        {
            cam.localEulerAngles = Vector3.zero;
        }

        if (gyroEnabled)
        {
            // Непосредственно движение камеры по гироскопу
            Quaternion newRotation = gyro.attitude * rot;
            container.transform.localRotation = newRotation;
        }
    }

    private void OnDestroy()
    {
        if (!isLocalPlayer) return;
        UIController.instance.CraneBlockButton.onClick.RemoveAllListeners();
    }
}
