using Bowling.Printing;
using System;

namespace Bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            new ConsoleTestGame("All Strikes", sampleGame => sampleGame.BowlAllStrikes())
                .Run();

            new ConsoleTestGame("All Open Frames", sampleGame => sampleGame.BowlAllOpenFrames())
                .Run();

            new ConsoleTestGame("All Spares", sampleGame => sampleGame.BowlAllSparesWithThrow1AndTenthFrameBonusThrow(new Throw(2), new Throw(5)))
                .Run();
        }

        private class ConsoleTestGame
        {
            private static IBowlingScoreCard CreateScoreCard() =>
                new BowlingScoreCard(new Frames(), new Bonuses());

            private static SampleGame CreateSampleGame(IBowlingScoreCard bowlingScoreCard) =>
                new SampleGame(bowlingScoreCard, new SampleGameFrameFactory());

            private static SampleScoreCardPrinter CreateSampleScoreCardPrinter() =>
                new SampleScoreCardPrinter();

            private IBowlingScoreCard bowlingScoreCard;
            private SampleGame sampleGame;
            private IBowlingScoreCardPrinter sampleScoreCardPrinter;
            private string name;
            private Action<SampleGame> gameRunner;

            public ConsoleTestGame(string name, Action<SampleGame> gameRunner)
            {
                this.name = name;
                this.gameRunner = gameRunner;
                this.bowlingScoreCard = CreateScoreCard();
                this.sampleGame = CreateSampleGame(bowlingScoreCard);
                this.sampleScoreCardPrinter = CreateSampleScoreCardPrinter();
            }

            public void Run()
            {
                Console.WriteLine(name);
                Console.WriteLine("===============");
                gameRunner(sampleGame);
                bowlingScoreCard.PrintOn(sampleScoreCardPrinter);
            }
        }
    }
}
