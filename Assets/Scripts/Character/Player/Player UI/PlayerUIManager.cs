using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [Header("NETWORK JOIN")] //DEBUG
    [SerializeField] bool startGameAsClient;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (startGameAsClient)
        {
            startGameAsClient = false;
            NetworkManager.Singleton.Shutdown();
            NetworkManager.Singleton.StartClient();
        }
    }
}