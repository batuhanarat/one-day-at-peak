using System;
using Game.Core.Enums;
using Game.Managers;

namespace Game.UI
{
    public class UIManager : IProvidable
    {
        public UIManager()
        {
            ServiceProvider.Register(this);
        }

        public void ShowFailScreen(Action tryAgainAction)
        {
            var failScreen=ServiceProvider.AssetLib.GetAsset<ScreenChangeUI>(AssetType.FAIL_SCREEN);
            failScreen.Show();
            failScreen.OnContinueClicked += () => {
                tryAgainAction();
                failScreen.Hide();
            };
        }

        public void ShowSuccessScreen(Action startNextLevel)
        {
            var successScreen=ServiceProvider.AssetLib.GetAsset<ScreenChangeUI>(AssetType.SUCCESS_SCREEN);
            successScreen.Show();
            successScreen.OnContinueClicked += () => { 
                startNextLevel();
                successScreen.Hide();
            };
        }

        public void ShowStartScreen() {
            var startScreen = ServiceProvider.AssetLib.GetAsset<ScreenChangeUI>(AssetType.START_SCREEN);
            startScreen.Show();
        }

    }
}