using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Core : MonoBehaviour
{
    //public Rigidbody rb;
    public Animator animator;

    public StateMachine machine;
    public GroundSensor groundSensor;

    public State state => machine.state;
    private Vector3 position = new Vector3 (0,1,0);

    protected void Set(State newState, bool forceReset = false)
    {
        machine.Set(newState, forceReset);
    }

    public void SetupInstances()
    {
        machine = new StateMachine();

        State[] allChildStates = GetComponentsInChildren<State>();
        foreach (State state in allChildStates)
        {
            state.SetCore(this);
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(Application.isPlaying)
        {
            List<State> states = machine.GetActiveStateBranch();
            UnityEditor.Handles.Label(transform.position + position, "Active States: " + string.Join(" > ", states));
        }
#endif
    }

}
