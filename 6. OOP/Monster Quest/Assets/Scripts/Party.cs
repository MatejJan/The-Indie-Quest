using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MonsterQuest
{
    public class Party : List<Character>
    {
        public override string ToString()
        {
            return StringHelpers.JoinWithAnd(this.Select(character => character.name));
        }
    }
}
