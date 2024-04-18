// Type: MonoGame2D.GamePage
// Assembly: MonoGame2D, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using ForestPlatformerExample;
using MonolithEngine;

namespace MonoGame2D
{
    public sealed partial class GamePage : Page
    {
		readonly PlatformerGame _game;

		public GamePage()
        {
            this.InitializeComponent();

            // Synthez "monolith chainpanel" ;)
            var launchArguments = string.Empty;
            _game = MonoGame.Framework.XamlGame<PlatformerGame>.Create(
                launchArguments, 
                Window.Current.CoreWindow, 
                swapChainPanel);
        }
    }
}
