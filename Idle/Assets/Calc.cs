using System;
using System.Collections.Generic;
using Idle.Effect;
using Idle.Building;

namespace Idle {
    public readonly struct Calc {
        private readonly float _deltaTime;
        private readonly IReadOnlyDictionary<Effect.Effect.EEffectName, Effect.Effect> _effects;
        private readonly IReadOnlyDictionary<Effect.PassiveEffect.EPassiveEffects, (Effect.PassiveEffect PassiveEffect, int Count)> _passiveEffects;
        private readonly IReadOnlyDictionary<IBuilding.EBuildingName, List<IBuilding>> _allTiles;
        
        public Calc(
            float deltaTime, 
            IReadOnlyDictionary<Effect.Effect.EEffectName, Effect.Effect> effects, 
            IReadOnlyDictionary<Effect.PassiveEffect.EPassiveEffects, (Effect.PassiveEffect PassiveEffect, int Count)> passiveEffects,
            IReadOnlyDictionary<IBuilding.EBuildingName, List<IBuilding>> allTiles) : this() {
            
            _deltaTime = deltaTime;
            _effects = effects;
            _passiveEffects = passiveEffects;
        }
        
        
        public IReadOnlyDictionary<IBuilding.EBuildingName, double> GetMultiplicators() {
            var res = PropMultiplikatorsWorker.FactoryDefault();
            
            foreach ((PassiveEffect.EPassiveEffects key, (PassiveEffect passiveEffect, int count)) in this._passiveEffects) {
                passiveEffect.CallEffect(res, count);
            }

            foreach ((Effect.Effect.EEffectName key, Effect.Effect value) in this._effects) {
                if (value.EffectIsActive() == false)
                    continue;
                value.CallEffect(res);
            }
            
            return res.Multiplikators;
        }
    }

    public readonly struct CalcFaith {
        public static ulong CalcFaithCost(Map map, uint factor) => 1000 * (ulong)factor;
    }
}





































