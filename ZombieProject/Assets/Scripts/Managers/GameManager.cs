using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentRound;

    private void Start()
    {
        currentRound = 1;
    }

    private void RoundCompleted()
    {
        currentRound++;
    }

}
