using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterYMovement : MonoBehaviour, ICharacterMovement
{
    public bool isMovingUp = false;
    private Vector3 lowestStackedCubePosition = new Vector3(0f, 0.5f, 0f);
    private float trackLevel = 0f;
    private float cubeHeight = 2f;

    private Transform playerCharacter;
    private CharacterAnimation characterAnimation;
    public GameObject cubeManager;
    private CubeStacker cubeStacker;

    private void Awake()
    {
        playerCharacter = transform.GetChild(0);
        cubeStacker = cubeManager.transform.GetComponent<CubeStacker>();
        characterAnimation = transform.GetChild(0).GetComponent<CharacterAnimation>();
    }
    public void Movement(float addedHeight)
    {
        if (isMovingUp)
        {
            characterAnimation.isJumping = true;
            playerCharacter.position = new Vector3(playerCharacter.position.x, (playerCharacter.position.y + addedHeight * cubeHeight), playerCharacter.position.z);
        }
        else if (!isMovingUp)
        {
            transform.position = new Vector3(transform.position.x, trackLevel, transform.position.z);
            playerCharacter.localPosition = new Vector3(0f, cubeStacker.numberOfStackedCubes, 0f);

            transform.GetChild(1).transform.localPosition = lowestStackedCubePosition;
            for (int i = 0; i < cubeStacker.numberOfStackedCubes; i++)
            {
                transform.GetChild(1).GetChild(i).transform.localPosition = new Vector3(0f, addedHeight * cubeHeight * i, 0f);
            }
        }
        isMovingUp = false;
    }
}
