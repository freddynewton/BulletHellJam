using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Item currentItem { get; set; }

    public List<Interactable> interactables { get; set; } = new List<Interactable>();

    public CookingPot cookingPot { get; set; }

    public TaskManager taskManager { get; set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector2.Distance(cookingPot.transform.position, transform.position) < cookingPot.InteractRange)
            {
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
            Debug.Log($"{currentItem.Name} used at the cooking pot.");
            currentItem = null;
            Destroy(taskManager.Grid.transform.GetChild(0).gameObject);
            taskManager.TaskList.RemoveAt(0);
        }

        if (taskManager.TaskList.Count == 0)
        {
            taskManager.UpdateTaskList();
        }
    }

    private void Awake()
    {
        cookingPot = FindObjectOfType<CookingPot>();
        taskManager = FindObjectOfType<TaskManager>();
        cookingPot.OnCookingPotUse += ClearCurrentItem;
    }
}
