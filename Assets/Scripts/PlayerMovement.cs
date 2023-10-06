using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement controller.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 2;

    private void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed);
    }
}
