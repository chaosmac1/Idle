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
            {EEffectName.RitualFood, prop => {
                double multiFarm = 0;
                double multiDocks = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Farm))
                    multiFarm = prop.Multiplikators[IBuilding.EBuildingName.Farm];
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Docks))
                    multiDocks = prop.Multiplikators[IBuilding.EBuildingName.Docks];

                prop.Multiplikators[IBuilding.EBuildingName.Farm] = (multiFarm == 0 ? 1 : multiFarm) * 2;
                prop.Multiplikators[IBuilding.EBuildingName.Docks] = (multiDocks == 0 ? 1 : multiDocks) * 2;
            } },
            {EEffectName.RitualWood, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Forest))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Forest];


                prop.Multiplikators[IBuilding.EBuildingName.Forest] = (multi == 0 ? 1 : multi) * 2;
            } },
            {EEffectName.RitualStone, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Mine))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Mine];

                prop.Multiplikators[IBuilding.EBuildingName.Mine] = (multi == 0 ? 1 : multi) * 2;
            } },
            {EEffectName.RitualMetal, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Smith))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Smith];

                prop.Multiplikators[IBuilding.EBuildingName.Smith] = (multi == 0 ? 1 : multi) * 2;
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