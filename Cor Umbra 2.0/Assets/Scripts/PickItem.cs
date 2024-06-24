using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPickItem += OnPickingItem;
    }

    private void OnPickingItem(GameObject item)
    {
        Debug.Log("Pegou Evento");
        Destroy(item);
    }
}
