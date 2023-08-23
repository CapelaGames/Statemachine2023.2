using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public enum Roster
    {
        Player,
        AI
    }

    public int currentTurn = 0;

    public Roster[] turnOrder;

    private void Awake()
    {
        currentTurn = 0;
        SetupTurnOrder();
    }

    public void SetupTurnOrder()
    {
        turnOrder = new Roster[2];
        turnOrder[0] = Roster.Player;
        turnOrder[1] = Roster.AI;
    }
    public Roster RandomTurn()
    {
        currentTurn = Random.Range(0, turnOrder.Length);
        return turnOrder[currentTurn];
    }
    public Roster NextTurn()
    {
        currentTurn++;
        currentTurn %= turnOrder.Length;
        return turnOrder[currentTurn];
    }

    public bool isMyTurn(Roster me)
    {
        return turnOrder[currentTurn] == me;
    }
}
