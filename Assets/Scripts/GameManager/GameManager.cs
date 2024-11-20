using System;
using UnityEngine;

// TODO: This needs to be expanded to include the oracle system
public class GameManager : MonoBehaviour
{
    // TODO: Consider separate classes for the following values
    public double xp;
    public uint coins;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // LoadSavedGameState();
    }

    void LoadSavedGameState()
    {
        // TODO: Implement this function
        throw new NotImplementedException();
    }
}
