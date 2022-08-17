using System;
using System.Collections.Generic;

namespace MonsterQuest
{
    [Serializable]
    public class AbilityScores
    {
        public AbilityScore strength = new();
        public AbilityScore dexterity = new();
        public AbilityScore constitution = new();
        public AbilityScore intelligence = new();
        public AbilityScore wisdom = new();
        public AbilityScore charisma = new();

        // Allow access with the enum.
        public AbilityScore this[Ability ability]
        {
            get
            {
                return ability switch
                {
                    Ability.Strength => strength,
                    Ability.Dexterity => dexterity,
                    Ability.Constitution => constitution,
                    Ability.Intelligence => intelligence,
                    Ability.Wisdom => wisdom,
                    Ability.Charisma => charisma,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}
