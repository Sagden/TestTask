using Mirror;
using System.Collections.Generic;
using UnityEngine;

// Создатель NPC перед игроком!
// Не создает экземпляр для локального себя, даёт команду серверу создать NPC
// Именно так потому что NPC объект общий, а не локальный, и удалять потом одновременно всех NPC так удобнее
public class NPCCreator : NetworkBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private GameObject container;

    [SerializeField] private float npcHeight = 0.496f;

    private void Start()
    {
        if (isLocalPlayer)
        {
            UIController.instance.CreateNPCButton.onClick.AddListener(CreateNPC);
            UIController.instance.DestroyNPCButton.onClick.AddListener(DestroyNpc);
        } 
    }

    [Command]
    private void DestroyNpc()
    {
        NPCController.instance.DestroyAllNPC();
    }

    [Command]
    private void CreateNPC()
    {
        Vector3 forward = container.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();

        var obj = Instantiate(npcPrefab, transform.position + forward * 5, Quaternion.identity);
        obj.transform.position = new Vector3(obj.transform.position.x, npcHeight, obj.transform.position.z);
        NPCController.instance.AddNPC(obj);
        NetworkServer.Spawn(obj, NetworkServer.localConnection);
    }

    private void OnDestroy()
    {
        if (isLocalPlayer)
        {
            UIController.instance.CreateNPCButton.onClick.RemoveAllListeners();
            UIController.instance.DestroyNPCButton.onClick.RemoveAllListeners();
        }
    }
}
