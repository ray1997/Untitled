using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Ofc, it's no loger true, this script only now provide location of player whereabout
/// But rename it might break stuff on scene; so it will have to stay as is.
/// </summary>
public class BetterCameraSwitcher : MonoBehaviour
{
    public static BetterCameraSwitcher Instance = null;
    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        PlayerSwitcher.OnPlayerChanged += UpdateLocationTo;
    }

    private void OnDisable()
    {
        PlayerSwitcher.OnPlayerChanged -= UpdateLocationTo;
    }

    public Player CurrentlyActivePlayer;
    private void UpdateLocationTo(GameObject player, Player current)
    {
        Debug.Log($"Update active player on {name} to {current}");
        CurrentlyActivePlayer = current;
    }

    public Collider StarterPlayer1Collider;
    public Collider StarterPlayer2Collider;

    Collider _p1, _p2;
    public Collider CurrentlyStayed1
    {
        get
        {
            if (_p1 is null)
                return StarterPlayer1Collider;
            return _p1;
        }
        set
        {
            if (!Equals(_p1, value))
            {
                OnPlayerChangingRoom?.Invoke(Player.First, value);
                _p1 = value;
            }
        }
    }
    public Collider CurrentlyStayed2
    {
        get
        {
            if (_p2 is null)
                return StarterPlayer2Collider;
            return _p2;
        }
        set
        {
            if (!Equals(_p2, value))
            {
                OnPlayerChangingRoom?.Invoke(Player.Second, value);
                _p2 = value;
            }
        }
    }
    public delegate void PlayerStayedColliderUpdate(Player current, Collider location);
    public static event PlayerStayedColliderUpdate OnPlayerChangingRoom;
    /// <summary>
    /// Since now all cameras work is handle by cinemachine, this script only here for telling what room current player is in
    /// </summary>
    /// <param name="triggered"></param>
    public void RequestSwitchTo(Collider triggered)
    {
        if (CurrentlyActivePlayer == Player.First)
            CurrentlyStayed1 = triggered;
        else
            CurrentlyStayed2 = triggered;
    }
}
