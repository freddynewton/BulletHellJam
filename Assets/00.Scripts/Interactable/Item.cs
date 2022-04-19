using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Interactable/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private Sprite icon;

    public string Name => itemName;

    public Sprite Icon => icon;
}
