namespace Slapstick
{

    /// <summary>
    /// Represents the current state of the gameplay (menu, in game, retry screen, etc.)
    /// </summary>
    public enum GamePlayState
    {
        MainMenu = 0,
        InGame = 1,
        RetryScreen = 2,
    }

    /// <summary>
    /// Handles universal variables and special functions
    /// </summary>
    public static class GameState
    {

        public static int BeatsPerMinute = 80;
        public static int Score = 0;
        public static GamePlayState CurrentGamePlayState = GamePlayState.InGame;


        /// <summary>
        /// Reset the game state to its default values
        /// </summary>
        public static void ResetGameState()
        {
            BeatsPerMinute = 80;
            Score = 0;
            CurrentGamePlayState = GamePlayState.MainMenu;
        }

        public static void AddBPM()
        {
            BeatsPerMinute += 20;
        }



    }
}
