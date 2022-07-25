using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lore.Questing
{
    public class SpeakGoal : QuestGoal
    {
        public string CharacterName;

        public override string GetDescription()
        {
            return $"Speak to {CharacterName}"; 
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Evaluate()
        {
            base.Evaluate();
        }

        public void OnCharacterSpeak(NPC character)
        {
            if (character.name == CharacterName)
            {
                Evaluate();
            }
        }
    }
}

