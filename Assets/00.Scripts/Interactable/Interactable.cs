using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float InteractRange = 3f;

    public Item item;

    public virtual Item Interact()
    {
        if (item == null)
        {
            Debug.LogError($"{item} is null");
        }

        SquishSquashAnimation();

        return item;
    }

    public virtual void Use()
    {
        SquishSquashAnimation();
    }

    private void Awake()
    {
        // DEMO CODE
        GameObject.FindObjectOfType<PlayerInventory>().interactables.Add(this);
    }

    protected void SquishSquashAnimation()
    {
        gameObject.transform.LeanScale(new Vector3(1.2f, 0.8f), 0.2f).setEaseInBack().setOnComplete(() =>
        {
            gameObject.transform.LeanScale(new Vector3(0.8f, 1.2f), 0.2f).setEaseInBack().setOnComplete(() =>
            {
                gameObject.transform.LeanScale(Vector3.one, 0.15f).setEaseInBack();
            });
        });
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, InteractRange);
    }
}