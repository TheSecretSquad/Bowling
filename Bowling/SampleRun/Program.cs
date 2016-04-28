using System;

namespace Bowling
{
    class Program
    {
        static void Main(string[] args)
        {
            IBowlingScoreCard bowlingScoreCard = CreateScoreCard();
            SampleGame sampleGame = CreateSampleGame(bowlingScoreCard);
            IBowlingScoreCardPrinter sampleScoreCardPrinter = CreateBleh();

            sampleGame.BowlAllStrikes();
            Console.WriteLine("All Strikes");
            Console.WriteLine("===========");
            bowlingScoreCard.PrintOn(sampleScoreCardPrinter);

            bowlingScoreCard = CreateScoreCard();
            sampleGame = CreateSampleGame(bowlingScoreCard);
            sampleScoreCardPrinter = CreateBleh();

            Console.WriteLine("All Open Frames");
            Console.WriteLine("===============");
            sampleGame.BowlAllOpenFrames();
            bowlingScoreCard.PrintOn(sampleScoreCardPrinter);
        }

        private static IBowlingScoreCard CreateScoreCard() =>
            new BowlingScoreCard(new Frames(), new Bonuses());

        private static SampleGame CreateSampleGame(IBowlingScoreCard bowlingScoreCard) =>
            new SampleGame(bowlingScoreCard, new SampleGameFrameFactory());

        private static SampleScoreCardPrinter CreateBleh() =>
            new SampleScoreCardPrinter();

    }
}
