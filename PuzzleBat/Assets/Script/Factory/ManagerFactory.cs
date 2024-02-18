using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFactory : Singleton<ManagerFactory>
{
    private GameManager gameManager;
    private BoardManager boardManager;
    private InputManager inputManager;
    private SoundManager soundManager;

    void Awake()
    {
        CreatManagers();
    }

    public void CreatManagers()
    {
        boardManager =  BoardManager.Instance;
        gameManager = GameManager.Instance;
        inputManager = InputManager.Instance;
        soundManager = SoundManager.Instance;
    }
}
