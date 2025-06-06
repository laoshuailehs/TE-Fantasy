using UnityEngine;
using UnityEngine.UI;
using TEngine;

namespace GameLogic
{
    [Window(UILayer.UI)]
    class OptionUI : UIWindow
    {
        #region 脚本工具生成的代码
        private Button _btnCreatorKit;
        private Button _btnGame;
        private Button _btnEffect;
        protected override void ScriptGenerator()
        {
            _btnCreatorKit = FindChildComponent<Button>("m_btnCreator_Kit");
            _btnGame = FindChildComponent<Button>("m_btnGame");
            _btnEffect = FindChildComponent<Button>("m_btnEffect");
            _btnCreatorKit.onClick.AddListener(OnClickCreatorKitBtn);
            _btnGame.onClick.AddListener(OnClickGameBtn);
            _btnEffect.onClick.AddListener(OnClickEffectBtn);
        }
        #endregion

        #region 事件
        private void OnClickCreatorKitBtn()
        {
            this.Close();
            GameModule.Scene.LoadSceneAsync("Game");
        }
        private void OnClickGameBtn()
        {
            this.Close();
            GameModule.Scene.LoadSceneAsync("Game");
            GameModule.UI.ShowUIAsync<HsTestUI>();
        }
        private void OnClickEffectBtn()
        {
            this.Close();
            GameModule.Scene.LoadSceneAsync("Effect");
        }
        #endregion

    }
}

