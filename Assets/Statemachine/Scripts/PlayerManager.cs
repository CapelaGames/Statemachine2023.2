using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    Button _abilityPrefab;

    [SerializeField]
    RectTransform _abilityPanel;

    [SerializeField]
    TurnManager _turnManager;
    private bool _previousTurn = false;

    void Awake()
    {
        Ability[] abilities = GetComponents<Ability>();

        foreach (Ability current in abilities)
        {
            Button abilityButton = Instantiate<Button>(_abilityPrefab, _abilityPanel);

            abilityButton.onClick.AddListener( current.UseAbility);
            abilityButton.interactable = false;

            //if we wanted parameters, we can do it this way:
            //abilityButton.onClick.AddListener(delegate { current.UseAbility(20); });
            //abilityButton.onClick.AddListener(()=>  current.UseAbility(20) );
        }
    }

    private void Update()
    {
        ManageTurn();
    }

    void ManageTurn()
    {
        if (_turnManager == null) return;

        if (_previousTurn != _turnManager.isMyTurn(TurnManager.Roster.Player))
        {
            _previousTurn = !_previousTurn;
            ToggleAbilities(_previousTurn);
            Debug.Log("toggl " + _previousTurn);
        }
    }

    void ToggleAbilities()
    {
        Button[] buttons = _abilityPanel.GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = !button.interactable;
        }
    }

    void ToggleAbilities(bool setInteractable)
    {
        Button[] buttons = _abilityPanel.GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            button.interactable = setInteractable;
        }
    }

}
