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
;默认打开的url,有2种模式，
;1.在线，直接写入url，例https://www.baidu.com
;2.本地，写入本地网页的根目录（绝对路径）,例E:\www，另外默认文档必须是index.html
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
### 历史更新
* 1.1
增加打开本地html文件功能，与在服务器端访问效果一致
修改加载本地html时的判断逻辑
增加F12调试功能
* 1.0
编写完成并项目使用