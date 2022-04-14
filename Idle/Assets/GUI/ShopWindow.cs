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

#nullable enable

namespace Idle.GUI {
    public class ShopWindow : MonoBehaviour {
        [SerializeField] 
        public ulong cost;
        
        [SerializeField]
        [FormerlySerializedAs("Button Passive Food")]
        public GameObject? buyButtonPassiveFood;
        
        [SerializeField]
        [FormerlySerializedAs("Text Passive Food")]
        public GameObject? buyCountNextPassiveFood;
        
        
        [SerializeField]
        [FormerlySerializedAs("Button Passive Wood")]
        public GameObject? buyButtonPassiveWood;
        [SerializeField]
        [FormerlySerializedAs("Text Passive Wood")]
        public GameObject? buyCountNextPassiveWood;
        
        
        [SerializeField]
        [FormerlySerializedAs("Button Passiv Stone")]
        public GameObject? buyButtonPassiveStone;
        [SerializeField]
        [FormerlySerializedAs("Text Passive Stone")]
        public GameObject? buyCountNextPassiveStone;
        
        
        [SerializeField]
        [FormerlySerializedAs("Button Passive Metal")]
        public GameObject? buyButtonPassiveMetal;
        
        [SerializeField]
        [FormerlySerializedAs("Text Passive Metal")]
        public GameObject? buyCountNextPassiveMetal;
        
        
        [SerializeField]
        [FormerlySerializedAs("Button Passive Faith")]
        public GameObject? buyButtonPassiveFaith;
        
        [SerializeField]
        [FormerlySerializedAs("Text Passive Faith")]
        public GameObject? buyCountNextPassiveFaith;

        private Map? _map;

        private Dictionary<PassiveEffect.EPassiveEffects, (TextMeshPro Text, Button Button)> _dictionaryFields = new();
        
        public void Start() {
            CheckNull();

            _map = GameObject.FindObjectOfType<Map>() ?? throw new NullReferenceException(nameof(Map) + " Not Found");

            AddAllInDictionaryFields();
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
        }
        
        private void AddAllInDictionaryFields() {
            void Put(GameObject objText, GameObject objButton, PassiveEffect.EPassiveEffects effectName) {
                var textMeshPro = objText.GetComponent<TextMeshPro>() 
                                  ?? throw new NullReferenceException("TextMeshPro Not Found For Effect: " + effectName);
                var button = objButton.GetComponent<Button>() 
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
            foreach (var (key, value) in _dictionaryFields) {
                value.Button.clicked += () => OnClick(map, key, value.Text, value.Button, cost);
            }
        }

        private static void OnClick(Map map, PassiveEffect.EPassiveEffects passiveEffectName, TextMeshPro text, Button button, ulong cost) {
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
            
            if (CanBuy() == false)
                return;
            
            Buy();
            map.AddCountOrSetPassiveEffects(passiveEffectName);
        }
    }
}



































