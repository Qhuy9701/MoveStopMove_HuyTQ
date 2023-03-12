using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosTrigger : MonoBehaviour
{
    public bool isHaveCharacter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == Constants.TAG_PLAYER || other.tag == Constants.TAG_BOT)
        {
            isHaveCharacter = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == Constants.TAG_PLAYER || other.tag == Constants.TAG_BOT)
        {
            isHaveCharacter = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == Constants.TAG_PLAYER || other.tag == Constants.TAG_BOT)
        {
            isHaveCharacter = false;
        }
    }

    public bool GetIsHaveCharacter()
    {
        return isHaveCharacter;
    }
}
