#nullable enable
using System;
using System.Collections.Generic;
using Idle.Building;
using UnityEngine;

namespace Idle.Effect {
    public class PassiveEffect {
        private Action<PropMultiplikatorsWorker, int> _func;
        private static readonly Dictionary<EPassiveEffects, Action<PropMultiplikatorsWorker, int>> CallFunc = new () {
            { EPassiveEffects.PassivFood, (prop, count) => {
                var mulityplay = 2 * count;
                double multiFarm = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Farm) * mulityplay;
                double multiDocks = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Docks) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Farm] = multiFarm;
                
            } },
            { EPassiveEffects.PassivWood, (prop, count) => {
                var mulityplay = 2 * count;
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Forest) * mulityplay;

                prop.Multiplikators[IBuilding.EBuildingName.Forest] = multi;
            } },
            { EPassiveEffects.PassivStone, (prop, count) => {
                var mulityplay = 2 * count;
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Mine) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Mine] = multi;
            } },
            { EPassiveEffects.PassivMetal, (prop, count) => {
                var mulityplay = 2 * count;
                double multi = GetMultiplikatorOr1(prop, IBuilding.EBuildingName.Smith) * mulityplay;
                
                prop.Multiplikators[IBuilding.EBuildingName.Smith] = multi;
            } },
            { EPassiveEffects.PassivFaith, (prop, count) => {
                var mulityplay = 2 * count;
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
        
        public void CallEffect(PropMultiplikatorsWorker prop, int count) => _func(prop, count);

        private PassiveEffect() => _func = (worker, count) => { };
        
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