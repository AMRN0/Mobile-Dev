using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    public Image[] liveImage;
    public int livesRemaining = 3;

    [ContextMenu("Lose life")]
    public void LoseLive()
    {
        livesRemaining--;


        //liveImage[livesRemaining].gameObject.SetActive(false);
        liveImage[livesRemaining].enabled = false;

        if (livesRemaining <= 0)
        {
            print("you lost");
        }
    }
}
