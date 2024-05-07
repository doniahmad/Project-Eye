using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static event EventHandler<TaskEventArgs> OnTaskComplete;
    public class TaskEventArgs : EventArgs
    {
        public TaskSO taskSO;
    }
    public event EventHandler<SubtaskEventArgs> OnSubTaskComplete;
    // public event EventHandler<SubtaskEventArgs> OnSubTaskUndoComplete;
    public class SubtaskEventArgs : EventArgs
    {
        public SubTaskSO subTaskSO;
    }

    public PlayerController player;
    public TaskContainerUI taskContainerUI;
    public CraftingUI craftingUI;

    private List<TaskSO> listTaskSO;
    private int currentTaskIndex = 0;
    private TaskSO currentTaskSO;
    private List<SubTaskSO> listSubtaskSO;
    private List<SubTaskUI> listSubtasks;
    private PlayerInventory playerInventory;
    private List<ItemObjectSO> storedItemInInventory;
    private SubTaskSO currentSubtask;
    private int currentSubtaskIndex = 0;

    private void Start()
    {
        playerInventory = player.GetPlayerInventory();
        storedItemInInventory = playerInventory.GetStoredItem();

        OnSubTaskComplete += TaskManager_OnSubtaskComplete;
        OnTaskComplete += TaskManager_OnTaskComplete;

    }

    private void TaskManager_OnTaskComplete(object sender, TaskEventArgs e)
    {
        if (currentTaskIndex < listTaskSO.Count)
        {
            StartNewTask();
        }
        else
        {
            taskContainerUI.taskUI.SetComplete();
        }
    }

    private void TaskManager_OnSubtaskComplete(object sender, SubtaskEventArgs e)
    {
        HandleTask();
    }

    private void Update()
    {
        if (listSubtasks != null)
        {
            CheckSubtask();
        }
    }

    public void SetListTaskSO(List<TaskSO> listTaskSOs)
    {
        listTaskSO = listTaskSOs;
        StartNewTask();
    }

    public void StartNewTask()
    {
        currentTaskSO = listTaskSO[currentTaskIndex];

        LoadTask(currentTaskSO);

        currentTaskIndex++;

    }

    public void LoadTask(TaskSO task)
    {
        currentSubtaskIndex = 0;
        currentTaskSO = task;
        listSubtaskSO = task.listSubtasks;
        listSubtasks = new List<SubTaskUI>();
        taskContainerUI.ClearTask();
        taskContainerUI.SetTaskSO(currentTaskSO);
        LoadNewSubtask();
    }

    private void LoadNewSubtask()
    {
        if (currentSubtaskIndex < listSubtaskSO.Count)
        {
            currentSubtask = listSubtaskSO[currentSubtaskIndex];

            SubTaskUI newSubtask = taskContainerUI.taskUI.AddNewSubtask(currentSubtask);

            listSubtasks.Add(newSubtask);

            currentSubtaskIndex++;
        }
    }

    public void HandleTask()
    {
        Debug.Log(listSubtasks);
        if (listSubtasks.Count == listSubtaskSO.Count)
        {
            bool allSubtaskComplete = true;
            foreach (SubTaskUI subtask in listSubtasks)
            {
                if (!subtask.isComplete)
                {
                    allSubtaskComplete = false;
                    break;
                }
            }

            if (allSubtaskComplete)
            {


                OnTaskComplete?.Invoke(this, new TaskEventArgs { taskSO = currentTaskSO });
            }

        }
    }

    public void CheckSubtask()
    {
        //     Debug.Log("Cheking Subtask");
        for (int i = 0; i < listSubtasks.Count; i++)
        {
            SubTaskUI subTask = listSubtasks[i];
            SubTaskSO thisSubtaskSO = subTask.subTaskSO;

            switch (thisSubtaskSO.taskCategory)
            {
                case SubTaskSO.TaskCategory.Gathering:
                    if (HandleGatheringTask(thisSubtaskSO))
                    {
                        subTask.isComplete = true;

                        OnSubTaskComplete?.Invoke(this, new SubtaskEventArgs { subTaskSO = thisSubtaskSO });

                        // Cek apakah subtask saat ini adalah subtask terakhir
                        if (i == listSubtasks.Count - 1 && listSubtasks.Count < listSubtaskSO.Count)
                        {
                            // Jika iya, dan masih ada subtask yang belum dimuat, maka muat subtask baru
                            LoadNewSubtask();
                            break; // Keluar dari loop setelah memuat subtask baru
                        }
                    }
                    else
                    {
                        foreach (ItemObjectSO item in subTask.subTaskSO.itemsToGather)
                        {
                            if (!CheckItemInCraftingSlot(item))
                            {
                                subTask.isComplete = false;
                            }
                        }
                    }
                    break;
                case SubTaskSO.TaskCategory.Insert:
                    if (HandleInsertingTask(thisSubtaskSO))
                    {
                        subTask.isComplete = true;

                        OnSubTaskComplete?.Invoke(this, new SubtaskEventArgs { subTaskSO = thisSubtaskSO });

                        // Cek apakah subtask saat ini adalah subtask terakhir
                        if (i == listSubtasks.Count - 1 && listSubtasks.Count < listSubtaskSO.Count)
                        {
                            // Jika iya, dan masih ada subtask yang belum dimuat, maka muat subtask baru
                            LoadNewSubtask();
                            break; // Keluar dari loop setelah memuat subtask baru
                        }
                    }
                    else
                    {
                        subTask.isComplete = false;
                    }
                    break;
                case SubTaskSO.TaskCategory.Crafting:
                    if (HandleCraftingTask(thisSubtaskSO))
                    {
                        subTask.isComplete = true;

                        OnSubTaskComplete?.Invoke(this, new SubtaskEventArgs { subTaskSO = thisSubtaskSO });

                        // Cek apakah subtask saat ini adalah subtask terakhir
                        if (i == listSubtasks.Count - 1 && listSubtasks.Count < listSubtaskSO.Count)
                        {
                            // Jika iya, dan masih ada subtask yang belum dimuat, maka muat subtask baru
                            LoadNewSubtask();
                            break; // Keluar dari loop setelah memuat subtask baru
                        }
                    }
                    break;
                case SubTaskSO.TaskCategory.Solving:
                    if (HandleSolvingTask(thisSubtaskSO))
                    {
                        subTask.isComplete = true;

                        OnSubTaskComplete?.Invoke(this, new SubtaskEventArgs { subTaskSO = thisSubtaskSO });

                        // Cek apakah subtask saat ini adalah subtask terakhir
                        if (i == listSubtasks.Count - 1 && listSubtasks.Count < listSubtaskSO.Count)
                        {
                            // Jika iya, dan masih ada subtask yang belum dimuat, maka muat subtask baru
                            LoadNewSubtask();
                            break; // Keluar dari loop setelah memuat subtask baru
                        }
                    }
                    else
                    {
                        if (!thisSubtaskSO.isOneTimeEvent)
                        {
                            subTask.isComplete = false;
                        }
                    }
                    break;
            }
        }
    }

    private bool CheckItemInCraftingSlot(ItemObjectSO item)
    {
        bool itemInserted = false;
        foreach (ItemObjectSO targetItem in craftingUI.GetListItemInCraftingSlot())
        {
            if (item == targetItem)
            {
                itemInserted = true;
                return true;
            }
        }

        if (!itemInserted)
        {
            return false;
        }

        return false;
    }

    /* 
    This function is for handling Gathering Quest
    The system is getting all correct item in inventory
    */
    private bool HandleGatheringTask(SubTaskSO task)
    {
        List<ItemObjectSO> tempListItemInInventorySlot = new List<ItemObjectSO>(storedItemInInventory);

        bool allItemFound = true;
        foreach (ItemObjectSO item in task.itemsToGather)
        {
            bool itemFound = false;
            foreach (ItemObjectSO targetItem in tempListItemInInventorySlot)
            {
                if (item == targetItem)
                {
                    itemFound = true;
                    tempListItemInInventorySlot.Remove(targetItem);
                    break;
                }
            }
            if (!itemFound)
            {
                allItemFound = false;
                break;
            }
        }

        if (!allItemFound)
        {
            // Debug.Log("Item Not Founded");
            return false;
        }

        if (allItemFound)
        {
            currentTaskSO.isComplete = true;
            // Debug.Log("Item Founded");
            return true;
        }

        return false;
    }

    /* 
    This function is for handling Inserting Quest
    The system is getting all correct item in Crafting Device slot
    */
    private bool HandleInsertingTask(SubTaskSO task)
    {
        List<ItemObjectSO> tempListItemInCraftingSlot = new List<ItemObjectSO>(craftingUI.GetListItemInCraftingSlot());

        bool allItemInserted = true;
        foreach (ItemObjectSO item in task.itemToInsert)
        {
            bool itemInserted = false;
            foreach (ItemObjectSO targetItem in tempListItemInCraftingSlot)
            {
                if (item == targetItem)
                {
                    itemInserted = true;
                    tempListItemInCraftingSlot.Remove(targetItem);
                    break;
                }
            }

            if (!itemInserted)
            {
                allItemInserted = false;
                break;
            }
        }

        if (!allItemInserted)
        {
            Debug.Log("Item Not Inserted");
            return false;
        }

        if (allItemInserted)
        {
            currentTaskSO.isComplete = true;

            Debug.Log("Item Inserted");
            return true;
        }

        return false;
    }

    private bool HandleCraftingTask(SubTaskSO task)
    {
        if (craftingUI.craftingStatus == CraftingUI.CraftingStatus.Crafted)
        {
            if (task.itemToCraft == craftingUI.craftedItem.itemObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private bool HandleSolvingTask(SubTaskSO task)
    {
        switch (task.problem)
        {
            case SubTaskSO.ProblemCategory.ReadBook:
                if (player.GetPlayerStatus() == PlayerStatus.Status.AfterReadBook)
                {
                    return true;
                }
                break;
            case SubTaskSO.ProblemCategory.WashHand:
                if (player.GetPlayerStatus() == PlayerStatus.Status.Clean || player.GetPlayerStatus() == PlayerStatus.Status.CleanGloved)
                {
                    return true;
                }
                break;
            case SubTaskSO.ProblemCategory.GloveHand:
                if (player.GetPlayerStatus() == PlayerStatus.Status.CleanGloved)
                {
                    return true;
                }
                break;
            case SubTaskSO.ProblemCategory.WriteWhiteboard:
                if (player.GetPlayerStatus() == PlayerStatus.Status.AfterWritingRecipe)
                {
                    return true;
                }
                break;
            case SubTaskSO.ProblemCategory.OpenLockedCupboard:
                break;
            case SubTaskSO.ProblemCategory.FixFumeHoodDoor:
                break;
            case SubTaskSO.ProblemCategory.OpenWerehouse:
                break;
        }
        return false;
    }

    private List<ItemObjectSO> GetStoredItemInInventory()
    {
        return playerInventory.GetStoredItem();
    }

}
