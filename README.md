# github_test
github测试题
###  2021/3/22 更新内容2  
数据库表删除了除了主键和name意外的字段  
更新数据库的sql文本：  \DATABASE\20210322_update_ddl.sql  
entity framework对应数据库的代码也修改了。  

###  2021/3/22 更新内容1  
对2021/3/15更新进行了修正。增加了开发工具说明，日志说明，启动说明  
开发工具： visual studio community 2019 
日志说明：使用nlog记录日志。日志会生成到执行程序目录下的log文件夹。

启动说明：  
1.在visual studio community 2019中选择菜单栏的“文件(F)”->“打开(O)”->“项目/解决方案(P)”。弹出窗选择“RESOURCE\GIthub_Test\GIthub_Test\GIthub_Test.sln”。  
2.打开项目后，初次启动时取消RESOURCE\GIthub_Test\GIthub_Test\Projram.cs文件中59行的注释。59行的dbContext.Database.EnsureCreated();会根据entity framework设置自动创建数据库。初次启动时创建数据库，之后再启动前注释或删除此行代码。

### 2021/3/17 更新内容  
完成  

### 2021/3/16 更新内容  
数据库修改：修改数据库结构新增了字段。对应文件\DATABASE\20210306_update_ddl.sql  
源代码修改：
    1.创建项目。对应路径 \RESOURCE\GIthub_Test  
    2.页面的静态功能完成。  
源代码结构说明：  
    Database类库：处理数据库操作业务。  
    Models类库：\Models\Database，数据库持久化使用得类；Models\JSON，调用webapi是序列化数据使用的类  
    Manager类库：业务逻辑操作。  
    GIthub_Test：网站。

### 2021/3/15 更新内容<br/>
##### 路径说明：
DATABASE：路径下保存数据库文件  
    ├ ddl.sql：创建数据库的sql文  
    ├ 20210306_update_ddl.sql：数据库结构修改
    └ 20210322_update_ddl.sql：第二次数据库结构修改
RESOURCE：路径下保存源码文件  
#### 环境说明：
开发工具： visual studio community 2019 
数据库说明：sql server express 13.0 
日志说明：使用nlog记录日志。日志会生成到执行程序目录下的log文件夹。

启动说明：  
1.在visual studio community 2019中选择菜单栏的“文件(F)”->“打开(O)”->“项目/解决方案(P)”。弹出窗选择“RESOURCE\GIthub_Test\GIthub_Test\GIthub_Test.sln”。  
2.打开项目后，初次启动时取消RESOURCE\GIthub_Test\GIthub_Test\Projram.cs文件中59行的注释。59行的`dbContext.Database.EnsureCreated();`会根据entity framework设置自动创建数据库。初次启动时创建数据库，之后再启动前注释或删除此行代码。
