using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public AudioManager audioManager;
    public GameObject gameUI_manager;
    private UIManager uiManager;

    public GameObject player;
    private CharacterController characterController;

    public GameObject cubeManager;
    private CubeStacker cubeStacker;
    private CubeRemover cubeRemover;

    public GameObject trackGeneration;
    private TrackGenerator trackGenerator;
    private bool isPopupActive;

    private void Awake()
    {
        Instance = this;
        isPopupActive = false;
        uiManager = gameUI_manager.GetComponent<UIManager>();
        characterController = player.GetComponent<CharacterController>();
        trackGenerator = trackGeneration.GetComponent<TrackGenerator>();
        cubeStacker = cubeManager.GetComponent<CubeStacker>();
        cubeRemover = cubeManager.GetComponent<CubeRemover>();
    }
    void Start()
    {
        uiManager.StartingScreen();
    }
    void Update()
    {
        if (characterController.gameStarted)
        {
            uiManager.StartGame();
        }

        trackGenerator.AddNewTracks(player.transform);

        if (!cubeRemover.noCubesLeft)
        {
            characterController.CheckPlayerTouch();
            if (characterController.pickedUpCube && !isPopupActive)
            {
                uiManager.CreatePopup(player.transform);
                isPopupActive = true;
            }
            else if (characterController.pickedUpCube && isPopupActive)
            {
                uiManager.MovePopup();
            }
            else if (!characterController.pickedUpCube)
            {
                uiManager.DestroyPopups();
                isPopupActive = false;
            }
            cubeRemover.DestroyRemovedCubes();
        }
        else if (cubeRemover.noCubesLeft)
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        uiManager.GameOverScreen();
        characterController.GameOver();
        audioManager.MuteAudio();
    }
}