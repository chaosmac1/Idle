using System;
using System.Collections.Generic;
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
                return System.IO.Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalizedResources), "SnatchIdle");
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                return System.IO.Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".SnatchIdle");
            } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                throw new NotImplementedException($"TODO Write Lambda {nameof(GetDir)}");
            }
            else
                throw new Exception("OS Not Supported");
        }
        
        public static SaveFileInfo[] GetAllFiles() {
            var dirPath = GetDir();

            if (System.IO.Directory.Exists(dirPath) == false) return Array.Empty<SaveFileInfo>();

            var res = new List<SaveFileInfo>(16);
            
            foreach (var fullFilePath in System.IO.Directory.GetFiles(dirPath)) {
                if(fullFilePath.Contains(Ext) == false) continue;

                var start = fullFilePath.LastIndexOf("/", StringComparison.Ordinal);
                
                var ticksString = fullFilePath.AsSpan(start + 1, fullFilePath.Length - Ext.Length - start -1);
                
                var ticks = Int64.Parse(ticksString);
                
                res.Add(
                    new SaveFileInfo(new DateTime(ticks, DateTimeKind.Utc),
                    fullFilePath
                ));
            }

            return res.ToArray();
        }

        public static void RemoveAllFiles() {
            foreach (var saveFileInfo in GetAllFiles()) System.IO.File.Delete(saveFileInfo.FullPath);
        }

        public static void CreateSaveFile(SaveFile saveFile) {
            var dir = GetDir();
            if (System.IO.Directory.Exists(dir) == false)
                System.IO.Directory.CreateDirectory(dir);
            File.WriteAllBytes(Path.Join(dir, saveFile.LastSave.Ticks + Ext)
                               ?? throw new NullReferenceException(nameof(FullPath)), saveFile.ToBytes());
        }

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