using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    Button _abilityPrefab;

    [SerializeField]
    RectTransform _abilityPanel;

    void Awake()
    {
        Ability[] abilities = GetComponents<Ability>();

        foreach (Ability current in abilities)
        {
            Button abilityButton = Instantiate<Button>(_abilityPrefab, _abilityPanel);

            abilityButton.onClick.AddListener( current.UseAbility);

            //if we wanted parameters, we can do it this way:
            //abilityButton.onClick.AddListener(delegate { current.UseAbility(20); });
            //abilityButton.onClick.AddListener(()=>  current.UseAbility(20) );
        }
    }
}
