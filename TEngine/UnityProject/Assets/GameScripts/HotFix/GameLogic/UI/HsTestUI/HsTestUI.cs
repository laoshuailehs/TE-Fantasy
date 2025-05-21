using UnityEngine;
using UnityEngine.UI;
using TEngine;
// using TMPro;

namespace GameLogic
{
    [Window(UILayer.Top)]
    class HsTestUI : UIWindow
    {
        #region 脚本工具生成的代码
        private Image _img1;
        // private TMP_Text _tmp1;
        private Text _text1;
        private Button _btn1;
        private Button _btn2;
        private bool isHide;
        protected override void ScriptGenerator()
        {
            _img1 = FindChildComponent<Image>("m_img1");
            // _tmp1 = FindChildComponent<TMP_Text>("m_tmp1");
            _text1 = FindChildComponent<Text>("m_text1");
            _btn1 = FindChildComponent<Button>("m_btn1");
            _btn2 = FindChildComponent<Button>("m_btn2");
            _btn1.onClick.AddListener(OnClick1Btn);
            _btn2.onClick.AddListener(OnClick2Btn);
        }
        #endregion

        protected override void OnUpdate()
        {
            base.OnUpdate();
            _hasOverrideUpdate  = true;
            if (isHide)
            {
                _text1.text = "隐藏";
                
            }
            else
            {
                _text1.text = "显示";
            }
            
        }

        #region 事件

        protected override void RegisterEvent()
        {
            base.RegisterEvent();
            AddUIEvent(1,Btn1);
            AddUIEvent(2,Btn2);
        }

        private void OnClick1Btn()
        {
           GameEvent.Send(1);
        }
        private void OnClick2Btn()
        {
            GameEvent.Send(2);
        }

        private void Btn1()
        {
            Log.Debug("Btn1");
            GameModule.UI.ShowUIAsync<BattleMainUI>();
            GameModule.Scene.LoadScene("hs");
            isHide = false;
        }

        private void Btn2()
        {
            Log.Debug("Btn2");
            GameModule.UI.HideUI<BattleMainUI>();
            isHide = true;
        }
        
        #endregion

    }
}