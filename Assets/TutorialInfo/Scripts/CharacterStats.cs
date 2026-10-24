using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public string CharacterName;
    public TeamType team = TeamType.Enemy;
    public int kills = 0;
    public int deaths = 0;
    public GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddPlayerName();
    }

    // Update is called once per frame
    public void AddKill()
    {
        kills++;
        if (team == TeamType.Player)
        {
            gameManager.UpdateFragScore(1);
        }
    }
    public void AddDeath()
    {
        deaths++;
        if (team == TeamType.Player)
        {
            gameManager.UpdateDeathScore(1);
        }
    }
    public void AddPlayerName()
    {
        if (team == TeamType.Player)
        {
            gameManager.PlayerList(CharacterName);
        }
    }
    public enum TeamType
    {
        Player,
        Ally,
        Enemy
    }
}
