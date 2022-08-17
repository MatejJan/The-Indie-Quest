using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MonsterQuest
{
    public static class StringHelpers
    {
        public static string JoinWithAnd(IEnumerable items, bool useSerialComma = true)
        {
            var itemsList = new List<string>();

            foreach (object item in items)
            {
                itemsList.Add(item.ToString());
            }

            int count = itemsList.Count;

            if (count == 0) return "";
            if (count == 1) return itemsList[0];
            if (count == 2) return $"{itemsList[0]} and {itemsList[1]}";

            var itemsCopy = new List<string>(itemsList);

            if (useSerialComma)
            {
                itemsCopy[count - 1] = $"and {itemsList[count - 1]}";
            }
            else
            {
                itemsCopy[count - 2] = $"{itemsList[count - 2]} and {itemsList[count - 1]}";
                itemsCopy.RemoveAt(count - 1);
            }

            return String.Join(", ", itemsCopy);
        }
    }
}
