using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Lore.Questing
{
    public abstract class QuestGoal : MonoBehaviour
    {
        protected string Description;
        public bool Completed;
        [HideInInspector] public UnityEvent GoalCompleted;

        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        public virtual void Evaluate()
        {

        }

        public void Complete()
        {
            Completed = true;
            GoalCompleted?.Invoke();
            GoalCompleted?.RemoveAllListeners();
        }

        public void Skip()
        {
            // 
            Complete();
        }
    }

}
