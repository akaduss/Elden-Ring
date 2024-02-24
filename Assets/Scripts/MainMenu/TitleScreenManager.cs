using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TitleScreenManager : MonoBehaviour
{
    public void StartGameAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartNewGame()
    {
        StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
    }
}