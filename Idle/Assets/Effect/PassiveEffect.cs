#nullable enable
using System;
using System.Collections.Generic;

namespace Effect {
    public class PassiveEffect {
        private Action<PropMultiplikatorsWorker> _func;
        private static readonly Dictionary<EPassiveEffects, Action<PropMultiplikatorsWorker>> CallFunc = new () {
            // TODO Write Hendrik CallFunc
            { EPassiveEffects.PassivFood, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Gold, out var gold);
                var goldNeeded = 100000;

                if (gold >= goldNeeded) {
                    // Set new Multiplicator for all producers
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var PmultiFood);
                    PmultiFood *= 2;

                    // Set new Gold Value
                    ETypeHint.Gold = fromMap.Cargo[ETypeHint.Gold] / goldNeeded;
                    gold = gold / goldNeeded;
                    
                    // Return new Multiplicator
                    return PmultiFood;
                } else {
                    throw new Exception("Not enough Gold")
                }
            } },
            { EPassiveEffects.PassivWood, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Gold, out var gold);
                var goldNeeded = 100000;

                if (gold >= goldNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var PmultiWood);
                    PmultiWood *= 2;

                    ETypeHint.Gold = fromMap.Cargo[ETypeHint.Gold] / goldNeeded;
                    gold = gold / goldNeeded;
                    
                    return PmultiWood;
                } else {
                    throw new Exception("Not enough Gold")
                }
            } },
            { EPassiveEffects.PassivStone, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Gold, out var gold);
                var goldNeeded = 100000;

                if (gold >= goldNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var PmultiStone);
                    PmultiStone *= 2;

                    ETypeHint.Gold = fromMap.Cargo[ETypeHint.Gold] / goldNeeded;
                    gold = gold / goldNeeded;
                    
                    return PmultiStone;
                } else {
                    throw new Exception("Not enough Gold")
                }
            } },
            { EPassiveEffects.PassivMetal, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Gold, out var gold);
                var goldNeeded = 100000;

                if (gold >= goldNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var PmultiMetal);
                    PmultiMetal *= 2;

                    ETypeHint.Gold = fromMap.Cargo[ETypeHint.Gold] / goldNeeded;
                    gold = gold / goldNeeded;
                    
                    return PmultiMetal;
                } else {
                    throw new Exception("Not enough Gold")
                }
            } },
            { EPassiveEffects.PassivFaith, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Gold, out var gold);
                var goldNeeded = 100000;

                if (gold >= goldNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var PmultiFaith);
                    PmultiFaith *= 2;

                    ETypeHint.Gold = fromMap.Cargo[ETypeHint.Gold] / goldNeeded;
                    gold = gold / goldNeeded;
                    
                    return PmultiFaith;
                } else {
                    throw new Exception("Not enough Gold")
                }
            } },
        };

        public void CallEffect(PropMultiplikatorsWorker prop) => _func(prop);

        private PassiveEffect() => _func = worker => { };
        
        public PassiveEffect(EPassiveEffects passiveEffect) {
            if (CallFunc.TryGetValue(passiveEffect, out _func)) 
                return;
            throw new Exception($"{passiveEffect} Not Found In Dic CallFunc");
        }
        
        
        public enum EPassiveEffects {
            PassivFood,
            PassivWood,
            PassivStone,
            PassivMetal,
            PassivFaith,
        }
    }
}