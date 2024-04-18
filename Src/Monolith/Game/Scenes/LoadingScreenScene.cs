// Decompiled with JetBrains decompiler
// Type: ForestPlatformerExample.LoadingScreenScene
// Assembly: PlatformerNetStandard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86D25325-3782-43C4-93B7-88CDEF6FED82
// Assembly location: C:\Users\Admin\Desktop\RE\PlatformerDemo\PlatformerNetStandard.dll

using Microsoft.Xna.Framework;
using MonolithEngine;
using System.Collections.Generic;


namespace ForestPlatformerExample
{
  internal class LoadingScreenScene : AbstractScene
  {
    public LoadingScreenScene()
      : base("loading", true)
    {
    }

    public override ICollection<object> ExportData() => (ICollection<object>) null;

    public override ISceneTransitionEffect GetTransitionEffect() => (ISceneTransitionEffect) null;

    public override void ImportData(ICollection<object> state)
    {
    }

    public override void Load()
    {
      this.UI.AddUIElement((IUIElement) new Image(Assets.GetTexture2D("HUDLoading"), new Vector2(200f, 200f)));
    }

    public override void OnEnd()
    {
    }

    public override void OnStart()
    {
    }

    public override void OnFinished()
    {
    }
  }
}
