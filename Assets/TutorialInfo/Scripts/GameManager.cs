using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameStartText;
    public float startTime = 5f;
    public bool StopCountDown;

    public GameObject tabPanel;
    public int FragScore = 0;
    public int DeathScore = 0;
    public TextMeshProUGUI fragText;
    public TextMeshProUGUI deathText;
    public TextMeshProUGUI playerNameText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StopCountDown = false;
        startTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        ScorePanel();
        StartCountdown();
        
    }
    void StartCountdown()
    {
        if (startTime > 0 && !StopCountDown)
        {
            startTime -= Time.deltaTime;
            gameStartText.text = "start in: " + startTime.ToString("0");
        }

        if (startTime <= 0 && !StopCountDown)
        {
            gameStartText.text = "Game Started!";
            Invoke("ClearCountdown", 1f);
        }
    }
     void ClearCountdown()
     {
    gameStartText.text = "";
    StopCountDown = true;
    startTime = 5;
     }

    void ScorePanel()
    {
        if (Keyboard.current[Key.Tab].wasPressedThisFrame)
        {
            tabPanel.SetActive(!tabPanel.activeSelf);
        }
    }
    public void UpdateFragScore(int frag)
    {
        FragScore += frag;
        fragText.text = FragScore.ToString();
    }
    public void UpdateDeathScore(int death)
    {
        DeathScore += death;
        deathText.text = DeathScore.ToString();
    }
    public void PlayerList(string playerName)
    {
        playerNameText.text = playerName;
    }
}
