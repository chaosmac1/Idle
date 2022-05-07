

#nullable enable

using System;
using System.Collections.Generic;
using Hint;
using Idle.Effect;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace Idle.GUI {
    public class ShopWindow : MonoBehaviour {
        [SerializeField] 
        public ulong cost;
        
        [SerializeField] [FormerlySerializedAs("Button Passive Food")]
        public GameObject? buyButtonPassiveFood;
        
        [SerializeField] [FormerlySerializedAs("Text Passive Food")]
        public GameObject? buyCountNextPassiveFood;
        
        
        [SerializeField] [FormerlySerializedAs("Button Passive Wood")]
        public GameObject? buyButtonPassiveWood;
        
        [SerializeField] [FormerlySerializedAs("Text Passive Wood")]
        public GameObject? buyCountNextPassiveWood;
        
        
        [SerializeField] [FormerlySerializedAs("Button Passiv Stone")]
        public GameObject? buyButtonPassiveStone;
        
        [SerializeField] [FormerlySerializedAs("Text Passive Stone")]
        public GameObject? buyCountNextPassiveStone;
        
        
        [SerializeField] [FormerlySerializedAs("Button Passive Metal")]
        public GameObject? buyButtonPassiveMetal;
        
        [SerializeField] [FormerlySerializedAs("Text Passive Metal")]
        public GameObject? buyCountNextPassiveMetal;
        
        
        [SerializeField] [FormerlySerializedAs("Button Passive Faith")]
        public GameObject? buyButtonPassiveFaith;
        
        [SerializeField] [FormerlySerializedAs("Text Passive Faith")]
        public GameObject? buyCountNextPassiveFaith;

        [SerializeField] [FormerlySerializedAs("Button Close")]
        public GameObject? buttonCloseObj;
        
        private Map? _map;

        private Dictionary<PassiveEffect.EPassiveEffects, (TextMeshProUGUI Text, UnityEngine.UI.Button Button)> _dictionaryFields = new();
        
        public void Start() {
            CheckNull();

            _map = GameObject.FindObjectOfType<Map>() ?? throw new NullReferenceException(nameof(Map) + " Not Found");

            var closeButton = this.buttonCloseObj!.GetComponent<UnityEngine.UI.Button>() ?? throw new NullReferenceException("buttonCloseObj Button Not Found");

            closeButton.onClick.AddListener(this.onClickClose);
            
            AddAllInDictionaryFields();
            
            SetOnClick(this._map);
        }

        public void Update() {
            foreach (var (key, (passiveEffect, count)) in _map!.PassiveEffects) {
                switch (key) {
                    case PassiveEffect.EPassiveEffects.PassivFood:
                        this._dictionaryFields[PassiveEffect.EPassiveEffects.PassivFood].Text.text = "X" + count;
                        break;
                    case PassiveEffect.EPassiveEffects.PassivWood:
                        this._dictionaryFields[PassiveEffect.EPassiveEffects.PassivWood].Text.text = "X" + count;
                        break;
                    case PassiveEffect.EPassiveEffects.PassivStone:
                        this._dictionaryFields[PassiveEffect.EPassiveEffects.PassivStone].Text.text = "X" + count;
                        break;
                    case PassiveEffect.EPassiveEffects.PassivMetal:
                        this._dictionaryFields[PassiveEffect.EPassiveEffects.PassivMetal].Text.text = "X" + count;
                        break;
                    case PassiveEffect.EPassiveEffects.PassivFaith:
                        this._dictionaryFields[PassiveEffect.EPassiveEffects.PassivFaith].Text.text = "X" + count;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void CheckNull() {
            if (buyButtonPassiveFood is null)
                throw new NullReferenceException(nameof(buyButtonPassiveFood));
            if (buyCountNextPassiveFood is null)
                throw new NullReferenceException(nameof(buyCountNextPassiveFood));
            if (buyButtonPassiveWood is null)
                throw new NullReferenceException(nameof(buyButtonPassiveWood)); 
            if (buyCountNextPassiveWood is null)
                throw new NullReferenceException(nameof(buyCountNextPassiveWood)); 
            if (buyButtonPassiveStone is null)
                throw new NullReferenceException(nameof(buyButtonPassiveStone)); 
            if (buyCountNextPassiveStone is null)
                throw new NullReferenceException(nameof(buyCountNextPassiveStone)); 
            if (buyButtonPassiveMetal is null)
                throw new NullReferenceException(nameof(buyButtonPassiveMetal)); 
            if (buyCountNextPassiveMetal is null)
                throw new NullReferenceException(nameof(buyCountNextPassiveMetal)); 
            if (buyButtonPassiveFaith is null)
                throw new NullReferenceException(nameof(buyButtonPassiveFaith)); 
            if (buyCountNextPassiveFaith is null)
                throw new NullReferenceException(nameof(buyCountNextPassiveFaith));
            if (buttonCloseObj is null)
                throw new NullReferenceException(nameof(buttonCloseObj));
        }
        
        private void AddAllInDictionaryFields() {
            void Put(GameObject objText, GameObject objButton, PassiveEffect.EPassiveEffects effectName) {
                var textMeshPro = objText.GetComponent<TextMeshProUGUI>() 
                                  ?? throw new NullReferenceException("TextMeshPro Not Found For Effect: " + effectName);
                var button = objButton.GetComponent<UnityEngine.UI.Button>() 
                             ?? throw new NullReferenceException("Button Not Found For Effect: " + effectName);

                _dictionaryFields[effectName] = (textMeshPro, button);
            }
            
            Put(buyCountNextPassiveFood!, buyButtonPassiveFood!, PassiveEffect.EPassiveEffects.PassivFood);
            
            Put(buyCountNextPassiveWood!, buyButtonPassiveWood!, PassiveEffect.EPassiveEffects.PassivWood);
                
            Put(buyCountNextPassiveStone!, buyButtonPassiveStone!, PassiveEffect.EPassiveEffects.PassivStone);
                    
            Put(buyCountNextPassiveMetal!, buyButtonPassiveMetal!, PassiveEffect.EPassiveEffects.PassivMetal);

            Put(buyCountNextPassiveFaith!, buyButtonPassiveFaith!, PassiveEffect.EPassiveEffects.PassivFaith);
        }

        private void SetOnClick(Map map) {
            foreach ((PassiveEffect.EPassiveEffects key, (TextMeshProUGUI text, Button button)) in _dictionaryFields) {
                button.onClick.AddListener(() => OnClick(map, key, text, button, cost));
            }
        }

        private static void OnClick(Map map, PassiveEffect.EPassiveEffects passiveEffectName, TextMeshProUGUI text, UnityEngine.UI.Button button, ulong cost) {
            var passiveEffectsDic = map.PassiveEffects!;

            bool CanBuy() {
                if (map.Cargo!.ContainsKey(ETypeHint.Gold) == false)
                    return false;
                return map.Cargo![ETypeHint.Gold] >= cost;
            }

            void Buy() {
                if (map.Cargo!.ContainsKey(ETypeHint.Gold) == false)
                    throw new Exception("Can Not Buy");

                map.Cargo![ETypeHint.Gold] -= cost;
            }

            if (CanBuy() == false) {
                Debug.Log("Can Not Buy: " + passiveEffectName);
                return;
            }
            Buy();
            Debug.Log("Buy: " + passiveEffectName);
            
            map.AddCountOrSetPassiveEffects(passiveEffectName);
        }

        private void onClickClose() {
            this.gameObject.SetActive(false);
        }
    }
}



































