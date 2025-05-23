using System;
using System.Collections;
using System.Collections.Generic;
using GameConfig.ybtest;
using TEngine;
using UnityEngine;

namespace GameLogic
{
    public class HsTest : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {
            GameEvent.AddEventListener(ILoginUI_Event.ShowLoginUI, OnShowLoginUI);
        }

        private void OnDestroy()
        {
            GameEvent.RemoveEventListener(ILoginUI_Event.ShowLoginUI, OnShowLoginUI);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                GameEvent.Get<ILoginUI>().ShowLoginUI();
            }
        }
        
        void OnShowLoginUI()
        {
            Debug.Log("OnShowLoginUI");
            GameModule.UI.ShowUI<LoginUI>();
        }
        
    }
}
