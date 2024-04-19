
// Type: ForestPlatformerExample.Spring
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;


namespace ForestPlatformerExample
{
  internal class Spring : AbstractInteractive
  {
    public int Power;

    public Spring(AbstractScene scene, Vector2 position, int power)
      : base(scene, position)
    {
      this.Active = true;
      this.Power = power;
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 16f, 9f, new Vector2(0.0f, 7f)));
      AnimationStateMachine newComponent = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(newComponent);
      newComponent.Offset = new Vector2(8f, 8f);
      SpriteSheetAnimation spriteSheetAnimation = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("SpringAnim"), 24);
      spriteSheetAnimation.Looping = false;
      SpriteSheetAnimation animation = spriteSheetAnimation;
      newComponent.RegisterAnimation("Bounce", (AbstractAnimation) animation);
    }

    public void Bounce()
    {
      this.GetComponent<AnimationStateMachine>().PlayAnimation(nameof (Bounce));
      AudioEngine.Play("SpringBounceSound");
    }
  }
}
