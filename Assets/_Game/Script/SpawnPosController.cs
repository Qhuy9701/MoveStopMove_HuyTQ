using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosController : MonoBehaviour
{
    public bool isHaveCharacter;
    public bool isHavePlayer()
    {
        return isHaveCharacter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Constants.TAG_PLAYER || other.gameObject.tag == Constants.TAG_BOT)
        {
            isHaveCharacter = true;
            Debug.Log("Have Character");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Constants.TAG_PLAYER || other.gameObject.tag == Constants.TAG_BOT)
        {
            isHaveCharacter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Constants.TAG_PLAYER || other.gameObject.tag == Constants.TAG_BOT)
        {
            isHaveCharacter = false;
        }
    }
}
