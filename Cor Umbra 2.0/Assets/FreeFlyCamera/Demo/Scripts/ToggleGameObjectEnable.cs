using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleGameObjectEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject _go;

    private bool _enabled = true;
    private InputAction _toggleAction;

    private void Awake()
    {
        // Configuração do InputAction para a tecla H
        _toggleAction = new InputAction(binding: "<Keyboard>/h");
        _toggleAction.performed += context => ToggleGameObject();
        _toggleAction.Enable();
    }

    private void OnDestroy()
    {
        // Desabilitar o InputAction quando o objeto for destruído
        _toggleAction.Disable();
    }

    private void ToggleGameObject()
    {
        _enabled = !_enabled;
        _go.SetActive(_enabled);
    }
}
