using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public Health enemyHealth;

    public void UseAbility()
    {
        enemyHealth.Damage(30);
    }
}
