using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStacker : MonoBehaviour, ICubeMechanic
{
    public BoxCollider cubeHolderCollider;
    public Transform currentCube;

    public GameObject playerCharacter;
    private CharacterYMovement characterYMovement;
    private CharacterController characterController;
    public int numberOfStackedCubes = 1;
    private float cubeOffset = 0.5f;

    private void Awake()
    {
        characterYMovement = playerCharacter.GetComponent<CharacterYMovement>();
        characterController = playerCharacter.GetComponent<CharacterController>();
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            characterController.pickedUpCube = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Destroy(collision.gameObject);
            AddCubesToStack();
            characterController.pickedUpCube = false;
        }
    }

    public void AddCubesToStack()
    {
        numberOfStackedCubes++;

        GameObject newCube = Instantiate(currentCube.gameObject, new Vector3(currentCube.transform.position.x,
        currentCube.transform.position.y + cubeOffset, currentCube.transform.position.z), Quaternion.identity);

        newCube.transform.position += Vector3.up * cubeOffset;

        UpdatePlayerCharacterPosition();

        newCube.transform.parent = transform;
        currentCube = newCube.transform;

        UpdateColliderSize();
    }

    public void UpdatePlayerCharacterPosition()
    {
        characterYMovement.isMovingUp = true;
        characterYMovement.Movement(cubeOffset);
    }
    public void UpdateColliderSize()
    {
        float newSize = numberOfStackedCubes;
        float newCenter = cubeOffset * (numberOfStackedCubes - 1);

        cubeHolderCollider.size = new Vector3(cubeHolderCollider.size.x, newSize, cubeHolderCollider.size.z);
        cubeHolderCollider.center = new Vector3(cubeHolderCollider.center.x, newCenter, cubeHolderCollider.center.z);
    }
}
