// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.EntityParser
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace ForestPlatformerExample
{
  internal class EntityParser
  {
    private LDTKMap world;
    private Hero hero;

    public EntityParser(LDTKMap world) => this.world = world;

    public void LoadEntities(AbstractScene scene, string levelID)
    {
      Vector2 zero = Vector2.Zero;
      List<(Vector2, int, int)> valueTupleList = new List<(Vector2, int, int)>();
      foreach (EntityInstance entityInstance in this.world.ParseLevel(scene))
      {
        Logger.Debug("Parsing entity: " + entityInstance.Identifier);
        Vector2 position1 = new Vector2((float) entityInstance.Px[0], (float) entityInstance.Px[1]);
        Vector2 pivot = new Vector2((float) entityInstance.Pivot[0], (float) entityInstance.Pivot[1]);
        if (entityInstance.Identifier.Equals("Hero"))
        {
          Vector2 position2 = position1;
          this.hero = new Hero(scene, position2);
        }
        else if (entityInstance.Identifier.Equals("Coin"))
        {
          bool flag = true;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "hasGravity")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              flag = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__0, fieldInstance.Value);
            }
          }
          new Coin(scene, position1).HasGravity = flag;
        }
        else if (entityInstance.Identifier.Equals("MovingPlatform"))
          valueTupleList.Add((position1, (int) entityInstance.Width, (int) entityInstance.Height));
        else if (entityInstance.Identifier.Equals("Spring"))
        {
          int power = -1;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "power")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              power = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__1.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__1, fieldInstance.Value);
            }
          }
          Spring spring = new Spring(scene, position1, power);
        }
        else if (entityInstance.Identifier.Equals("Box"))
        {
          Box box = new Box(scene, position1);
        }
        else if (entityInstance.Identifier.Equals("Ladder"))
        {
          Ladder ladder = new Ladder(scene, position1, (int) entityInstance.Width, (int) entityInstance.Height);
        }
        else if (entityInstance.Identifier.Equals("MovingPlatformTurn"))
        {
          Direction turnDirection = Direction.CENTER;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "Direction")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, Direction>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (Direction), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, Direction> target = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__3.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, Direction>> p3 = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__3;
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (EntityParser), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__2, typeof (Enum), typeof (Direction), fieldInstance.Value);
              turnDirection = target((CallSite) p3, obj);
            }
            MovingPlatformTurner movingPlatformTurner = new MovingPlatformTurner(scene, position1, turnDirection);
          }
        }
        else if (entityInstance.Identifier.Equals("SlideWall"))
        {
          SlideWall slideWall = new SlideWall(scene, position1, (int) entityInstance.Width, (int) entityInstance.Height);
        }
        else if (entityInstance.Identifier.Equals("Spikes"))
        {
          Direction direction = Direction.CENTER;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "Direction")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, Direction>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (Direction), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, Direction> target = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__5.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, Direction>> p5 = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__5;
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, Type, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (EntityParser), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__4.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__4, typeof (Enum), typeof (Direction), fieldInstance.Value);
              direction = target((CallSite) p5, obj);
            }
          }
          float length = entityInstance.Width > entityInstance.Height ? (float) entityInstance.Width : (float) entityInstance.Height;
          Spikes spikes = new Spikes(scene, position1, (int) length, direction);
        }
        else if (entityInstance.Identifier.Equals("RespawnPoint"))
        {
          RespawnPoint respawnPoint = new RespawnPoint(scene, 256, 256, position1);
        }
        else if (entityInstance.Identifier.Equals("IceTrigger"))
        {
          IceTrigger iceTrigger = new IceTrigger(scene, (int) entityInstance.Width, (int) entityInstance.Height, position1);
        }
        else if (entityInstance.Identifier.Equals("NextLevelTrigger"))
        {
          NextLevelTrigger nextLevelTrigger = new NextLevelTrigger(scene, position1, (int) entityInstance.Width, (int) entityInstance.Height);
        }
        else if (entityInstance.Identifier.Equals("EnemyTrunk"))
        {
          Trunk trunk = new Trunk(scene, position1, Direction.WEST);
        }
        else if (entityInstance.Identifier.Equals("EnemySpikedTurtle"))
        {
          SpikedTurtle spikedTurtle = new SpikedTurtle(scene, position1, Direction.WEST);
        }
        else if (entityInstance.Identifier.Equals("EnemyCarrot"))
        {
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "speed")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__6 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              int num = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__6.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__6, fieldInstance.Value);
            }
          }
          Carrot carrot = new Carrot(scene, position1, Direction.EAST);
        }
        else if (entityInstance.Identifier.Equals("EnemyIceCream"))
        {
          IceCream iceCream = new IceCream(scene, position1);
        }
        else if (entityInstance.Identifier.Equals("Saw"))
        {
          bool horizontalMovement = true;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "HorizontalMovement")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__7 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              horizontalMovement = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__7.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__7, fieldInstance.Value);
            }
          }
          Saw saw = new Saw(scene, position1, horizontalMovement, pivot);
        }
        else if (entityInstance.Identifier.Equals("SawPath"))
        {
          SawPath sawPath = new SawPath(scene, position1, (int) entityInstance.Width, (int) entityInstance.Height);
        }
        else if (entityInstance.Identifier.Equals("Fan"))
        {
          int forceFeildHeight = -1;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "forceFeildHeight")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__8 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (int), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              forceFeildHeight = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__8.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__8, fieldInstance.Value);
            }
          }
          Fan fan = new Fan(scene, position1, forceFeildHeight);
        }
        else if (entityInstance.Identifier.Equals("EnemyRock"))
        {
          RockSize size = RockSize.BIG;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "RockSize")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__10 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, RockSize>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (RockSize), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              Func<CallSite, object, RockSize> target = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__10.Target;
              // ISSUE: reference to a compiler-generated field
              CallSite<Func<CallSite, object, RockSize>> p10 = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__10;
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__9 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__9 = CallSite<Func<CallSite, Type, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Parse", (IEnumerable<Type>) null, typeof (EntityParser), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
                {
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
                  CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
                }));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              object obj = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__9.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__9, typeof (Enum), typeof (RockSize), fieldInstance.Value);
              size = target((CallSite) p10, obj);
            }
          }
          Rock rock = new Rock(scene, position1, size);
        }
        else if (entityInstance.Identifier.Equals("EnemyGhost"))
        {
          Ghost ghost = new Ghost(scene, position1);
        }
        else if (entityInstance.Identifier.Equals("GameFinishedTrophy"))
        {
          GameFinishTrophy gameFinishTrophy = new GameFinishTrophy(scene, position1, pivot);
        }
        else if (entityInstance.Identifier.Equals("PopupTextTrigger"))
        {
          string textName = (string) null;
          foreach (FieldInstance fieldInstance in entityInstance.FieldInstances)
          {
            if (fieldInstance.Identifier == "TextName")
            {
              // ISSUE: reference to a compiler-generated field
              if (EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__11 == null)
              {
                // ISSUE: reference to a compiler-generated field
                EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (EntityParser)));
              }
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              textName = EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__11.Target((CallSite) EntityParser.\u003C\u003Eo__3.\u003C\u003Ep__11, fieldInstance.Value);
            }
          }
          PopupTrigger popupTrigger = new PopupTrigger(scene, position1, (int) entityInstance.Width, (int) entityInstance.Height, textName);
        }
      }
      foreach ((Vector2, int, int) valueTuple in valueTupleList)
      {
        MovingPlatform movingPlatform = new MovingPlatform(scene, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3);
      }
    }

    public void LoadIntGrid(AbstractScene scene)
    {
      foreach (KeyValuePair<Vector2, int> keyValuePair in this.world.GetIntGrid(scene, "Colliders"))
      {
        Vector2 gridPosition = new Vector2(keyValuePair.Key.X, keyValuePair.Key.Y);
        StaticCollider staticCollider = new StaticCollider(scene, gridPosition);
        switch (keyValuePair.Value)
        {
          case 1:
            staticCollider.AddTag("Collider");
            continue;
          case 2:
            staticCollider.AddTag("SlideWall");
            continue;
          case 5:
            staticCollider.AddTag("Platform");
            staticCollider.AddBlockedDirection(Direction.WEST);
            continue;
          case 6:
            staticCollider.AddTag("Platform");
            staticCollider.AddBlockedDirection(Direction.EAST);
            continue;
          case 7:
            staticCollider.AddTag("Platform");
            staticCollider.AddBlockedDirection(Direction.NORTH);
            continue;
          case 8:
            staticCollider.AddTag("Platform");
            staticCollider.AddBlockedDirection(Direction.SOUTH);
            continue;
          default:
            continue;
        }
      }
    }

    public Hero GetHero() => this.hero;
  }
}
