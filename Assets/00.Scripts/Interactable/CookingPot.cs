using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingPot : Interactable
{
    public event Action OnCookingPotUse;

    public override void Use()
    {
        base.Use();

        OnCookingPotUse?.Invoke();
    }
}
