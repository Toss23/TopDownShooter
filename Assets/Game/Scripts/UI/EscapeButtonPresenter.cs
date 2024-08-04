using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class EscapeButtonPresenter : MonoBehaviour, IInitable
{
    private Button _button;

    public void PreInit(IContext context)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(context.EscapeFromGame);
    }

    public void Init(IContext context)
    {
        
    }
}
