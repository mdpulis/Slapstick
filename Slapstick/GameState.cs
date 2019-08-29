namespace Slapstick
{

    /// <summary>
    /// Represents the current state of the gameplay (menu, in game, retry screen, etc.)
    /// </summary>
    public enum GameplayState
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

        public static bool BarrierOn = false;


        public static int BeatsPerMinute = 180;
        public static int Score = 0;
        public static int Lives = 3;
        public static GameplayState CurrentGameplayState = GameplayState.MainMenu;

        private const int MAX_BEATS_PER_MINUTE = 250;
        private const int ADD_BEATS_PER_MINUTE = 10;



        /// <summary>
        /// Reset the game state to its default values
        /// </summary>
        public static void ResetGameState()
        {
            BeatsPerMinute = 180;
            Score = 0;
            Lives = 3;
            CurrentGameplayState = GameplayState.MainMenu;
        }

        /// <summary>
        /// Adds more beats per minute
        /// </summary>
        public static void AddBPM()
        {
            if(BeatsPerMinute < MAX_BEATS_PER_MINUTE)
            {
                BeatsPerMinute += ADD_BEATS_PER_MINUTE;
            }
        }

        /// <summary>
        /// Progress to the next gameplay state
        /// </summary>
        public static void ProgressGameplayState()
        {
            switch (CurrentGameplayState)
            {
                case (GameplayState.MainMenu):
                    CurrentGameplayState = GameplayState.InGame;
                    break;
                case (GameplayState.InGame):
                    CurrentGameplayState = GameplayState.RetryScreen;
                    break;
                case (GameplayState.RetryScreen):
                    CurrentGameplayState = GameplayState.MainMenu;
                    break;
            }
        }



    }
}
