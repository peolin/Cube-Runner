using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterXMovement : MonoBehaviour, ICharacterMovement
{
    private float borderX_a = -2f;
    private float borderX_b = 2f;

    //sideway_speed = character_speed * touch.deltaPosition.x)
    public void Movement(float sideway_speed)
    {
        transform.position = new Vector3(
            Mathf.Clamp((transform.position.x + sideway_speed), borderX_a, borderX_b),
            transform.position.y, transform.position.z);
    }
}
