using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterZMovement : MonoBehaviour, ICharacterMovement
{
    public void Movement(float forward_speed)
    {
        transform.Translate(Vector3.forward * forward_speed * Time.deltaTime);
    }
}
