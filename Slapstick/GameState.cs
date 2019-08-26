namespace Slapstick
{
    /// <summary>
    /// Handles universal variables and special functions
    /// </summary>
    public static class GameState
    {

        public static int BeatsPerMinute = 80;


        /// <summary>
        /// Reset the game state to its default values
        /// </summary>
        public static void ResetGameState()
        {
            BeatsPerMinute = 80;
        }
    }
}
