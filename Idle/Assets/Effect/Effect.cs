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
            { EEffectName.RitualFood, prop => { } },
            { EEffectName.RitualWood, prop => { } },
            { EEffectName.RitualStone, prop => { } },
            { EEffectName.RitualMetal, prop => { } },
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