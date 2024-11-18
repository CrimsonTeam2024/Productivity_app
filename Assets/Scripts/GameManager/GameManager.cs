using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public double xp;
    public uint coins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // LoadSavedGameState();
    }

    void LoadSavedGameState()
    {
        throw new NotImplementedException();
    }
}
