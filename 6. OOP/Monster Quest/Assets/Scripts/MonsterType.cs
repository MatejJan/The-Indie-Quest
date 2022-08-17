using UnityEngine;

namespace MonsterQuest
{
    [CreateAssetMenu(fileName = "New Monster", menuName = "Monster")]
    public class MonsterType : ScriptableObject
    {
        public new string name;
        public string description;
        public string alignment;
        public int armorClass;
        public string hitPointsRoll;
        public AbilityScores abilityScores = new();
        public float challengeRating;
        public string attackDamageRoll;
    }
}
