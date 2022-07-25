using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lore.Questing
{
    public class Quest : ScriptableObject
    {
        public List<QuestGoal> Goals;
        public bool Completed;
        public QuestCompletedEvent QuestCompleted;

        [Serializable]
        public struct QuestInfo
        {
            public string Name;
            public Sprite Icon;
            public string Description;
        }
        [Header("Info")]
        public QuestInfo questInfo;

        [Header("Reward")]
        public QuestReward questReward;

        public void Initialize()
        {
            Completed = false;
            QuestCompleted = new QuestCompletedEvent();

            foreach (var goal in Goals)
            {
                goal.Initialize();
                goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
            }
        }

        public void CheckGoals()
        {
            Completed = Goals.All(g => g.Completed);
            if (Completed)
            {
                QuestCompleted?.Invoke(this);
                QuestCompleted?.RemoveAllListeners();
            }
        }
    }
}

