using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float hp;

    [SerializeField] private Collider headCollider;
    [SerializeField] private Collider torsoCollider;
    [SerializeField] private Collider[] ArmLCollider;
    [SerializeField] private Collider[] ArmRCollider;
    [SerializeField] private Collider[] LegLCollider;
    [SerializeField] private Collider[] LegRCollider;

    public void Hitted(Collider collider, float damageReceived)
    {
        if(hp > 0)
        {
            if(collider == headCollider)
            {
                hp -= damageReceived * 2f;
            }
            else if(collider == torsoCollider)
            {
                hp -= damageReceived * 1f;
            }
            else
            {
                hp -= damageReceived * 0.8f;
            }
        }
        

        if(hp > 0)
        {
            Debug.Log("hp = " + hp);
        }
        else
        {
            Debug.Log("Morreu");
        }
    }
}
