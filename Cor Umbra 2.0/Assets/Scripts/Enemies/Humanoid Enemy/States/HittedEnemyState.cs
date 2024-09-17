using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedEnemyState : State
{
    [SerializeField] private float health;

    [SerializeField] private DieEnemyState dieState;

    [SerializeField] private Collider headCollider;
    [SerializeField] private Collider torsoCollider;
    [SerializeField] private Collider[] ArmLCollider;
    [SerializeField] private Collider[] ArmRCollider;
    [SerializeField] private Collider[] LegLCollider;
    [SerializeField] private Collider[] LegRCollider;

    public void Hitted(Collider collider, float damageReceived)
    {
        if (health > 0)
        {
            if (collider == headCollider)
            {
                health -= damageReceived * 2f;
            }
            else if (collider == torsoCollider)
            {
                health -= damageReceived * 1f;
            }
            else
            {
                health -= damageReceived * 0.8f;
            }
        }

        Debug.Log(health);
    }

    public override void Do()
    {
       
    }
}
