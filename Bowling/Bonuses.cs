using System.Collections.Generic;

namespace Bowling
{
    public class Bonuses
    {
        private ICollection<IBonus> bonuses;

        public Bonuses()
        {
            this.bonuses = new List<IBonus>();
        }

        public virtual void UpdateWithFramesNewFrame(Frames frames, Frame newFrame)
        {
            RememberBonusForFrame(newFrame);
            RequestBonusesFromFrames(frames);
        }

        private void RememberBonusForFrame(Frame frame) =>
            frame.AnnounceToBonuses(this);

        private void RequestBonusesFromFrames(Frames frames)
        {
            foreach (IBonus bonus in bonuses)
                RequestBonusFromFrames(bonus, frames);
        }

        private void RequestBonusFromFrames(IBonus bonus, Frames frames) =>
            bonus.RequestFromFrames(frames);

        public virtual void RememberBonus(IBonus bonus)
        {
            bonuses.Add(bonus);
        }
    }
}
