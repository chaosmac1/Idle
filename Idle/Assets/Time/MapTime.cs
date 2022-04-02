using System;

namespace Time {
    public readonly struct MapTime {
        public readonly float DeltaTime;
        public readonly DateTime LastUpdate;

        public MapTime(float deltaTime, DateTime lastUpdate) {
            DeltaTime = deltaTime;
            LastUpdate = lastUpdate;
        }

        public MapTime NextUpdate() {
            throw new NotImplementedException("TODO Write Lambda " + nameof(NextUpdate));
        }

        public static MapTime Start() {
            throw new NotImplementedException($"TODO Write Lambda {nameof(Start)}");
        }
    }
}