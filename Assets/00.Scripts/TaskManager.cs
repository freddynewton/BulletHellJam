using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [Header("References")]
    public GameObject Grid;
    public GameObject ItemPrefab;
    public Image CurrentItem;

    [Space]
    [Header("Settings")]
    [SerializeField]
    private int MaxPhaseAmount = 10;

    private Item[] itemPool { get; set; }

    private List<Item> taskList { get; set; } = new List<Item>();

    public List<Item> TaskList => taskList;

    private int phaseCount = 1;
    private int currentPhaseAmount = 0;

    public event Action OnTaskListUpdated;

    private void Start()
    {
        itemPool = Resources.LoadAll<Item>("Items/");
        UpdateTaskList();
    }

    public void UpdateTaskList()
    {
        taskList.Clear();

        // TODO: Add Highscore points

        foreach (Transform child in Grid.transform)
        {
            Destroy(child.gameObject);
        }

        if (currentPhaseAmount == phaseCount)
        {
            currentPhaseAmount = 0;

            if (phaseCount < 10)
            {
                phaseCount++;
            }
        }

        for (int i = 0; i < phaseCount; i++)
        {
            taskList.Add(itemPool[UnityEngine.Random.Range(0, itemPool.Length)]);

            var image = Instantiate(ItemPrefab, Grid.transform);
            image.GetComponent<Image>().sprite = taskList[i].Icon;

            Debug.Log($"Item {taskList[i]} added to the TaskList");
        }

        OnTaskListUpdated?.Invoke();

        currentPhaseAmount++;
    }
}
