using System;
using TMPro;
using UnityEngine;

namespace Idle.GUI {
    public class FaithRitualText: MonoBehaviour {
        private TextMeshPro _textMeshPro;
        private Map _map;
        public void Start() {
            _textMeshPro = GetComponent<TextMeshPro>() ?? throw new NullReferenceException("TextMeshPro Not Found");
            _map = GameObject.FindObjectOfType<Map>() ?? throw new NullReferenceException("Map Not Found In Game");
        }

        public void Update() 
            => _textMeshPro.text = $"Cost: {CalcFaith.CalcFaithCost(_map, 1).ToString()}";
    }
}