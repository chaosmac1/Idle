using System;

namespace LambdaTime {
    public readonly struct MapTime {
        public readonly float DeltaTime;
        public readonly DateTime LastUpdate;

        public MapTime(float deltaTime, DateTime lastUpdate) {
            DeltaTime = deltaTime;
            LastUpdate = lastUpdate;
        }

        public MapTime NextUpdate() {
            var utcNow = DateTime.UtcNow;
            var deltaTime = ((float) (utcNow.Ticks - LastUpdate.Ticks)) / 100000f;
            return new MapTime(deltaTime, utcNow);
        }

        public static MapTime Start() => new MapTime(1, DateTime.UtcNow);

        public float Fps() => 1f / DeltaTime;
    }
}