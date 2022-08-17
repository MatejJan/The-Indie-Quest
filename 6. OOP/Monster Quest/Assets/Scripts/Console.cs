using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MonsterQuest
{
    public class Console : MonoBehaviour
    {
        private static Console _instance;
        
        private TextMeshProUGUI _textMeshProUGui;

        public static void Write(string text)
        {
            _instance.WriteInternal(text);
        }

        public static void WriteLine(string text)
        {
            _instance.WriteLineInternal(text);
        }
        
        public void WriteInternal(string text)
        {
            _textMeshProUGui.text += text;
        }
        
        public void WriteLineInternal(string text)
        {
            _textMeshProUGui.text += $"{text}\n";
        }
        
        private void Awake()
        {
            _textMeshProUGui = GetComponent<TextMeshProUGUI>();
            _instance = this;
        }
    }
}
