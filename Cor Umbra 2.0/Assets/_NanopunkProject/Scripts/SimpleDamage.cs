using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDamage : MonoBehaviour
{
    [SerializeField] private HumanoidEnemy humanoidEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(humanoidEnemy.strikeDamage, null, other.transform.position);
            other.gameObject.transform.position += new Vector3(0, 0,Mathf.Lerp(0, -0.4f, 1f));
        }
    }
}
