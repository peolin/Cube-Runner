using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRemover : MonoBehaviour, ICubeMechanic
{
    public GameObject playerCharacter;
    private CharacterYMovement characterYMovement;
    private CharacterController characterController;

    private CubeStacker cubeStacker;

    public BoxCollider cubeHolderCollider;
    public Transform leftoverCubeHolder;

    private float wallCubeXPosition;
    private int numberOfCollidersInWallColumn = 0;
    private float defaultWallCubeXPosition = -100f;
    private float cubeOffset = 0.5f;

    public bool noCubesLeft = false;
    private int maxNumOfLeftoverCubes = 7;

    private void Awake()
    {
        cubeStacker = transform.GetComponent<CubeStacker>();
        wallCubeXPosition = defaultWallCubeXPosition;
        characterYMovement = playerCharacter.GetComponent<CharacterYMovement>();
        characterController = playerCharacter.GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            float newWallCubeXPosition = collision.gameObject.transform.position.x;

            if ((wallCubeXPosition == -100f) || (wallCubeXPosition == newWallCubeXPosition))
            {
                numberOfCollidersInWallColumn++;

                if (cubeStacker.numberOfStackedCubes != 0)
                {
                    RemoveCubesFromStack(collision.gameObject.transform);
                }

                characterController.isPassingWall = true;
                if (cubeStacker.numberOfStackedCubes == 0)
                {
                    noCubesLeft = true;
                }
                wallCubeXPosition = newWallCubeXPosition;
            }
        }
    }
    public void RemoveCubesFromStack(Transform columnPosition)
    {
        Transform cube = transform.GetChild(0);
        cube.SetParent(leftoverCubeHolder);

        cube.position = new Vector3(columnPosition.position.x, columnPosition.position.y, columnPosition.position.z - cubeOffset * 2);
        cubeStacker.numberOfStackedCubes -= 1;

        UpdateColliderSize();
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if ((characterController.isPassingWall == true) & (cubeStacker.numberOfStackedCubes != 0))
            {
                UpdatePlayerCharacterPosition();

                numberOfCollidersInWallColumn = 0;
                characterController.isPassingWall = false;
            }
        }
        wallCubeXPosition = defaultWallCubeXPosition;
    }

    public void UpdatePlayerCharacterPosition()
    {
        characterYMovement.isMovingUp = false;
        characterYMovement.Movement(cubeOffset);
    }

    public void UpdateColliderSize()
    {
        float newSize = cubeStacker.numberOfStackedCubes;
        float newCenter = cubeOffset * (cubeStacker.numberOfStackedCubes - 1);

        cubeHolderCollider.size = new Vector3(cubeHolderCollider.size.x, newSize, cubeHolderCollider.size.z);
        cubeHolderCollider.center = new Vector3(cubeHolderCollider.center.x, newCenter, cubeHolderCollider.center.z);
    }
    public void DestroyRemovedCubes()
    {
        int numberOfLeftoverCubes = leftoverCubeHolder.transform.childCount - maxNumOfLeftoverCubes;
        if (numberOfLeftoverCubes > 0)
        {
            for (int i = 0; i <= numberOfLeftoverCubes; i++)
            {
                Destroy(leftoverCubeHolder.transform.GetChild(i).gameObject);
            }
        }
    }
}