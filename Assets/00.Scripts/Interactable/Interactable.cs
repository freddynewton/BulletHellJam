using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float InteractRange = 3f;

    public Item item;
    public CircleCollider2D collider2D;
    private PlayerInventory playerInventory;

    SoundStation audioStation;

    private void Start()
    {
        audioStation = SoundStation.Instance;
    }

    public virtual Item Interact()
    {
        if (item == null)
        {
            Debug.LogError($"{item} is null");
        }

        audioStation.Play(SoundStation.Asset.IngredientSFX ,"Pickup");
        SquishSquashAnimation(true);

        return item;
    }

    public virtual void Use()
    {
        SquishSquashAnimation(false);
    }

    public virtual void HoverAnimation()
    {
        //LeanTween.moveY(gameObject, 0.5f, 1f).setLoopPingPong().setEaseInBack();
    }

    private void Awake()
    {

        HoverAnimation();

        // DEMO CODE
        if (collider2D == null)
        {
            collider2D = GetComponent<CircleCollider2D>();
        }

        if (item != null)
        {
            collider2D.radius = InteractRange;
            collider2D.isTrigger = true;
        }

        playerInventory = GameObject.FindObjectOfType<PlayerInventory>();
        playerInventory.interactables.Add(this);
    }

    protected void SquishSquashAnimation(bool destroyObject)
    {
        gameObject.transform.LeanScale(new Vector3(1.2f, 0.8f), 0.2f).setEaseInBack().setOnComplete(() =>
        {
            gameObject.transform.LeanScale(new Vector3(0.8f, 1.2f), 0.2f).setEaseInBack().setOnComplete(() =>
            {
                if (destroyObject)
                {
                    gameObject.transform.LeanScale(Vector3.zero, 0.15f).setEaseInBack().setOnComplete(() =>
                    {
                        Destroy(gameObject);
                    });
                }
                else
                {
                    gameObject.transform.LeanScale(Vector3.one, 0.15f).setEaseInBack();
                }
            });
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
   {
        if (item == null || !collision.CompareTag(playerInventory.tag) || playerInventory.currentItem == item)
        {
            return;
        }

        playerInventory.ChangeCurrentItem(Interact());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, InteractRange);
    }
}