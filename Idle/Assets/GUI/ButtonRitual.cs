using System;
using Hint;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace Idle.GUI {
    public class ButtonRitual : MonoBehaviour {
        public Effect.Effect.EEffectName effect;
        public uint effectTimeInSek = 60;
        public Sprite? canClickTexture;
        public Sprite? canNotClickTexture;
        private GameObject? _gameObject;
        private Button? _button;
        private Image? _image;
        public EStatus status = EStatus.NoFaith;
        
        public (DateTime? EndTime, EStatus Status) StatusAndEndTime() {
            Map? map;
            map = GetMap()??throw new NullReferenceException(nameof(map));
            
            var (endTime, exist) = EffectExist(map);
            
            return (exist == false?null: endTime, status);
        }
            
        
        public enum EStatus {
            CanClick,
            NoFaith,
            RitualActive,
        }

        private void CheckNullReference() {
            if (_gameObject is null) throw new NullReferenceException(nameof(_gameObject));
            if (_button is null) throw new NullReferenceException(nameof(_button));
            if (_image is null) throw new NullReferenceException(nameof(_image));
            if (canClickTexture is null) throw new NullReferenceException(nameof(canClickTexture));
            if (canNotClickTexture is null) throw new NullReferenceException(nameof(canNotClickTexture));
        }
        
        public void Start() {
            _gameObject = this.gameObject;
            _button = this.GetComponent<Button>();
            _image = this.GetComponent<Image>();

            CheckNullReference();
            
            _button.onClick.AddListener(OnClick);
            SetStatus(EStatus.NoFaith);
        }

        public void LateUpdate() {
            var map = GetMap();
            
            switch (status) {
                case EStatus.CanClick:
                    if (CanBuy(map))
                        return;
                    SetStatus(EStatus.NoFaith);
                    break;
                case EStatus.NoFaith:
                    if (CanBuy(map))
                        SetStatus(EStatus.CanClick);
                    break;
                case EStatus.RitualActive:
                    
                    if (EffectExist(map).Exist == false) {
                        SetStatus(EStatus.NoFaith);
                        LateUpdate();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Map? GetMap() => GameObject.FindObjectOfType<Map>();

        private void OnClick() {
            CheckNullReference();
            
            if (status != EStatus.CanClick) return;

            Map? map = GetMap();
            if (map is null)
                throw new NotImplementedException(nameof(map));

            if (CanBuy(map) == false) {
                SetStatus(EStatus.NoFaith);
                return;
            }
            
            Buy(map);
            SetStatus(EStatus.RitualActive);
        }

        private (DateTime? EndTime, bool Exist) EffectExist(Map map) {
            var effects = map.Effects;
            if (effects.ContainsKey(this.effect) == false)
                return (null, false);

            Effect.Effect effect = effects[this.effect]!;
            
            if (effect.EffectIsActive() == false) 
                return (null, false);
            return (effect.endTime, true);
        }

        private void SetStatus(EStatus newStatus) {
            status = newStatus;
            _image!.sprite = newStatus switch {
                EStatus.CanClick => this.canClickTexture,
                EStatus.NoFaith => this.canNotClickTexture,
                EStatus.RitualActive => this.canNotClickTexture,
                _ => throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null)
            };
        }
        
        private bool CanBuy(Map map) {
            var cost = Idle.CalcFaith.CalcFaithCost(map, 1);
            ulong cargoFaith = 0;
            if (map.Cargo!.ContainsKey(ETypeHint.Faith))
                cargoFaith = map.Cargo![ETypeHint.Faith];

            return cargoFaith >= cost;
        }

        private void Buy(Map map) {
            var cost = Idle.CalcFaith.CalcFaithCost(map, 1);
            
            ulong cargoFaith = 0;
            if (map.Cargo!.ContainsKey(ETypeHint.Faith))
                cargoFaith = map.Cargo![ETypeHint.Faith];

            map.Cargo![ETypeHint.Faith] = cargoFaith - cost;

            map.SetEffects(this.effect, DateTime.UtcNow.Add(TimeSpan.FromSeconds(this.effectTimeInSek)));
        }
        
        public void Update() => CheckNullReference();
    }
}


















