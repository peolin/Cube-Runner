using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Touch touch;
    public bool playersTouch = false;
    public bool gameStarted = false;

    public float sideway_speed = 0.07f;
    public float forward_speed = 10f;
    public float cubeOffset = 0.5f;

    private CharacterXMovement characterXMovement;
    private CharacterYMovement characterYMovement;
    private CharacterZMovement characterZMovement;

    private RagdollOnOff ragdollMode;

    public GameObject cubeManager;
    private CubeStacker cubeStacker;
    public bool isPassingWall;
    public bool pickedUpCube = false;
    private CubeRemover cubeRemover;
    private void Awake()
    {
        characterXMovement = transform.GetComponent<CharacterXMovement>();
        characterYMovement = transform.GetComponent<CharacterYMovement>();
        characterZMovement = transform.GetComponent<CharacterZMovement>();

        ragdollMode = transform.GetChild(0).GetComponent<RagdollOnOff>();

        cubeStacker = cubeManager.GetComponent<CubeStacker>();
        cubeRemover = cubeManager.GetComponent<CubeRemover>();
    }

    public void CheckPlayerTouch()
    {
        if (Input.touchCount > 0)
        {
            gameStarted = true;
            CharacterMovement();
        }
        else if ((Input.touchCount == 0) & (gameStarted))
        {
            CharacterMovement();
        }
    }
    public void CharacterMovement()
    {
        if ((!cubeRemover.noCubesLeft) & (cubeStacker.numberOfStackedCubes != 0))
        {
            characterZMovement.Movement(forward_speed);

            if (Input.touchCount != 0)
            {
                touch = Input.GetTouch(0);
                if ((touch.phase == TouchPhase.Moved) & (!isPassingWall))
                {
                    playersTouch = true;
                    characterXMovement.Movement(sideway_speed * touch.deltaPosition.x);
                }
            }
            else playersTouch = false;
        }
        else if ((cubeRemover.noCubesLeft) & (cubeStacker.numberOfStackedCubes == 0))
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        //transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        ragdollMode.isRagdoll = true;
    }
}
