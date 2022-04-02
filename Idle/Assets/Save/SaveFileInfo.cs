using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using static Idle.Utils.Utils;
#nullable enable
namespace Save {
    public readonly struct SaveFileInfo {
        private const string Ext = ".snatch";
        public readonly DateTime CreateTime;
        public readonly string FullPath;

        public SaveFileInfo(DateTime createTime, string fullPath) {
            CreateTime = createTime;
            FullPath = fullPath;
        }

        private static string GetDir() {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                throw new NotImplementedException($"TODO Write Lambda {nameof(GetDir)}");
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                return "~/.SnatchIdle";
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                throw new NotImplementedException($"TODO Write Lambda {nameof(GetDir)}");
            }
            else
                throw new Exception("OS Not Supported");
        }
        
        public static SaveFileInfo[] GetAllFiles() {
            var dirPath = GetDir();

            if (System.IO.Directory.Exists(dirPath) == false) return Array.Empty<SaveFileInfo>();

            return System.IO.Directory.GetFiles(dirPath)
                .Where(x => x.Contains(Ext) && long.TryParse(x.Replace(Ext, ""), out var _))
                .Select( x => 
                    new SaveFileInfo(
                        new DateTime(long.Parse(x.Replace(Ext, ""))),
                        System.IO.Path.Join(dirPath, x)
                        )
                ).ToArray();
        }

        public static void RemoveAllFiles() {
            foreach (var saveFileInfo in GetAllFiles()) System.IO.File.Delete(saveFileInfo.FullPath);
        }

        public static void CreateSaveFile(SaveFile saveFile) => 
            File.WriteAllBytes(Path.Join(GetDir(), saveFile.LastSave + Ext) 
                               ?? throw new NullReferenceException(nameof(FullPath)), saveFile.ToBytes());

        public static SaveFileInfo? GetLatesFileInfo() {
            var all = GetAllFiles();
            if (all.Length == 0) return null;

            var latest = all[0];
            foreach (var i in all) {
                if (latest.CreateTime > i.CreateTime) continue;

                latest = i;
            }

            return latest;
        }
    }
}