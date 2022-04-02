using System;
using JetBrains.Annotations;

#nullable enable
namespace Idle.Utils {
    public static class Utils {
        public static void ThrowIfNull(object? obj, string name) {
            if (obj is null) throw new NotImplementedException(name);
        }

        public static void ThrowIfNull(object? obj, ref string name) {
            if (obj is null) throw new NotImplementedException(name);
        }
    }
}