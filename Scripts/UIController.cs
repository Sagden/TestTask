using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// По факту просто ссылки на необходимые кнопки через синглтон + включение всего после подключения
// Не стал мудрить ничего более сложного, думаю для поставленных задач вполне рабочий вариант
public class UIController : NetworkBehaviour
{
    public static UIController instance;

    [SerializeField] private CustomButton craneBlockButton;
    [SerializeField] private CustomButton forwardMoveButton;
    [SerializeField] private CustomButton createNPCButton;
    [SerializeField] private CustomButton destroyNPCButton;
    [Space]
    [SerializeField] private GameObject content;
    public CustomButton CraneBlockButton => craneBlockButton;
    public CustomButton ForwardMoveButton => forwardMoveButton;
    public CustomButton CreateNPCButton => createNPCButton;
    public CustomButton DestroyNPCButton => destroyNPCButton;

    private void Awake()
    {
        content.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        content.SetActive(true);
    }

}
