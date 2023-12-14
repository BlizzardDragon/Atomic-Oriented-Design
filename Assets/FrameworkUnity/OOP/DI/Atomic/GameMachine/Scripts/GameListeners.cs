namespace GameMachines
{
    public interface IGameStartListener
    {
        void OnGameStart();
    }

    public interface IGamePauseListener
    {
        void OnGamePause();
    }

    public interface IGameResumeListener
    {
        void OnGameResume();
    }

    public interface IGameStopListener
    {
        void OnGameStop();
    }
}