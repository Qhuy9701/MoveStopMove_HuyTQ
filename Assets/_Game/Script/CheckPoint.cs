using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isHaveCharacter = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isHaveCharacter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            isHaveCharacter = false;
        }
    }

    public bool GetIsHaveCharacter()
    {
        return isHaveCharacter;
    }
}
