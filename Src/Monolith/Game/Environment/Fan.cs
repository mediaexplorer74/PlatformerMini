
// Type: ForestPlatformerExample.Fan
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;


namespace ForestPlatformerExample
{
  internal class Fan : Entity
  {
    public int ForceFieldHeight;
    private Vector2 drawOffset = new Vector2(7f, 8f);

    public Fan(AbstractScene scene, Vector2 position, int forceFeildHeight = 256)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      if (forceFeildHeight <= 0)
        throw new Exception("Incorrect force field height for fan!");
      this.AddTag(nameof (Fan));
      this.ForceFieldHeight = forceFeildHeight;
      AnimationStateMachine newComponent = new AnimationStateMachine();
      newComponent.Offset = this.drawOffset;
      this.AddComponent<AnimationStateMachine>(newComponent);
      SpriteSheetAnimation animation = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("FanAnim"), 24);
      animation.Scale = 2f;
      newComponent.RegisterAnimation("FanWorking", (AbstractAnimation) animation);
      this.AddComponent<BoxTrigger>(new BoxTrigger((Entity) this, 64, this.ForceFieldHeight, new Vector2(-32f, (float) -this.ForceFieldHeight) + this.drawOffset));
      this.AddTriggeredAgainst(typeof (Hero));
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 46f, 8f, new Vector2(-14f, 0.0f)));
      this.Active = true;
    }

    public override void OnEnterTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).EnterFanArea(this);
      base.OnEnterTrigger(triggerTag, otherEntity);
    }

    public override void OnLeaveTrigger(string triggerTag, IGameObject otherEntity)
    {
      if (otherEntity is Hero)
        (otherEntity as Hero).LeaveFanArea();
      base.OnEnterTrigger(triggerTag, otherEntity);
    }
  }
}
