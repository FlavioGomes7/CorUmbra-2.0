using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidEnemy : Core
{
    [SerializeField] PatrolEnemyState patrolState;
    //[SerializeField] ChaseEnemyState chaseState;

    public void Start()
    {
        SetupInstances();
        Set(patrolState);
    }

    public void Update()
    {
        SelectState();
    }

    private void SelectState()
    {
        if(state.isCompleted)
        {
            if(groundSensor.grounded)
            {
                Set(patrolState);
            }
        }

        state.DoBranch();
    }

}
