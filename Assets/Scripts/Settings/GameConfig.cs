using Unity.VisualScripting;

namespace Settings
{
    public static class GameConfig
    {
        public const int TicksPerSecond = 4;

        public const string GameSceneName = "GameScene";

        public const float OrthographicCameraSizeOffset = 3.0f;

        public const float TickRate = 1f / TicksPerSecond;
    }
}