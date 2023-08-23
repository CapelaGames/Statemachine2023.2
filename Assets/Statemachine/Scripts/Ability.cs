using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Health enemyHealth;

    [SerializeField]
    TurnManager _turnManager;

    public void UseAbility()
    {
        enemyHealth.Damage(30);

        _turnManager.NextTurn();
    }
}
