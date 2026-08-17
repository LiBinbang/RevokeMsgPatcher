
<p align="center">
	<a><img width="100px" src="https://raw.githubusercontent.com/huiyadanli/RevokeMsgPatcher/master/Images/logo.png"/></a>
</p>
<p align="center">
	<a href="https://www.microsoft.com/download/details.aspx?id=30653">
		<img src="https://img.shields.io/badge/platform-windows-lightgrey.svg?style=flat-square"/>
	</a>
	<a href="https://github.com/huiyadanli/RevokeMsgPatcher/releases">
		<img src="https://img.shields.io/github/downloads/huiyadanli/RevokeMsgPatcher/total.svg?style=flat-square"/>
	</a>
	<a href="https://ci.appveyor.com/project/huiyadanli/RevokeMsgPatcher">
		<img src="https://img.shields.io/appveyor/ci/huiyadanli/RevokeMsgPatcher.svg?style=flat-square"/>
	</a>
</p>

# 👀微信/QQ/TIM防撤回补丁
适用于 Windows 下 PC 版微信/QQ/TIM的防撤回补丁。**支持微信/QQ/TIM**，其中微信能够选择安装多开功能。

<img width="180px" src="https://raw.githubusercontent.com/huiyadanli/RevokeMsgPatcher/master/Images/revoke.jpg"/>

下载地址：
**[⚡️点我下载最新版本](https://github.com/huiyadanli/RevokeMsgPatcher/releases/download/2.1/RevokeMsgPatcher.v2.1.zip)** |
[☁备用下载-蓝奏云](https://wwmy.lanzouq.com/b0fot7dpe)  密码:coco| 
[☁备用下载-百度云](https://pan.baidu.com/s/15ilr78t8F1-VW8eUZSkr_Q?pwd=3rrj) 

相关文档：
**[✔支持哪些版本](https://github.com/huiyadanli/RevokeMsgPatcher/wiki/%E7%89%88%E6%9C%AC%E6%94%AF%E6%8C%81)** | 
[❓常见问题](https://github.com/huiyadanli/RevokeMsgPatcher/wiki#%E5%B8%B8%E8%A7%81%E9%97%AE%E9%A2%98) | 
[📖查看完整文档](https://github.com/huiyadanli/RevokeMsgPatcher/wiki)

原理与方法：
[📗微信](https://github.com/huiyadanli/RevokeMsgPatcher/wiki/%E5%BE%AE%E4%BF%A1%E9%98%B2%E6%92%A4%E5%9B%9E%E4%B8%8E%E5%A4%9A%E5%BC%80%E6%95%99%E7%A8%8B) |
[📕QQ](https://github.com/huiyadanli/RevokeMsgPatcher/wiki/QQ%E6%88%96TIM%E9%98%B2%E6%92%A4%E5%9B%9E%E6%95%99%E7%A8%8B) |
[📘TIM](https://github.com/huiyadanli/RevokeMsgPatcher/wiki/QQ%E6%88%96TIM%E9%98%B2%E6%92%A4%E5%9B%9E%E6%95%99%E7%A8%8B)
**（本人不参与方法寻找，仅做特征搬运）**

附带产物：[一个通用的微信多开工具](https://github.com/huiyadanli/RevokeMsgPatcher/tree/master/RevokeMsgPatcher.MultiInstance)

## 📷截图
![Screenshot](https://raw.githubusercontent.com/huiyadanli/RevokeMsgPatcher/master/Images/screenshot.png)

## 🔨使用方法

1. 首先，你的系统需要满足以下条件：

    * Windows 7 或更高版本，**不支持XP**。
    * [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) 或更高版本。**低于此版本在打开程序时可能无反应，或者直接报错**。

2. 使用本程序前，先关闭微信/QQ/TIM。

3. **以管理员身份运行本程序**，等待右下角获取最新的补丁信息。

4. 选择微信/QQ/TIM的安装路径。如果你用的安装版的微信/QQ/TIM，正常情况下本程序会自动从注册表中获取安装路径，绿色版需要手动选择路径。

5. 点击防撤回。界面可能会出现一段时间的无响应，请耐心等待。**由于修改了微信的 WeChatWin.dll 文件、QQ/TIM的 IM.dll 文件，杀毒软件可能会弹出警告，放行即可。**

注意：微信/QQ/TIM更新之后要重新安装补丁！

### 微信 4.1.12.x 推荐用法（多开 + 带提示防撤回）

`4.1.12.x`（含 `4.1.12.55`）建议把两件事拆开：

| 能力 | 用什么 | 说明 |
| --- | --- | --- |
| 多开 | 本工具 **DLL 补丁（只勾「多开」）** | 需改掉两处 `0xB7` 互斥判断；旧特征只改 1 处会表现为装了多开仍不能开第二个 |
| 带提示防撤回 | **RevokeHook**（菜单集成） | 运行时注入；不要再勾/安装本工具的 DLL「防撤回」，避免两套路子叠用 |

#### A. 只装多开（DLL）

1. **完全退出微信**（托盘也退出）。
2. 管理员运行本程序 → 选中 **微信（Weixin）**。
3. 若曾打过补丁，先点 **备份还原**。
4. 功能里 **只勾「多开」**，不要勾「防撤回」→ **安装补丁**。
5. 先开一个微信，再开第二次 `Weixin.exe`（或开始菜单再启一次）验证多开。

说明：通用多开工具已支持进程名 `Weixin`（微信 4.x）。若已成功打上 DLL 多开补丁，一般不必再开该工具。

#### B. 再用 RevokeHook 做带提示防撤回

微信 4.x 的 DLL 十六进制「防撤回」多为拦住删除、保留原文，灰字提示常单独一行，多了不易对上气泡。需要提示时用 Hook。

1. 确认已按 **A** 只装好多开，且 **没有** 安装 DLL「防撤回」。
2. 菜单 **高级 → RevokeHook 带提示防撤回**。
3. 「微信根目录」填含 `Weixin.exe` 的目录（如 `C:\Program Files\Tencent\Weixin`，不是 `4.1.12.55` 子目录）。
4. GitHub 代理可选；`ghproxy.cn` 等对 release zip 可能 405，失败会自动换直连/其它代理，也可代理留空后重试。
5. **下载/更新** → **搜索偏移** → RevokeHookUI 里点「开始搜索」→ **保存配置**（版本应匹配当前如 `4.1.12.55`）。
6. **全部退出微信** → **注入启动**（登录第一个号）。
7. **多开第二个号时：** 再开一个微信后，再运行一次注入（再点「注入启动」或再开 `RevokeInject`）。每个微信进程都要注入，否则未注入的窗口看起来像「防撤回没提示」。
8. 自己撤回也要提示时：在 RevokeHookUI 勾选「防撤回自己的消息」→ 保存 → 重新注入。
9. 远程撤回提示有时需重新进入聊天窗口才刷新。

项目与反馈：[EEEEhex/RevokeHook](https://github.com/EEEEhex/RevokeHook) · [Discussions](https://github.com/EEEEhex/RevokeHook/discussions/12)

### 本分支改动摘要

* 目标框架升级为 **.NET Framework 4.8**。
* 适配微信 **4.1.12.55** 无提示防撤回特征（结构偏移 `0x200` → `0x248`）。
* **多开特征修正：** `4.1.12.x` 使用短特征 `81 FF B7 00 00 00 0F 85` → `90 E9`，一次命中两处互斥判断。
* 集成 **RevokeHook** 下载/启动入口；清单改为 `asInvoker`，启动时 UAC 提权（兼容 ClickOnce 发布）。
* 通用多开工具识别 **`WeChat` + `Weixin`** 进程名。
* 调试/安装补丁需管理员权限（写入 `Program Files` 下微信目录）。
* 本地目录 `publish/`、`RevokeHook/` 已加入 `.gitignore`。

## 💡致谢

本项目早期内容源自 [wechat_anti_revoke](https://github.com/36huo/wechat_anti_revoke) 项目。

2.0 之前版本 QQNT 防撤回依赖于 [LiteLoaderQQNT](https://github.com/LiteLoaderQQNT/LiteLoaderQQNT)，修补依赖于 [DLLHijackMethod](https://github.com/LiteLoaderQQNT/QQNTFileVerifyPatch/tree/DLLHijackMethod) 并集成了以下插件：

* [插件列表查看 LL-plugin-list-viewer](https://github.com/ltxhhz/LL-plugin-list-viewer)
* [防撤回 LiteLoaderQQNT-Anti-Recall](https://github.com/xh321/LiteLoaderQQNT-Anti-Recall)

2.1 版本的 QQNTT 防撤回特征来自 [NTQQAntiRecall]( https://github.com/NapNeko/NTQQAntiRecall)

微信4.0版本后的防撤回特征来自于 [BetterWX](https://github.com/zetaloop/BetterWX)

微信 4.x「带提示」防撤回集成自 [RevokeHook](https://github.com/EEEEhex/RevokeHook)（EEEEhex）

## ❤️投喂

觉的好用的话，可以支持作者哟ヾ(･ω･`｡) 
* [⚡爱发电](https://afdian.com/@huiyadanli)
* [🍚微信赞赏](https://github.com/huiyadanli/huiyadanli/blob/master/DONATE.md)

## 📄License
[GPLv3](https://github.com/huiyadanli/RevokeMsgPatcher/blob/master/LICENSE)

![](https://raw.githubusercontent.com/huiyadanli/RevokeMsgPatcher/master/Images/give_a_star.png)
