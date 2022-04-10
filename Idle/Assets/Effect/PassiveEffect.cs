#nullable enable
using System;
using System.Collections.Generic;
using Idle.Building;

namespace Effect {
    public class PassiveEffect {
        private Action<PropMultiplikatorsWorker> _func;
        private static readonly Dictionary<EPassiveEffects, Action<PropMultiplikatorsWorker>> CallFunc = new () {
            // TODO Write Hendrik CallFunc
            { EPassiveEffects.PassivFood, prop => {
                double multiFarm = 0;
                double multiDocks = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Farm))
                    multiFarm = prop.Multiplikators[IBuilding.EBuildingName.Farm];
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Docks))
                    multiDocks = prop.Multiplikators[IBuilding.EBuildingName.Docks];

                prop.Multiplikators[IBuilding.EBuildingName.Farm] = (multiFarm == 0 ? 1 : multiFarm) * 2;
                prop.Multiplikators[IBuilding.EBuildingName.Docks] = (multiDocks == 0 ? 1 : multiDocks) * 2;
            } },
            {EPassiveEffects.PassivWood, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Forest))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Forest];


                prop.Multiplikators[IBuilding.EBuildingName.Forest] = (multi == 0 ? 1 : multi) * 2;
            } },
            {EPassiveEffects.PassivStone, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Mine))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Mine];


                prop.Multiplikators[IBuilding.EBuildingName.Mine] = (multi == 0 ? 1 : multi) * 2;
            } },
            {EPassiveEffects.PassivMetal, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Smith))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Smith];

                prop.Multiplikators[IBuilding.EBuildingName.Smith] = (multi == 0 ? 1 : multi) * 2;
            } },
            { EPassiveEffects.PassivFaith, prop => {
                double multi = 0;
                
                if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Shrine))
                    multi = prop.Multiplikators[IBuilding.EBuildingName.Shrine];

                prop.Multiplikators[IBuilding.EBuildingName.Shrine] = (multi == 0 ? 1 : multi) * 2;
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