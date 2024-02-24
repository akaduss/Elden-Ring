using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    //singleton
    public static WorldSaveGameManager instance;

    [SerializeField] private int worldBuildIndex = 1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //awake only one instance

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    //start dontdestroyload her sahnede bu objeyi kullanacaðýz

    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldBuildIndex);
        yield return null;
    }

    public int GetWorldBuildIndex()
    {
        return worldBuildIndex;
    }
}
