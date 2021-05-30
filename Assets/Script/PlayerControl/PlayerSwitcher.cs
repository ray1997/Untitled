using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    private void Start()
    {
        PlayerControl.OnRequestDisableControl += DisablePlayerSwitcher;
        PlayerControl.OnRequestRestoreControl += RestorePlayerSwitcher;
    }

    float cachedTime;
    private void RestorePlayerSwitcher()
    {
        NextSwitchAllow = cachedTime;
    }

    private void DisablePlayerSwitcher(DisableType disable)
    {
        //Disallow character switch
        cachedTime = NextSwitchAllow;
        NextSwitchAllow = float.MaxValue;
    }

    public GameObject Player1;
    public GameObject Player2;

    public Transform LastPlayer1Location;
    public Transform LastPlayer2Location;

    public delegate void PlayerChanged(GameObject player);
    public static event PlayerChanged OnPlayerChanged;

    [Range(1f, 60f)]
    public float Cooldown = 3;
    public float NextSwitchAllow;

    public enum Player
    {
        First,
        Second
    }
    public Player CurrentPlayer;
    private void OnEnable()
    {
        PlayerInput.OnPlayerRequestSwitch += SwitchPlayer;
    }

    private void SwitchPlayer()
    {
        if (Time.realtimeSinceStartup < NextSwitchAllow)
            return;
        NextSwitchAllow = Time.realtimeSinceStartup + Cooldown;
        //Sync location
        if (CurrentPlayer == Player.First)
        {
            LastPlayer1Location.position = Camera.main.transform.position;
            LastPlayer1Location.rotation = Camera.main.transform.rotation;
        }
        else
        {
            LastPlayer2Location.position = Camera.main.transform.position;
            LastPlayer2Location.rotation = Camera.main.transform.rotation;
        }
        //Switch
        CurrentPlayer++;
        if (CurrentPlayer > Player.Second)
            CurrentPlayer = Player.First;
        Player1.SetActive(CurrentPlayer == Player.First);
        Player1.GetComponent<PlayerControl>().playerSpeed = CurrentPlayer == Player.First ? 4 : 0;
        Player2.SetActive(CurrentPlayer == Player.Second);
        Player2.GetComponent<PlayerControl>().playerSpeed = CurrentPlayer == Player.Second ? 4 : 0;
        //Send current active player
        OnPlayerChanged?.Invoke(Player1.activeSelf ? Player1 : Player2);
        //Move camera to previous player location
        Camera.main.transform.position =
            CurrentPlayer == Player.First ? LastPlayer1Location.position : LastPlayer2Location.position;
        Camera.main.transform.rotation =
            CurrentPlayer == Player.First ? LastPlayer1Location.rotation : LastPlayer2Location.rotation;
    }
}
