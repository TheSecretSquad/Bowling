﻿using System.Collections.Generic;
using System.Linq;

namespace Bowling
{
    public class Frames
    {
        private IList<Frame> frames;

        public Frames()
        {
            this.frames = new List<Frame>();
        }

        public virtual void RecordFrame(Frame frame)
        {
            InitializeNextFrame(frame);
            frames.Add(frame);
        }

        private void InitializeNextFrame(Frame nextFrame) =>
            InitializeNextFrameFromFrame(nextFrame, LastFrame());

        private void InitializeNextFrameFromFrame(Frame nextFrame, Frame fromFrame)
        {
            if (fromFrame != null)
                fromFrame.InitializeNextFrame(nextFrame);
        }

        private Frame LastFrame() => FrameForFrameNumber(frames.Count);

        public virtual void RequestBonusFromFrameNumberToFrameNumber(PositiveInteger fromFrameNumber, PositiveInteger toFrameNumber)
        {
            ContributeToFrameFromFrame(FrameForFrameNumber(toFrameNumber), FrameForFrameNumber(fromFrameNumber));
        }

        private void ContributeToFrameFromFrame(Frame toFrame, Frame fromFrame)
        {
            if (fromFrame != null && toFrame != null)
                toFrame.ContributeFromFrame(fromFrame);
        }

        private Frame FrameForFrameNumber(PositiveInteger frameNumber) =>
            FrameAtIndex(IndexOfFrameNumber(frameNumber));

        private Frame FrameAtIndex(int frameIndex) =>
            frames.ElementAtOrDefault(frameIndex);

        private int IndexOfFrameNumber(int frameNumber) =>
            frameNumber - 1;

        public virtual void PrintOn(IFramesPrinter framesPrinter)
        {
            framesPrinter.BeginPrint(this);
            PrintFrames(framesPrinter);
            framesPrinter.EndPrint(this);
        }

        private void PrintFrames(IFramesPrinter framesPrinter)
        {
            foreach (Frame frame in frames)
                frame.PrintOn(framesPrinter);
        }
    }
}
