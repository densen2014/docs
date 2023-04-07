DNS 故障临时解决方法

1. 直接解压 [hosts.zip](https://github.com/densen2014/docs/files/11178477/hosts.zip) ,替换hosts到`C:\Windows\System32\drivers\etc`

2. 手动方式,打开路径
```
C:\Windows\System32\drivers\etc
```



，里面会有一个hosts文件，右击用笔记本打开。在最下面添加如下格式 IP，空格，网址。

```
91.142.222.126 freepos.es
91.142.222.126 c.freepos.es
91.142.211.149 reg.app1.es
91.142.211.149 file2.app1.es
91.142.222.126 file.app1.es
91.142.222.126 yunii.app1.es
91.142.211.149 916423330.com
91.142.211.149 densen.es
91.142.211.149 app1.es
54.77.233.106 pos.916423330.com
91.142.211.149 sync.916423330.com
91.142.211.149 dvr.densen.es
91.142.222.126 tpv.densen.es
91.142.211.149 sync.densen.es 
91.142.222.126 hub.freepos.es
```

![image](https://user-images.githubusercontent.com/8428709/230596398-e6bc8746-ed41-46b0-bb54-72b75be657eb.png)
