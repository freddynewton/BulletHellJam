using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item currentItem { get; private set; }

    public List<Interactable> interactables { get; set; } = new List<Interactable>();

    public CookingPot cookingPot { get; set; }

    public TaskManager taskManager { get; set; }

    private PlayerManager playerManager { get; set; }

    SoundStation audioStation;

    private void Start()
    {
        audioStation = SoundStation.Instance;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cookingPot != null)
        {
            if (Vector2.Distance(cookingPot.transform.position, transform.position) < cookingPot.InteractRange)
            {
                if (taskManager == null)
                {
                    taskManager = FindObjectOfType<TaskManager>();
                }

                taskManager.CurrentItem.enabled = false;
                cookingPot.Use();
            }
        }
    }

    public void ChangeCurrentItem(Item interactableItem)
    {
        currentItem = interactableItem;

        if (taskManager == null)
        {
            taskManager = FindObjectOfType<TaskManager>();
        }

        taskManager.CurrentItem.sprite = currentItem.Icon;
        taskManager.CurrentItem.enabled = true;
    }

    private Interactable GetClosestInteractable()
    {
        Interactable temp = null;

        foreach (var interactable in interactables.Where(inter => Vector2.Distance(inter.transform.position, transform.position) < inter.InteractRange))
        {
            if (temp == null)
            {
                temp = interactable;
            }
            else
            {
                if (Vector2.Distance(transform.position, interactable.transform.position) < Vector2.Distance(transform.position, temp.transform.position))
                {
                    temp = interactable;
                }
            }
        }

        return temp;
    }

    private void ClearCurrentItem()
    {
        if (currentItem == taskManager.TaskList[0])
        {
            currentItem = null;
            Destroy(taskManager.Grid.transform.GetChild(0).gameObject);
            taskManager.TaskList.RemoveAt(0);
        }

        if (taskManager.TaskList.Count == 0)
        {
            playerManager.GetHealth(1);
            taskManager.UpdateTaskList();
            audioStation.Play(SoundStation.Asset.IngredientSFX, "DishComplete");
        }
        else
        {
            audioStation.Play(SoundStation.Asset.IngredientSFX, "IntoPot");
        }
    }

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        cookingPot = FindObjectOfType<CookingPot>();
        taskManager = FindObjectOfType<TaskManager>();

        if (cookingPot != null)
        {
            cookingPot.OnCookingPotUse += ClearCurrentItem;
        }
    }
}
