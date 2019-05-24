# ToyChromium
### 说明

本工具使用CefSharp.WinForms稳定版73.1.130插件，实现简单访问功能，去除浏览器中的各种友好提示。实现沉浸式访问

需要最低framework4.5.2和Microsoft Visual C++ 2015-2019 Redistribtable (x86)的支持

Visual Studio 2017开发

### 配置文件说明

```ini
[app]
;是否全屏
fullscreen=0
;是否屏蔽右键
disablemouseright=1
;默认打开的url
url=www.baidu.com
;udp服务端监听端口号
udpport=10010
;执行脚本文件名
script=script.js
[command]
;下面是命令列表，例，发送cmd1执行functiona()方法。如没有发送socket命令，则忽略
cmd1=functiona()
cmd2=functionb()
```