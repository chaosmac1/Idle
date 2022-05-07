#nullable enable
using System;
using System.Collections.Generic;
using Hint;
using TMPro;
using UnityEngine;

namespace Idle {
    public class UI: MonoBehaviour {
        public GameObject? textFoodObj;
        public GameObject? textWoodObj;
        public GameObject? textStoneObj;
        public GameObject? textMetalObj;
        public GameObject? textGoldObj;
        public GameObject? textFaithObj;

        private TextMeshProUGUI? _textFood;
        private TextMeshProUGUI? _textWood;
        private TextMeshProUGUI? _textStone;
        private TextMeshProUGUI? _textMetal;
        private TextMeshProUGUI? _textGold;
        private TextMeshProUGUI? _textFaith;
        private Map? _map;

        public void Start() {
            _map = GameObject.FindObjectOfType<Map>();
            CheckNullsAndThrow();
            
            this._textFood = textFoodObj!.GetComponent<TextMeshProUGUI>();
            this._textWood = textWoodObj!.GetComponent<TextMeshProUGUI>();
            this._textStone = textStoneObj!.GetComponent<TextMeshProUGUI>();
            this._textMetal = textMetalObj!.GetComponent<TextMeshProUGUI>();
            this._textGold = textGoldObj!.GetComponent<TextMeshProUGUI>();
            this._textFaith = textFaithObj!.GetComponent<TextMeshProUGUI>();
        }

        public void Update() {
            Dictionary<ETypeHint, ulong> cargo = _map!.Cargo!;
            if (cargo is null)
                throw new NullReferenceException(nameof(cargo));

            ulong GetValueOrDefault(ETypeHint hint) => cargo.ContainsKey(hint) == false ? 0 : cargo[hint];

            _textFood!.text = "Food: " + GetValueOrDefault(ETypeHint.Food);
            _textWood!.text = "Wood: " + GetValueOrDefault(ETypeHint.Wood);
            _textStone!.text = "Stone: " + GetValueOrDefault(ETypeHint.Stone);
            _textMetal!.text = "Metal: " + GetValueOrDefault(ETypeHint.Metal);
            _textGold!.text = "Gold: " + GetValueOrDefault(ETypeHint.Gold);
            _textFaith!.text = "Faith: " + GetValueOrDefault(ETypeHint.Faith);
        }
        
        private void CheckNullsAndThrow() {
            if (this.textFoodObj is null)
                throw new NullReferenceException(nameof(this.textFoodObj));
            if (this.textWoodObj is null)
                throw new NullReferenceException(nameof(this.textWoodObj));
            if (this.textStoneObj is null)
                throw new NullReferenceException(nameof(this.textStoneObj));
            if (this.textMetalObj is null)
                throw new NullReferenceException(nameof(this.textMetalObj));
            if (this.textGoldObj is null)
                throw new NullReferenceException(nameof(this.textGoldObj));
            if (this.textFaithObj is null)
                throw new NullReferenceException(nameof(this.textFaithObj));
        }
    }
}