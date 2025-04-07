using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnChar : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    private void OnTriggerEnter(Collider other)
    {
        playerController.TakeDamage(10, other, new Vector3(0,0,0));
    }
}
