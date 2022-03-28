#nullable enable
using System;
using System.Collections.Generic;

namespace Effect {
    public class PassiveEffect {
        private Action<PropMultiplikatorsWorker> _func;
        private static readonly Dictionary<EPassiveEffects, Action<PropMultiplikatorsWorker>> CallFunc = new () {
            // TODO Write Hendrik CallFunc 
        };

        public void CallEffect(PropMultiplikatorsWorker prop) => _func(prop);

        private PassiveEffect() => _func = worker => { };
        
        public PassiveEffect(EPassiveEffects passiveEffect) {
            if (CallFunc.TryGetValue(passiveEffect, out _func)) 
                return;
            throw new Exception($"{passiveEffect} Not Found In Dic CallFunc");
        }
        
        
        public enum EPassiveEffects {
            Faith,
            
            // TODO Write Hendrik EPassiveEffects 
        }
    }
}