// Type: MonolithEngine.StaticTransform
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using System;


namespace MonolithEngine
{
  public class StaticTransform : AbstractTransform
  {
    //private Vector2 velocity;

    public StaticTransform(IGameObject owner, Vector2 position = default (Vector2))
    : base(owner, position)
    {
    }

    public override Vector2 Velocity
    {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
            //get => this.velocity;
            //set => this.velocity = value;
    }

    public override float VelocityX
    {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
            //get => this.velocity.X;
            //set => this.velocity.X = value;
    }

    public override float VelocityY
    {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
            //get => this.velocity.Y;
            //set => this.velocity.Y = value;
    }
  }
}
