using System;
using UnityEngine;

namespace MonsterQuest
{
    [Serializable]
    public class AbilityScore
    {
        public int score;
        public int modifier => (score - 10) / 2;

        public static implicit operator int(AbilityScore abilityScore) => abilityScore.score;
    }
}
