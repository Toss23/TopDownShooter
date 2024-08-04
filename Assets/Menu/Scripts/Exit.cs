using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Exit : MonoBehaviour
{
    private Button _exitButton;

    private void Awake()
    {
        _exitButton = GetComponent<Button>();
        _exitButton.onClick.AddListener(() => Application.Quit());
    }
}
