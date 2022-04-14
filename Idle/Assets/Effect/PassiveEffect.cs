#nullable enable
using System;
using System.Collections.Generic;
using Idle.Building;
using UnityEngine;

namespace Idle.Effect {
    public class PassiveEffect {
        private Action<PropMultiplikatorsWorker> _func;
        private static readonly Dictionary<EPassiveEffects, Action<PropMultiplikatorsWorker>> CallFunc = new () {
            { EPassiveEffects.PassivFood, prop => {
                var mulityplay = SetMulityplayByCount(2, EPassiveEffects.PassivFood);
                double multiFarm = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Farm) * mulityplay;
                double multiDocks = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Docks) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Farm] = multiFarm;
                prop.Multiplikators[IBuilding.EBuildingName.Docks] = multiDocks;
            } },
            { EPassiveEffects.PassivWood, prop => {
                var mulityplay = SetMulityplayByCount(2, EPassiveEffects.PassivWood);
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Forest) * mulityplay;

                prop.Multiplikators[IBuilding.EBuildingName.Forest] = multi;
            } },
            { EPassiveEffects.PassivStone, prop => {
                var mulityplay = SetMulityplayByCount(2, EPassiveEffects.PassivStone);
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Mine) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Mine] = multi;
            } },
            { EPassiveEffects.PassivMetal, prop => {
                var mulityplay = SetMulityplayByCount(2, EPassiveEffects.PassivMetal);
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Smith) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Smith] = multi;
            } },
            { EPassiveEffects.PassivFaith, prop => {
                var mulityplay = SetMulityplayByCount(2, EPassiveEffects.PassivFaith);
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Shrine) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Shrine] = multi;
            } },
        };

        private static double GetMultiplikatorOr1(PropMultiplikatorsWorker prop, IBuilding.EBuildingName name) {
            double res = 0;
            if (prop.Multiplikators.ContainsKey(IBuilding.EBuildingName.Docks))
                res = prop.Multiplikators[IBuilding.EBuildingName.Docks];
            return res == 0 ? 1 : res;
        }

        private static int GetBuyNumber(PassiveEffect.EPassiveEffects effectName, Map? map = null) {
            map = map is null
                ? (GameObject.FindObjectOfType<Map>()?? throw new NullReferenceException(nameof(Map)))
                : map;
            if (map.PassiveEffects.ContainsKey(effectName) == false)
                return 0;
            return map.PassiveEffects[effectName].Count;
        }

        private static double SetMulityplayByCount(double now, PassiveEffect.EPassiveEffects effectName, Map? map = null) {
            return now * ((double)GetBuyNumber(effectName, map) * 2);
        }
        
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