using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSettings : MonoBehaviour
{
    public AudioSource audioSource;

    public GameObject movementController;

    private void Awake()
    {
        if (PlayerPrefs.GetFloat("volume") == 0)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.volume = PlayerPrefs.GetFloat("volume");
        }

        if (PlayerPrefs.GetInt("gyro") == 1)
        {
            movementController.GetComponent<gyroMovement>().enabled = true;
            movementController.GetComponent<MoveByTouch>().enabled = false;
        }
        else
        {
            movementController.GetComponent<gyroMovement>().enabled = false;
            movementController.GetComponent<MoveByTouch>().enabled = true;
        }
    }
}
