
// Type: ForestPlatformerExample.MobileButtonPanel
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class MobileButtonPanel
  {
    private List<SelectableImage> buttons = new List<SelectableImage>();

    public MobileButtonPanel(Hero hero)
    {
      SelectableImage selectableImage1 = new SelectableImage(Assets.GetTexture2D("LeftArrow"), 
          position: new Vector2(25f, 550f), scale: 9f, fireOnHold: true);

      selectableImage1.OnClick = this.AddButtonAction(hero, new Action(hero.MoveLeft));
      selectableImage1.OnRelease += (Action) (() => hero.MovementButtonDown = false);

      this.buttons.Add(selectableImage1);

      this.buttons.Add(new SelectableImage(Assets.GetTexture2D("DownArrow"), 
          position: new Vector2(265f, 550f), scale: 9f, fireOnHold: true)
      {
        OnClick = this.AddButtonAction(hero, new Action(hero.ClimbDownOrDescend)),
        OnRelease = this.AddButtonAction(hero, new Action(hero.ClimbDescendRelease))
      });

      this.buttons.Add(new SelectableImage(Assets.GetTexture2D("UpArrow"), 
          position: new Vector2(265f, 450f), scale: 9f, fireOnHold: true)
      {
        OnClick = this.AddButtonAction(hero, new Action(hero.ClimbUpOnLadder))
      });

      SelectableImage selectableImage2 = new SelectableImage(Assets.GetTexture2D("RightArrow"), 
          position: new Vector2(150f, 550f), scale: 9f, fireOnHold: true);

      selectableImage2.OnClick = this.AddButtonAction(hero, new Action(hero.MoveRight));
      selectableImage2.OnRelease += (Action) (() => hero.MovementButtonDown = false);

      this.buttons.Add(selectableImage2);

      this.buttons.Add(new SelectableImage(Assets.GetTexture2D("SquareButton"), 
          position: new Vector2(1050f, 550f), scale: 9f)
      {
        OnClick = this.AddButtonAction(hero, new Action(hero.AttackOrThrow)),
        //OnRelease = this.AddButtonAction(hero, new Action(hero.AttackOrThrowRelease)),
      });

      this.buttons.Add(new SelectableImage(Assets.GetTexture2D("XButton"), 
          position: new Vector2(1200f, 550f), scale: 9f)
      {
        OnClick = this.AddButtonAction(hero, new Action(hero.Jump)),
        //OnRelease = this.AddButtonAction(hero, new Action(hero.JumpRelease)),
      });

      this.buttons.Add(new SelectableImage(Assets.GetTexture2D("TriangleButton"), 
          position: new Vector2(1125f, 450f), scale: 9f)
      {
        OnClick = this.AddButtonAction(hero, new Action(hero.InteractWithItem))
      });

      this.buttons.Add(new SelectableImage(Assets.GetTexture2D("CircleButton"), 
          position: new Vector2(1275f, 450f), scale: 9f)
      {
        OnClick = this.AddButtonAction(hero, new Action(hero.Slide))
      });
    }

    private Action AddButtonAction(Hero hero, Action action)
    {
      return (Action) (() =>
      {
        if (hero.GetComponent<UserInputController>().ControlsDisabled)
        {
              //RnD
              return;
        }

        action();
      });
    }

    public List<SelectableImage> GetButtons()
    {
      return new List<SelectableImage>((IEnumerable<SelectableImage>) this.buttons);
    }
  }
}
