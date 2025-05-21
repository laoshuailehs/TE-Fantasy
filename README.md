# TE-Fantasy
基于TEngine框架 游戏开发
# 说明目录 
[URL在线转换](https://www.toolhelper.cn/EncodeDecode/Url?type=1)
[大小写转换](https://app.xunjiepdf.com/yinwen)
- [快速开始使用](#%E5%BF%AB%E9%80%9F%E5%BC%80%E5%A7%8B%E4%BD%BF%E7%94%A8)  
    * [打包运行](#%E6%89%93%E5%8C%85%E8%BF%90%E8%A1%8C)
    * [注意事项](#%E6%B3%A8%E6%84%8F%E4%BA%8B%E9%A1%B9)
- [导入luban](#luban)


## 快速开始使用

### 打包运行
 *   1.运行菜单 HybridCLR/Install... 安装HybridCLR，每次更新HybridCLR版本需要重新执行一次安装。
 *   2.运行菜单 HybridCLR/Define Symbols/Enable HybridCLR 运行开启HybridCLR热更新。
 *   3.运行菜单 HybridCLR/Generate/All 进行必要的生成操作。这一步不可遗漏!!!
 *   4.运行菜单 HybridCLR/Build/BuildAssets And CopyTo AssemblyPath，生成热更新dll并copy到热更程序集中。
 *   5.运行菜单 YooAsset/AssetBundle Builder 构建AB。
 *   6.打开Build Settings对话框，点击Build And Run，打包并且运行热更新示例工程。
### 注意事项
* 打包时需要打包的模式在 main 场景 ResourceDriver 下修改模式
![](./Readmesrc/1.png)  

* 修改热更新服务器地址 在TEngine 下的 settings 修改
![](./Readmesrc/2.png)

* 之后的热更新，如果修改了代码，需要进行步骤4，如果修改了ab包(资源，预制体等待)则需要进行步骤6

## LuBan
* 下载[luban](https://github.com/focus-creative-games/luban.git) 到TEngine同一目录下   

![](./Readmesrc/4.png)
* 点击Tools下的bat文件  

![](./Readmesrc/3.png)

* 新建表，添加表配置
![](./Readmesrc/5.png)

* 在TEngine下转表 会在GameProto下生成文件 注意ConfigSystem现在不会生成(原因是运行的bat不一样，需要去运行图8中的bat)  

![](./Readmesrc/6.png)
![](./Readmesrc/7.png)
![](./Readmesrc/8.png)
* 最后在GameApp中测试，测试代码如下
```c#
 ConfigSystem.Instance.Load();
        foreach (var item in ConfigSystem.Instance.Tables.Tb.DataMap)
        {
            Log.Debug(item.Value.Id + " |" + item.Value.Desc);
        }
```