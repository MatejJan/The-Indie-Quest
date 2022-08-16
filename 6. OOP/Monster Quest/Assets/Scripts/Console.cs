using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MonsterQuest
{
    public class Console : MonoBehaviour
    {
        private static Console instance;
        
        private TextMeshProUGUI textMeshProUGui;

        public static void Write(string text)
        {
            instance.WriteInternal(text);
        }

        public static void WriteLine(string text)
        {
            instance.WriteLineInternal(text);
        }
        
        public void WriteInternal(string text)
        {
            textMeshProUGui.text += text;
        }
        
        public void WriteLineInternal(string text)
        {
            textMeshProUGui.text += $"{text}\n";
        }
        
        private void Awake()
        {
            textMeshProUGui = GetComponent<TextMeshProUGUI>();
            instance = this;
        }
    }
}
