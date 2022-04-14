using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Idle.Effect;
using Hint;
using Idle.Building;
using Unity.VisualScripting;

#nullable enable

namespace Save {
    [Serializable]
    public class SaveFile {
        public (ulong Worker, IBuilding.EBuildingName EBuildingName)?[,] Tiles;
        public Dictionary<ETypeHint, ulong> Cargo;
        public DateTime LastSave;
        public List<(PassiveEffect.EPassiveEffects, int Count)> PassiveEffectsList;
        
        public SaveFile(
            (ulong Worker, IBuilding.EBuildingName EBuildingName)?[,] tiles, 
            Dictionary<ETypeHint, ulong> cargo, 
            DateTime lastSave, 
            List<(PassiveEffect.EPassiveEffects, int Count)> passiveEffectsList) {
            
            Tiles = tiles;
            Cargo = cargo;
            LastSave = lastSave;
            PassiveEffectsList = passiveEffectsList;
        }

        public Byte[] ToBytes() {
            var ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);
            return ms.ToArray();
        }

        public static SaveFile? LoadFromFile(string path) {
          return ToThis(File.ReadAllBytes(path));
        }

        public static SaveFile? ToThis(Byte[] bytes) {
            var memStream = new MemoryStream();
            var binForm = new BinaryFormatter();
            memStream.Write(bytes, 0, bytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);

            return binForm.Deserialize(memStream) as SaveFile;
        }
    }
}