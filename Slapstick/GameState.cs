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

        public static int BeatsPerMinute = 140;
        public static int Score = 0;
        public static int Lives = 3;
        public static GamePlayState CurrentGamePlayState = GamePlayState.InGame;

        private const int MAX_BEATS_PER_MINUTE = 240;


        /// <summary>
        /// Reset the game state to its default values
        /// </summary>
        public static void ResetGameState()
        {
            BeatsPerMinute = 80;
            Score = 0;
            Lives = 3;
            CurrentGamePlayState = GamePlayState.MainMenu;
        }

        /// <summary>
        /// Adds more beats per minute
        /// </summary>
        public static void AddBPM()
        {
            if(BeatsPerMinute <= MAX_BEATS_PER_MINUTE)
            {
                BeatsPerMinute += 20;
            }
        }



    }
}
