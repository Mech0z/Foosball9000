
namespace Logic
{
    public class EloRating : IRating
    {
        /// <summary>
        /// Temp elo rating calc
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// <param name="playerOneWin"></param>
        /// <returns></returns>

        public double CalculateRating(int player1, int player2, bool playerOneWin)
        {
            const double medium = 20;
            const double diversification = 20;
            const double minRating = medium - diversification;
            const double maxRating = medium + diversification;

            double diff;

            if (playerOneWin)
            {
                diff = player1 - player2;
            }
            else
            {
                diff = player2 - player1;
            }

            var result = (medium * diversification - diff) / medium + minRating;

            if (result > maxRating)
            {
                result = maxRating;
            }
            else if (result < minRating)
            {
                result = minRating;
            }

            return result;
        }
    }
}