using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Отвечает за стрельбу и за вкл/откл головы в зависимости от я/другой игрок
public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject bodyView;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject container;

    private void Start()
    {
        if (isLocalPlayer)
            bodyView.SetActive(false);
        else
            cameraObj.SetActive(false);
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

#if PLATFORM_ANDROID
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
        {
            Fire();
        }
#else
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Fire();
        }
#endif
    }

    [Command]
    private void Fire()
    {
        var arrow = Instantiate(this.arrow, container.transform.position, container.transform.rotation);
        NetworkServer.Spawn(arrow);
    }
}
