using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }

    [Header("Prefabs")]
    public GameObject[] prefabs;

    [Header("Player")]
    public GameObject player;
    public GameObject playerCamera;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var prefab in prefabs)
        {
            Instantiate(prefab);
        }
    }
}
