using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject uiCanvas;
    public AudioManager audioManager;
    public Button audioOnOffButton;
    public Sprite audioTurnedOn;
    public Sprite audioTurnedOff;
    private bool audioOn = true;
    public TextMeshProUGUI startMessage;
    public TextMeshProUGUI gameOverMessage;

    public TextMeshProUGUI popMessage;
    private GameObject currentPopupMessage;
    public float popupSpeed = 15f;
    private float minXPosition = -2f;
    private float maxXPosition = 2f;
    private float minPopupXPosition = -400f;
    private float maxPopupXPosition = 0f;
    private float minPopupYPosition = -500f;

    public GameObject gameOverPanel;
    public GameObject tryAgainButton;
    public bool tryAgain;

    private void Awake()
    {
        tryAgain = false;
    }

    public void TurnOnOffAudio()
    {
        if (!audioOn)
        {
            audioManager.UnmuteAudio();
            audioOnOffButton.image.sprite = audioTurnedOn;
            Debug.Log("Turn on");
            audioOn = true;
        }
        else
        {
            audioManager.MuteAudio();
            audioOnOffButton.image.sprite = audioTurnedOff;
            Debug.Log("Turn off");
            audioOn = false;
        }
    }
    public void StartingScreen()
    {
        startMessage.text = "Tap to Play";
        startMessage.gameObject.SetActive(true);
        gameOverPanel.SetActive(false);
        popMessage.gameObject.SetActive(true);
        audioOnOffButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        startMessage.gameObject.SetActive(false);
    }

    public void CreatePopup(Transform player)
    {
        float newXPosition = Mathf.Lerp(minPopupXPosition, maxPopupXPosition, Mathf.InverseLerp(minXPosition, maxXPosition, player.position.x));

        GameObject newPopupMessage = Instantiate(popMessage.gameObject, uiCanvas.transform);
        newPopupMessage.transform.localPosition = new Vector3(newXPosition, minPopupYPosition, 0f);

        currentPopupMessage = newPopupMessage;
    }

    public void MovePopup()
    {
        float newYPosition = currentPopupMessage.transform.localPosition.y + popupSpeed;
        currentPopupMessage.transform.localPosition = new Vector3(currentPopupMessage.transform.localPosition.x, newYPosition, 0f);
    }

    public void DestroyPopups()
    {
        if (uiCanvas.transform.childCount > 4)
        {
            for (int i = 4; i < uiCanvas.transform.childCount; i++)
            {
                GameObject popupMessage = uiCanvas.transform.GetChild(i).gameObject;

                if (popupMessage.transform.localPosition.y >= minPopupYPosition)
                {
                    Destroy(popupMessage);
                }
            }
        }
    }

    public void GameOverScreen()
    {
        audioOnOffButton.gameObject.SetActive(false);
        startMessage.text = "Game Over";
        gameOverPanel.SetActive(true);
        gameOverMessage.gameObject.SetActive(true);
        tryAgainButton.SetActive(true);
    }

    public void TryAgain()
    {
        tryAgain = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
