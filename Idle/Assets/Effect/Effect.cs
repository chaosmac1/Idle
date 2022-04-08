using System;
using System.Collections.Generic;
using Hint;
using Idle.Building;

namespace Effect
{
    public class Effect
    {
        private readonly DateTime _endEffect;
        private readonly Action<PropMultiplikatorsWorker> _func;

        private static readonly Dictionary<Effect.EEffectName, Action<PropMultiplikatorsWorker>> EffectFuncs = new() {
            // TODO Write Hendrik EffectFuncs 
            { EEffectName.RitualFood, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Faith, out var faith);
                var faithNeeded = 10000;

                if (faith >= faithNeeded) {
                    // Set new Multiplicator for all producers
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var multiFood);
                    multiFood *= 2;

                    // Set new Faith Value
                    ETypeHint.Faith = fromMap.Cargo[ETypeHint.Faith] / faithNeeded;
                    faith = faith / faithNeeded;

                    // Return new Multiplicator
                    return multiFood;
                } else {
                    throw new Exception("Not enough Faith")
                }
            } },
            { EEffectName.RitualWood, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Faith, out var faith);
                var faithNeeded = 10000;

                if (faith >= faithNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var multiWood);
                    multiWood *= 2;

                    ETypeHint.Faith = fromMap.Cargo[ETypeHint.Faith] / faithNeeded;
                    faith = faith / faithNeeded;
                    
                    return multiWood;
                } else {
                    throw new Exception("Not enough Faith")
                }
            } },
            { EEffectName.RitualStone, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Faith, out var faith);
                var faithNeeded = 10000;

                if (faith >= faithNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var multiStone);
                    multiStone *= 2;

                    ETypeHint.Faith = fromMap.Cargo[ETypeHint.Faith] / faithNeeded;
                    faith = faith / faithNeeded;
                    
                    return multiStone;
                } else {
                    throw new Exception("Not enough Faith")
                }
            } },
            { EEffectName.RitualMetal, prop => {
                var fromMap = Object.FindObjectOfType<Map>();
                fromMap.Cargo.TryGetValue(ETypeHint.Faith, out var faith);
                var faithNeeded = 10000;

                if (faith >= faithNeeded) {
                    fromMap.Cargo.TryGetValue(prop.Multiplikators.Values,out var multiMetal);
                    multiMetal *= 2;

                    ETypeHint.Faith = fromMap.Cargo[ETypeHint.Faith] / faithNeeded;
                    faith = faith / faithNeeded;
                    
                    return multiMetal;
                } else {
                    throw new Exception("Not enough Faith")
                }
            } },
        };



        public bool EffectIsActive() => DateTime.UtcNow < _endEffect;

        public void CallEffect(PropMultiplikatorsWorker prop)
        {
            if (EffectIsActive() == false) return;
            _func(prop);
        }


        public Effect(DateTime endEffect, EEffectName eEffectName)
        {
            _endEffect = endEffect;
            if (EffectFuncs.TryGetValue(eEffectName, out _func)) return;



            throw new Exception($"{eEffectName} Not Fount In {nameof(EffectFuncs)}");
        }

        public enum EEffectName
        {
            RitualFood,
            RitualWood,
            RitualStone,
            RitualMetal,
        }
    }
}