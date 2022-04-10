using System;
using System.Collections.Generic;
using Effect;
using Idle.Building;

namespace Idle {
    public readonly struct Calc {
        private readonly float _deltaTime;
        private readonly IReadOnlyDictionary<Effect.Effect.EEffectName, Effect.Effect> _effects;
        private readonly IReadOnlyDictionary<Effect.PassiveEffect.EPassiveEffects, Effect.PassiveEffect> _passiveEffects;
        private readonly IReadOnlyDictionary<IBuilding.EBuildingName, List<IBuilding>> _allTiles;
        
        public Calc(
            float deltaTime, 
            IReadOnlyDictionary<Effect.Effect.EEffectName, Effect.Effect> effects, 
            IReadOnlyDictionary<PassiveEffect.EPassiveEffects, PassiveEffect> passiveEffects,
            IReadOnlyDictionary<IBuilding.EBuildingName, List<IBuilding>> allTiles) : this() {
            
            _deltaTime = deltaTime;
            _effects = effects;
            _passiveEffects = passiveEffects;
        }

        /// <summary> Get Multiplicator For Eatch BuildingType As Dictionary </summary>
        public IReadOnlyDictionary<IBuilding.EBuildingName, double> GetMultiplicators() {
            var res = new Dictionary<IBuilding.EBuildingName, double>(16);
            
            throw new NotImplementedException($"TODO Write Langer {nameof(Calc)}");
            
            return res;
        }
    }

    public readonly struct CalcFaith {
        public static ulong CalcFaithCost(Map map, uint factor) {
            // TODO Write Calc
            throw new NotImplementedException(nameof(CalcFaithCost));
        }
    }
}





































