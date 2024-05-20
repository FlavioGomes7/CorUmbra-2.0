using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    private InputHandler inputHandler;
    private Animator animator;
    private int velocityHash;
    private float velocityX = 0f;
    private float velocityZ = 0f;
    private float[] thresholdX = new float[2];
    private float[] thresholdZ = new float[2];
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private void HandleBlendMove()
    {
        bool fowardpress = (inputHandler.moveInput.x > 0 ? true : false);
        bool backwardpress = (inputHandler.moveInput.x < 0 ? true : false);
        bool rightpress = (inputHandler.moveInput.y > 0 ? true : false);
        bool leftpress = (inputHandler.moveInput.y < 0 ? true : false);
        int runpressed = (inputHandler.sprintValue > 0 ? 1 : 0);
        
        if(fowardpress && velocityZ < thresholdZ[runpressed])
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        else if(backwardpress && velocityZ > -thresholdZ[runpressed])
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        else
        {
            velocityZ = (velocityZ > 0 && !fowardpress ? velocityZ -= Time.deltaTime * deceleration : velocityZ);
            velocityZ = (velocityZ < 0 && !backwardpress ? velocityZ += Time.deltaTime * deceleration : velocityZ);
        }

        if(rightpress && velocityX < thresholdX[runpressed])
        {
            velocityX += Time.deltaTime * acceleration;
        }        
        else if(leftpress && velocityX > -thresholdX[runpressed])
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        else
        {
            velocityX = (velocityX > 0 && !rightpress ? velocityX -= Time.deltaTime * deceleration : velocityX);
            velocityX = (velocityX < 0 && !leftpress ? velocityX += Time.deltaTime * deceleration : velocityX);
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);

    }

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = InputHandler.instance;
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Velocity");

        thresholdX[0] = 0.5f;
        thresholdX[1] = 1.0f;

        thresholdZ[0] = 0.5f;
        thresholdZ[1] = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
       HandleBlendMove();
    }
}
