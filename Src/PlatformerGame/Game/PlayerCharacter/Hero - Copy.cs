// Type: ForestPlatformerExample.Hero
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonolithEngine;
using System;
using System.Collections.Generic;

#nullable disable
namespace ForestPlatformerExample
{
  internal class Hero : PhysicalEntity
  {
    private readonly int CAMERA_HOOK_SPEED = 5;
    private readonly float JUMP_RATE = 0.1f;
    private readonly float SLIDE_FORCE = 1f;
    private static double lastJump;
    private bool doubleJumping;
    private bool canJump = true;
    private bool canDoubleJump;
    private Vector2 jumpModifier = Vector2.Zero;
    private bool canAttack = true;
    private float climbSpeed = Config.CHARACTER_SPEED / 2f;
    public Fist fist;
    private bool isCarryingItem;
    private IMovableItem overlappingItem;
    private IMovableItem carriedItem;
    private Vector2 originalAnimOffset = Vector2.Zero;
    private AnimationStateMachine Animations;
    public Ladder Ladder;
    private List<IGameObject> overlappingEnemies = new List<IGameObject>(5);
    private bool isWallSliding;
    private Direction slideDirection;
    private Direction jumpDirection;
    public Vector2 LastSpawnPoint;
    private Vector2 autoMovementSpeed = Vector2.Zero;
    private bool levelEndReached;
    private Random random;
    private float horizFrictBackup;
    public bool MovementButtonDown;
    private Fan fan;
    private UserInputController UserInput;
    private CoinPickupEffect coinEffect;
    private bool descended;

    private bool isSliding
    {
      get => this.slideDirection == Direction.EAST || this.slideDirection == Direction.WEST;
    }

    public bool LevelEndReached
    {
      get => this.levelEndReached;
      set
      {
        if (value)
          this.SetupAutoMovement();
        else
          this.DisableAutoMovement();
        this.levelEndReached = value;
      }
    }

    public bool ReadyForNextLevel => this.Ladder != null;

    public bool OnIce
    {
      set
      {
        if (value)
        {
          this.HorizontalFriction = 0.92f;
          this.MovementSpeed /= 5f;
        }
        else
        {
          this.HorizontalFriction = Config.HORIZONTAL_FRICTION;
          this.MovementSpeed = Config.CHARACTER_SPEED;
        }
      }
      get => (double) this.HorizontalFriction != (double) Config.HORIZONTAL_FRICTION;
    }

    public Hero(AbstractScene scene, Vector2 position)
      : base(scene.LayerManager.EntityLayer, startPosition: position)
    {
      this.DrawPriority = 0.0f;
      this.LastSpawnPoint = position;
      this.random = new Random();
      this.AddCollisionAgainst(typeof (Coin));
      this.AddCollisionAgainst(typeof (Box));
      this.AddCollisionAgainst(typeof (Spring));
      this.AddCollisionAgainst(typeof (Carrot));
      this.AddCollisionAgainst(typeof (Ghost));
      this.AddCollisionAgainst(typeof (IceCream));
      this.AddCollisionAgainst(typeof (Rock));
      this.AddCollisionAgainst(typeof (SpikedTurtle));
      this.AddCollisionAgainst(typeof (Trunk));
      this.AddCollisionAgainst(typeof (MovingPlatform), false);
      this.AddCollisionAgainst(typeof (Ladder));
      this.AddCollisionAgainst(typeof (IceTrigger));
      this.AddCollisionAgainst(typeof (NextLevelTrigger));
      this.AddCollisionAgainst(typeof (PopupTrigger));
      this.AddCollisionAgainst(typeof (RespawnPoint));
      this.AddCollisionAgainst(typeof (SlideWall));
      this.AddCollisionAgainst(typeof (Bullet));
      this.AddCollisionAgainst(typeof (Spikes));
      this.AddCollisionAgainst(typeof (IceCreamProjectile));
      this.AddCollisionAgainst(typeof (Saw));
      this.AddCollisionAgainst(typeof (Fan));
      this.AddCollisionAgainst(typeof (GameFinishTrophy));
      this.AddTag(nameof (Hero));
      this.CanFireTriggers = true;
      this.BlocksRay = true;
      this.coinEffect = new CoinPickupEffect(this.Scene);
      this.AddComponent<BoxCollisionComponent>(new BoxCollisionComponent((IColliderEntity) this, 16f, 25f, new Vector2(-8f, -24f)));
      this.SetupAnimations();
      this.SetupController();
      this.CurrentFaceDirection = Direction.EAST;
      this.fist = new Fist(scene, (Entity) this, new Vector2(20f, -10f));
    }

    private void SetupAnimations()
    {
      this.Animations = new AnimationStateMachine();
      this.AddComponent<AnimationStateMachine>(this.Animations);
      this.Animations.Offset = new Vector2(0.0f, -32f);
      this.CollisionOffsetRight = 0.45f;
      this.CollisionOffsetLeft = 0.6f;
      this.CollisionOffsetBottom = 1f;
      this.CollisionOffsetTop = 1f;
      SpriteSheetAnimation spriteSheetAnimation1 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroHurt"), 24);
      spriteSheetAnimation1.Looping = false;
      SpriteSheetAnimation animation1 = spriteSheetAnimation1;
      this.Animations.RegisterAnimation("HurtRight", (AbstractAnimation) animation1, (Func<bool>) (() => false));
      this.Animations.RegisterAnimation("HurtLeft", (AbstractAnimation) animation1.CopyFlipped(), (Func<bool>) (() => false));
      SpriteSheetAnimation animation2 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroIdle"), 24);

      bool isIdleRight() => CurrentFaceDirection == Direction.EAST && !isCarryingItem;
      this.Animations.RegisterAnimation("IdleRight", (AbstractAnimation) animation2, isIdleRight);

      bool isIdleLeft() => CurrentFaceDirection == Direction.WEST && !isCarryingItem;

      this.Animations.RegisterAnimation("IdleLeft", (AbstractAnimation) animation2.CopyFlipped(), isIdleLeft);

      SpriteSheetAnimation spriteSheetAnimation2 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroIdleWithItem"), 24);
      spriteSheetAnimation2.AnimationSwitchCallback = (Action) (() =>
      {
        if (this.carriedItem == null)
          return;
        (this.carriedItem as Entity).GetComponent<AnimationStateMachine>().Offset = this.originalAnimOffset;
      });
      spriteSheetAnimation2.EveryFrameAction = (Action<int>) (frame =>
      {
        if (this.carriedItem == null)
          return;
        Entity carriedItem = this.carriedItem as Entity;
        Vector2 offset = carriedItem.GetComponent<AnimationStateMachine>().Offset;
        float num = 0.5f;
        switch (frame)
        {
          case 2:
          case 3:
          case 8:
          case 14:
          case 15:
          case 20:
            offset.Y += num;
            break;
          case 6:
          case 18:
            offset.Y -= num;
            break;
          case 7:
          case 19:
            offset.Y -= 2f * num;
            break;
        }
        carriedItem.GetComponent<AnimationStateMachine>().Offset = offset;
      });
      SpriteSheetAnimation animation3 = spriteSheetAnimation2;

      bool isIdleRight1() => CurrentFaceDirection == Direction.EAST && !isCarryingItem;
      this.Animations.RegisterAnimation("IdleCarryRight", (AbstractAnimation) animation3, isIdleRight1);

      bool isIdleLeft1() => CurrentFaceDirection == Direction.WEST && !isCarryingItem;
      this.Animations.RegisterAnimation("IdleCarryLeft", (AbstractAnimation) animation3.CopyFlipped(), isIdleLeft1);
      SpriteSheetAnimation animation4 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroRun"), 40);
      animation4.EveryFrameAction = (Action<int>) (frame =>
      {
        if (frame != 1 && frame != 6)
          return;
        AudioEngine.Play("FastFootstepsSound");
      });
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("RunningRight", (AbstractAnimation) animation4, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isRunningRight\u007C41_7)), 1);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("RunningLeft", (AbstractAnimation) animation4.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isRunningLeft\u007C41_8)), 1);
      SpriteSheetAnimation spriteSheetAnimation3 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroRun"), 12, SpriteEffects.FlipHorizontally);
      SpriteSheetAnimation spriteSheetAnimation4 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroRunWithItem"), 24);
      spriteSheetAnimation4.AnimationSwitchCallback = (Action) (() =>
      {
        if (this.carriedItem == null)
          return;
        (this.carriedItem as Entity).GetComponent<AnimationStateMachine>().Offset = this.originalAnimOffset;
      });
      spriteSheetAnimation4.EveryFrameAction = (Action<int>) (frame =>
      {
        if (this.carriedItem == null)
          return;
        Entity carriedItem = this.carriedItem as Entity;
        Vector2 offset = carriedItem.GetComponent<AnimationStateMachine>().Offset;
        float num = 3f;
        switch (frame)
        {
          case 3:
          case 8:
            offset.Y += num;
            break;
          case 4:
          case 9:
            offset.Y -= num;
            break;
        }
        carriedItem.GetComponent<AnimationStateMachine>().Offset = offset;
      });
      SpriteSheetAnimation animation5 = spriteSheetAnimation4;
      SpriteSheetAnimation spriteSheetAnimation5 = animation5;
      spriteSheetAnimation5.EveryFrameAction = spriteSheetAnimation5.EveryFrameAction + (Action<int>) (frame =>
      {
        if (frame != 1 && frame != 6)
          return;
        AudioEngine.Play("FastFootstepsSound");
      });
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("RunningCarryRight", (AbstractAnimation) animation5, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isRunningCarryRight\u007C41_9)), 1);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("RunningCarryLeft", (AbstractAnimation) animation5.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isRunningCarryLeft\u007C41_10)), 1);
      SpriteSheetAnimation spriteSheetAnimation6 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroJump"), 24);
      spriteSheetAnimation6.Looping = false;
      SpriteSheetAnimation animation6 = spriteSheetAnimation6;
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("JumpingRight", (AbstractAnimation) animation6, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isJumpingRight\u007C41_11)), 2);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("JumpingLeft", (AbstractAnimation) animation6.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isJumpingLeft\u007C41_12)), 2);
      this.Animations.AddFrameTransition("JumpingRight", "JumpingLeft");
      SpriteSheetAnimation spriteSheetAnimation7 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroJumpWithItem"), 24);
      spriteSheetAnimation7.Looping = false;
      SpriteSheetAnimation animation7 = spriteSheetAnimation7;
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("CarryJumpingRight", (AbstractAnimation) animation7, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isCarryJumpingRight\u007C41_13)), 2);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("JumpingCarryLeft", (AbstractAnimation) animation7.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isCarryJumpingLeft\u007C41_14)), 2);
      this.Animations.AddFrameTransition("CarryJumpingRight", "JumpingCarryLeft");
      SpriteSheetAnimation animation8 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroWallSlide"), 12, SpriteEffects.FlipHorizontally);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("WallSlideRight", (AbstractAnimation) animation8, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isWallSlidingRight\u007C41_15)), 6);
      SpriteSheetAnimation spriteSheetAnimation8 = animation8;
      spriteSheetAnimation8.Offset = spriteSheetAnimation8.Offset + new Vector2(6f, 0.0f);
      SpriteSheetAnimation animation9 = animation8.CopyFlipped();
      SpriteSheetAnimation spriteSheetAnimation9 = animation8;
      spriteSheetAnimation9.Offset = spriteSheetAnimation9.Offset + new Vector2(-12f, 0.0f);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("WallSlideLeft", (AbstractAnimation) animation9, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isWallSlidingLeft\u007C41_16)), 6);
      SpriteSheetAnimation spriteSheetAnimation10 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroDoubleJump"), 12);
      spriteSheetAnimation10.StartFrame = 12;
      spriteSheetAnimation10.EndFrame = 16;
      SpriteSheetAnimation animation10 = spriteSheetAnimation10;
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("DoubleJumpingRight", (AbstractAnimation) animation10, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isDoubleJumpingRight\u007C41_17)), 3);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("DoubleJumpingLeft", (AbstractAnimation) animation10.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isDoubleJumpingLeft\u007C41_18)), 3);
      this.Animations.AddFrameTransition("DoubleJumpingRight", "DoubleJumpingLeft");
      SpriteSheetAnimation animation11 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroClimb"), 40);
      animation11.EveryFrameAction = (Action<int>) (frame =>
      {
        if (frame != 1 && frame != 7)
          return;
        AudioEngine.Play("FastFootstepsSound");
      });
      // ISSUE: method pointer
      animation11.AddFrameAction(1, new Action<int>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__setSpeed\u007C41_21)));
      // ISSUE: method pointer
      animation11.AddFrameAction(7, new Action<int>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__setSpeed\u007C41_21)));
      // ISSUE: method pointer
      animation11.AnimationPauseCondition = new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isHangingOnLadder\u007C41_23));
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("ClimbingLadder", (AbstractAnimation) animation11, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isClimbing\u007C41_22)), 6);
      SpriteSheetAnimation spriteSheetAnimation11 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroJump"), 24);
      spriteSheetAnimation11.StartFrame = 9;
      spriteSheetAnimation11.EndFrame = 11;
      SpriteSheetAnimation animation12 = spriteSheetAnimation11;
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("FallingRight", (AbstractAnimation) animation12, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isFallingRight\u007C41_24)), 5);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("FallingLeft", (AbstractAnimation) animation12.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isFallingLeft\u007C41_25)), 5);
      this.Animations.AddFrameTransition("FallingRight", "FallingLeft");
      SpriteSheetAnimation spriteSheetAnimation12 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroJumpWithItem"), 24);
      spriteSheetAnimation12.StartFrame = 9;
      spriteSheetAnimation12.EndFrame = 11;
      SpriteSheetAnimation animation13 = spriteSheetAnimation12;
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("CarryFallingRight", (AbstractAnimation) animation13, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isCarryFallingRight\u007C41_26)), 5);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("CarryFallingLeft", (AbstractAnimation) animation13.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isCarryFallingLeft\u007C41_27)), 5);
      this.Animations.AddFrameTransition("CarryFallingRight", "CarryFallingLeft");
      SpriteSheetAnimation spriteSheetAnimation13 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroAttack"), 48);
      spriteSheetAnimation13.Looping = false;
      SpriteSheetAnimation animation14 = spriteSheetAnimation13;
      this.Animations.RegisterAnimation("AttackRight", (AbstractAnimation) animation14, (Func<bool>) (() => false), 8);
      this.Animations.RegisterAnimation("AttackLeft", (AbstractAnimation) animation14.CopyFlipped(), (Func<bool>) (() => false), 8);
      SpriteSheetAnimation spriteSheetAnimation14 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroPickup"), 24);
      spriteSheetAnimation14.Looping = false;
      spriteSheetAnimation14.StartedCallback = (Action) (() => this.UserInput.ControlsDisabled = true);
      spriteSheetAnimation14.StoppedCallback = (Action) (() => this.UserInput.ControlsDisabled = false);
      SpriteSheetAnimation animation15 = spriteSheetAnimation14;
      animation15.AddFrameAction(15, (Action<int>) (frame =>
      {
        this.carriedItem.Lift((Entity) this, new Vector2(0.0f, -20f));
        AudioEngine.Play("BoxPickup");
      }));
      this.Animations.RegisterAnimation("PickupRight", (AbstractAnimation) animation15, (Func<bool>) (() => false));
      this.Animations.RegisterAnimation("PickupLeft", (AbstractAnimation) animation15.CopyFlipped(), (Func<bool>) (() => false));
      SpriteSheetAnimation spriteSheetAnimation15 = new SpriteSheetAnimation((Entity) this, Assets.GetAnimationTexture("HeroSlide"), 24);
      spriteSheetAnimation15.Looping = false;
      SpriteSheetAnimation animation16 = spriteSheetAnimation15;
      animation16.StoppedCallback = (Action) (() =>
      {
        this.slideDirection = Direction.CENTER;
        this.canAttack = true;
        this.HorizontalFriction = this.horizFrictBackup;
      });
      animation16.AnimationSwitchCallback = (Action) (() =>
      {
        this.slideDirection = Direction.CENTER;
        this.canAttack = true;
        this.HorizontalFriction = this.horizFrictBackup;
      });
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("SlideRight", (AbstractAnimation) animation16, new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isSlidingRight\u007C41_35)), 7);
      // ISSUE: method pointer
      this.Animations.RegisterAnimation("SlideLeft", (AbstractAnimation) animation16.CopyFlipped(), new Func<bool>((object) this, __methodptr(\u003CSetupAnimations\u003Eg__isSlidingLeft\u007C41_36)), 7);
      this.Animations.AddFrameTransition("SlideRight", "SlideLeft");
    }

    private void SetupController()
    {
      this.UserInput = new UserInputController();
      this.AddComponent<UserInputController>(this.UserInput);
      this.UserInput.RegisterKeyPressAction(Keys.R, (Action) (() => this.ResetPosition(new Vector2((float) (12 * Config.GRID), (float) (12 * Config.GRID)))), true);
      this.UserInput.RegisterKeyPressAction(Keys.Right, new Action(this.MoveRight));
      this.UserInput.RegisterKeyPressAction(Keys.Left, new Action(this.MoveLeft));
      this.UserInput.RegisterKeyPressAction(Keys.Up, new Action(this.Jump), true);
      this.UserInput.RegisterKeyPressAction(Keys.Space, new Action(this.AttackOrThrow), true);
      this.UserInput.RegisterKeyPressAction(Keys.RightControl, new Action(this.Slide), true);
      this.UserInput.RegisterKeyPressAction(Keys.LeftControl, new Action(this.Slide), true);
      this.UserInput.RegisterKeyPressAction(Keys.Down, new Action(this.ClimbDownOrDescend));
      this.UserInput.RegisterKeyReleaseAction(Keys.Down, new Action(this.ClimbDescendRelease));
      this.UserInput.RegisterKeyPressAction(Keys.Up, new Action(this.ClimbUpOnLadder));
      this.UserInput.RegisterKeyPressAction(Keys.LeftShift, new Action(this.InteractWithItem), true);
      this.UserInput.RegisterKeyPressAction(Keys.RightShift, new Action(this.InteractWithItem), true);
      this.UserInput.RegisterKeyReleaseAction(Keys.Left, (Action) (() => this.MovementButtonDown = false));
      this.UserInput.RegisterKeyReleaseAction(Keys.Right, (Action) (() => this.MovementButtonDown = false));
      this.UserInput.RegisterMouseActions((Action) (() => Timer.Repeat(300f, (Action<float>) (elapsedTime =>
      {
        foreach (Camera camera in this.Scene.Cameras)
          camera.Zoom += 1f / 500f * elapsedTime;
      }))), (Action) (() => Timer.Repeat(300f, (Action<float>) (elapsedTime =>
      {
        foreach (Camera camera in this.Scene.Cameras)
          camera.Zoom -= 1f / 500f * elapsedTime;
      }))));
    }

    public void MoveLeft() => this.Move(Direction.WEST);

    public void MoveRight() => this.Move(Direction.EAST);

    private void Move(Direction direction)
    {
      int num = 1;
      if (direction == Direction.WEST)
        num = -1;
      if (this.slideDirection != direction)
        this.Transform.VelocityX += (float) ((double) num * (double) this.MovementSpeed * Globals.GameTime.ElapsedGameTime.TotalSeconds) * Config.TIME_OFFSET;
      this.CurrentFaceDirection = direction;
      this.fist.ChangeDirection();
      this.MovementButtonDown = true;
    }

    public void Jump()
    {
      if (this.Ladder != null || !this.canJump && !this.canDoubleJump)
        return;
      if (this.canJump)
      {
        if (!this.isCarryingItem)
          this.canDoubleJump = true;
        this.canJump = false;
      }
      else
      {
        if (Hero.lastJump < (double) this.JUMP_RATE)
          return;
        Hero.lastJump = 0.0;
        this.canDoubleJump = false;
        this.doubleJumping = true;
      }
      this.Transform.VelocityY -= Config.JUMP_FORCE + this.jumpModifier.Y;
      this.Transform.VelocityX += this.jumpModifier.X;
      if ((double) this.jumpModifier.X < 0.0)
        this.CurrentFaceDirection = Direction.WEST;
      else if ((double) this.jumpModifier.X > 0.0)
        this.CurrentFaceDirection = Direction.EAST;
      this.jumpModifier = Vector2.Zero;
      this.FallSpeed = (float) Globals.GameTime.TotalGameTime.TotalSeconds;
      AudioEngine.Play("JumpSound");
    }

    public void ClimbUpOnLadder()
    {
      if (this.Ladder == null)
        return;
      this.Transform.VelocityY -= this.MovementSpeed * (float) Globals.GameTime.ElapsedGameTime.TotalSeconds * Config.TIME_OFFSET;
    }

    public void ClimbDownOrDescend()
    {
      if (this.Ladder == null)
      {
        if (!this.HasGravity || this.descended)
          return;
        StaticCollider collider = this.Scene.GridCollisionChecker.GetColliderAt(GridUtil.GetBelowGrid((IGameObject) this));
        if (collider == null || !collider.HasTag("Platform") || !collider.BlocksMovement)
          return;
        collider.BlocksMovement = false;
        Timer.TriggerAfter(500f, (Action) (() => collider.BlocksMovement = true));
        this.descended = true;
      }
      else if (this.IsOnGround)
        this.LeaveLadder();
      else
        this.Transform.VelocityY += this.MovementSpeed * (float) Globals.GameTime.ElapsedGameTime.TotalSeconds * Config.TIME_OFFSET;
    }

    public void ClimbDescendRelease() => this.descended = false;

    public void InteractWithItem()
    {
      if (this.isCarryingItem && this.carriedItem != null)
        this.DropCurrentItem();
      else
        this.PickupItem();
    }

    public void AttackOrThrow()
    {
      if (this.isCarryingItem)
      {
        (this.carriedItem as Entity).GetComponent<AnimationStateMachine>().Offset = this.originalAnimOffset;
        this.ThrowCurrentItem(this.CurrentFaceDirection != Direction.WEST ? new Vector2(2.5f, 0.0f) : new Vector2(-2.5f, 0.0f));
      }
      else
        this.Attack();
    }

    public void Slide()
    {
      if (this.isSliding || !this.IsOnGround || this.isCarryingItem || this.isWallSliding || this.Ladder != null)
        return;
      this.slideDirection = this.CurrentFaceDirection;
      this.canAttack = false;
      this.horizFrictBackup = this.HorizontalFriction;
      this.HorizontalFriction = 0.9f;
      if (this.CurrentFaceDirection == Direction.EAST)
        this.Transform.VelocityX = this.SLIDE_FORCE;
      else
        this.Transform.VelocityX = -this.SLIDE_FORCE;
    }

    private void PickupItem()
    {
      if (this.overlappingItem == null || this.carriedItem != null)
        return;
      Entity overlappingItem = this.overlappingItem as Entity;
      if (this.CurrentFaceDirection == Direction.WEST)
        this.Animations.PlayAnimation("PickupLeft");
      else
        this.Animations.PlayAnimation("PickupRight");
      this.carriedItem = this.overlappingItem;
      this.originalAnimOffset = overlappingItem.GetComponent<AnimationStateMachine>().Offset;
      this.isCarryingItem = true;
    }

    private void ThrowCurrentItem(Vector2 force)
    {
      if (!this.isCarryingItem || this.carriedItem == null)
        return;
      this.carriedItem.Throw((Entity) this, force);
      this.isCarryingItem = false;
      this.carriedItem = (IMovableItem) null;
    }

    private void DropCurrentItem() => this.ThrowCurrentItem(new Vector2(0.0f, -0.5f));

    private void Attack()
    {
      if (!this.canAttack)
        return;
      this.fist.Attack();
    }

    public override void FixedUpdate()
    {
      if (this.LevelEndReached && this.Ladder != null)
        this.Transform.Velocity += this.autoMovementSpeed;
      else if (this.fan != null)
      {
        this.canJump = false;
        this.canDoubleJump = false;
        this.FallSpeed = 0.0f;
        float num = MathHelper.Lerp(0.5f, 0.0f, (this.fan.Transform.Y - this.Transform.Y) / (float) this.fan.ForceFieldHeight);
        if (this.random.Next(0, 11) % 2 == 0)
          num = 0.0f;
        this.Transform.VelocityY -= num;
      }
      else
      {
        if (this.Ladder != null && !this.IsOnGround)
          this.SetupLadderMovement();
        else if (this.Ladder != null)
          this.ExitLadderMovement();
        if (!this.isSliding && this.overlappingEnemies.Count > 0 && !Timer.IsSet("Invincible"))
        {
          if (this.overlappingEnemies[0] is Spikes)
            this.Hit(this.overlappingEnemies[0], forceRightFacing: new Vector2(0.0f, -1.5f));
          else
            this.Hit(this.overlappingEnemies[0]);
        }
        if ((double) this.Transform.Y > 2000.0)
          this.Respawn();
      }
      base.FixedUpdate();
    }

    public override void Update()
    {
      if (this.Ladder == null)
      {
        if (this.HasGravity && this.IsOnGround)
        {
          this.FallSpeed = 0.0f;
          if ((double) this.Transform.VelocityY == 0.0)
          {
            this.canJump = true;
            this.canDoubleJump = false;
          }
          this.doubleJumping = false;
        }
        if ((double) this.FallSpeed > 0.0)
          Hero.lastJump += (double) Globals.ElapsedTime;
        else
          this.doubleJumping = false;
      }
      base.Update();
    }

    protected override void SetRayBlockers()
    {
      this.RayBlockerLines.Clear();
      this.RayBlockerLines.Add((new Vector2(this.Transform.X - (float) (Config.GRID / 2), this.Transform.Y - 10f), new Vector2(this.Transform.X + (float) (Config.GRID / 2), this.Transform.Y - 10f)));
      this.RayBlockerLines.Add((new Vector2(this.Transform.X, (float) ((double) this.Transform.Y - (double) (Config.GRID / 2) - 10.0)), new Vector2(this.Transform.X, (float) ((double) this.Transform.Y + (double) (Config.GRID / 2) - 10.0))));
    }

    public void EnterLadder(Ladder ladder)
    {
      this.Ladder = ladder;
      this.Transform.VelocityY = 0.0f;
      this.SetupLadderMovement();
    }

    private void SetupLadderMovement()
    {
      this.canJump = false;
      this.canDoubleJump = true;
      this.MovementSpeed = this.climbSpeed;
      this.HasGravity = false;
    }

    private void ExitLadderMovement()
    {
      if (this.Ladder != null && (double) this.Ladder.Transform.Position.Y > (double) this.Transform.Y && (double) this.Transform.VelocityY < 0.0)
        this.Transform.VelocityY -= Config.JUMP_FORCE;
      this.FallSpeed = 0.0f;
      this.HasGravity = true;
      this.MovementSpeed = Config.CHARACTER_SPEED;
      this.HorizontalFriction = Config.HORIZONTAL_FRICTION;
      this.VerticalFriction = Config.VERTICAL_FRICTION;
    }

    public void LeaveLadder()
    {
      this.ExitLadderMovement();
      this.Ladder = (Ladder) null;
    }

    private void Hit(IGameObject otherCollider, bool usePositionCheck = true, Vector2 forceRightFacing = default (Vector2))
    {
      if (Timer.IsSet("Invincible"))
        return;
      this.FallSpeed = 0.0f;
      AudioEngine.Play("HeroHurtSound");
      this.CancelVelocities();
      this.DropCurrentItem();
      this.UserInput.ControlsDisabled = true;
      TimeSpan timeSpan = TimeSpan.FromSeconds(1.0);
      Timer.SetTimer("Invincible", (float) timeSpan.TotalMilliseconds);
      timeSpan = TimeSpan.FromSeconds(0.5);
      Timer.TriggerAfter((float) timeSpan.TotalMilliseconds, (Action) (() => this.UserInput.ControlsDisabled = false));
      timeSpan = TimeSpan.FromSeconds(0.5);
      Timer.TriggerAfter((float) timeSpan.TotalMilliseconds, (Action) (() => this.canAttack = true));
      if (this.CurrentFaceDirection == Direction.WEST)
        this.Animations.PlayAnimation("HurtLeft");
      else if (this.CurrentFaceDirection == Direction.EAST)
        this.Animations.PlayAnimation("HurtRight");
      if (forceRightFacing == new Vector2())
        forceRightFacing = new Vector2(1f, -1f);
      if (usePositionCheck)
      {
        if ((double) otherCollider.Transform.X > (double) this.Transform.X)
          forceRightFacing.X *= -1f;
        this.Transform.Velocity += forceRightFacing;
      }
      else
      {
        if ((double) (otherCollider as PhysicalEntity).Transform.VelocityX == 0.0)
          forceRightFacing.X = 0.0f;
        else if ((double) (otherCollider as PhysicalEntity).Transform.VelocityX < 0.0)
          forceRightFacing.X *= -1f;
        this.Transform.Velocity += forceRightFacing;
      }
    }

    public override void OnCollisionStart(IGameObject otherCollider)
    {
      if (otherCollider.HasTag("Enemy"))
      {
        this.overlappingEnemies.Add(otherCollider);
        if (!this.isSliding)
        {
          if (otherCollider is SpikedTurtle && (otherCollider as SpikedTurtle).SpikesOut)
          {
            this.Hit(otherCollider);
            return;
          }
          float num = MathUtil.DegreeFromVectors(this.Transform.Position, otherCollider.Transform.Position);
          if ((double) this.Transform.VelocityY > 0.0 && (double) num <= 155.0 && (double) num >= 25.0 && !Timer.IsSet("Invincible"))
          {
            this.Transform.VelocityY = 0.0f;
            this.Bump(new Vector2(0.0f, -0.5f));
            this.FallSpeed = 0.0f;
            (otherCollider as AbstractEnemy).Hit(Direction.NORTH);
            Timer.SetTimer("Invincible", (float) TimeSpan.FromSeconds(0.5).TotalMilliseconds);
            this.canJump = false;
            this.canDoubleJump = true;
          }
          else
            this.Hit(otherCollider);
          if (otherCollider is Carrot)
            (otherCollider as Carrot).OverlapsWithHero = true;
        }
      }
      else
      {
        switch (otherCollider)
        {
          case Bullet _:
            if (!this.isSliding)
            {
              this.Hit(otherCollider, false);
              break;
            }
            break;
          case Coin _:
            AudioEngine.Play("CoinPickupSound");
            this.coinEffect.AddCoin(otherCollider.Transform.Position);
            (otherCollider as Coin).Die();
            break;
          case Box _ when (otherCollider as Box).Transform.Velocity == Vector2.Zero && (double) this.Transform.Y < (double) otherCollider.Transform.Y:
            AudioEngine.Play("BoxBounceSound");
            this.Transform.VelocityY = 0.0f;
            this.Bump(new Vector2(0.0f, -0.5f));
            this.FallSpeed = 0.0f;
            (otherCollider as Box).Hit(Direction.CENTER);
            break;
          case Spikes _:
            Direction direction = (otherCollider as Spikes).Direction;
            if (direction != Direction.SOUTH || direction == Direction.SOUTH && !this.isSliding)
              this.Hit(otherCollider, forceRightFacing: new Vector2(0.0f, -1.5f));
            this.overlappingEnemies.Add(otherCollider);
            break;
          case Spring _:
            if (!this.isSliding)
            {
              ((Spring) otherCollider).Bounce();
              this.Transform.VelocityY = 0.0f;
              this.Bump(new Vector2(0.0f, -2f));
              this.canJump = false;
              this.canDoubleJump = false;
              this.FallSpeed = 0.0f;
              break;
            }
            break;
          case IMovableItem _:
            this.overlappingItem = otherCollider as IMovableItem;
            break;
          case SlideWall _ when !this.IsOnGround:
            if (Timer.IsSet("IsAttacking") || this.isCarryingItem)
              return;
            this.isWallSliding = true;
            if ((double) this.Transform.VelocityY < 0.0)
              this.Transform.VelocityY = 0.0f;
            if ((double) this.GravityValue == (double) Config.GRAVITY_FORCE)
            {
              this.GravityValue /= 4f;
              this.canAttack = false;
            }
            this.canDoubleJump = true;
            if ((double) otherCollider.Transform.X < (double) this.Transform.X)
            {
              this.jumpModifier = new Vector2(1f, 0.0f);
              break;
            }
            if ((double) otherCollider.Transform.X > (double) this.Transform.X)
            {
              this.jumpModifier = new Vector2(-1f, 0.0f);
              break;
            }
            break;
          case IceCreamProjectile _:
            this.Hit(otherCollider);
            (otherCollider as IceCreamProjectile).DestroyBullet();
            break;
          case Saw _:
            this.Hit(otherCollider);
            break;
          case Fan _:
            this.LeaveFanArea();
            this.Hit(otherCollider);
            break;
          case GameFinishTrophy _:
            this.Scene.Finish();
            break;
        }
      }
      base.OnCollisionStart(otherCollider);
    }

    public override void OnCollisionEnd(IGameObject otherCollider)
    {
      if (otherCollider is IMovableItem)
        this.overlappingItem = (IMovableItem) null;
      else if (otherCollider is SlideWall)
      {
        this.isWallSliding = false;
        this.GravityValue = Config.GRAVITY_FORCE;
        this.jumpModifier = Vector2.Zero;
        this.canAttack = true;
      }
      else if (otherCollider.HasTag("Enemy"))
      {
        if (otherCollider is Carrot)
          (otherCollider as Carrot).OverlapsWithHero = false;
        this.overlappingEnemies.Remove(otherCollider);
      }
      else if (otherCollider is Spikes)
        this.overlappingEnemies.Remove(otherCollider);
      base.OnCollisionEnd(otherCollider);
    }

    private void Respawn() => this.ResetPosition(this.LastSpawnPoint);

    public override void Destroy() => base.Destroy();

    private void SetupAutoMovement()
    {
      Ladder ladder = this.Ladder;
      this.UserInput.ControlsDisabled = true;
      this.autoMovementSpeed = new Vector2(0.0f, -0.3f);
    }

    private void DisableAutoMovement()
    {
      this.UserInput.ControlsDisabled = false;
      this.autoMovementSpeed = Vector2.Zero;
    }

    public void EnterFanArea(Fan fan)
    {
      this.fan = fan;
      this.VerticalFriction = 0.0f;
      this.canJump = false;
      this.canDoubleJump = false;
      this.doubleJumping = false;
    }

    public void LeaveFanArea()
    {
      this.fan = (Fan) null;
      this.VerticalFriction = Config.VERTICAL_FRICTION;
    }
  }
}
