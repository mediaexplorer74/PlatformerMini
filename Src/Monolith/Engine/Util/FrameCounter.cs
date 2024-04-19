
// Type: MonolithEngine.FrameCounter
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using System;
using System.Collections.Generic;
using System.Linq;


namespace MonolithEngine
{
  public class FrameCounter
  {
    public const int MAXIMUM_SAMPLES = 100;
    private Queue<float> sampleBuffer = new Queue<float>();

    public long TotalFrames { get; private set; }

    public float TotalSeconds { get; private set; }

    public float AverageFramesPerSecond { get; private set; }

    public float CurrentFramesPerSecond { get; private set; }

    public bool Update(float deltaTime)
    {
      this.CurrentFramesPerSecond = 1f / deltaTime;
      this.sampleBuffer.Enqueue(this.CurrentFramesPerSecond);
      if (this.sampleBuffer.Count > 100)
      {
        double num = (double) this.sampleBuffer.Dequeue();
        this.AverageFramesPerSecond = this.sampleBuffer.Average<float>((Func<float, float>) (i => i));
      }
      else
        this.AverageFramesPerSecond = this.CurrentFramesPerSecond;
      ++this.TotalFrames;
      this.TotalSeconds += deltaTime;
      return true;
    }
  }
}
