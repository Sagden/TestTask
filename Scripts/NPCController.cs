using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс собирающий и управляющий всеми NPC
// Так как NPC-объекты общие для всех храню их на сервере
public class NPCController : NetworkBehaviour
{
    public static NPCController instance;

    private List<GameObject> allNPC = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Контроллер нужен онли на сервере, если это не сервер — самоуничтожаемся
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (isClientOnly)
        {
            Destroy(gameObject);
        }
    }

    public void AddNPC(GameObject obj)
    {
        allNPC.Add(obj);
    }

    public void DestroyAllNPC()
    {
        foreach (var obj in allNPC)
        {
            NetworkServer.Destroy(obj);
        }
        allNPC.Clear();
    }
}
