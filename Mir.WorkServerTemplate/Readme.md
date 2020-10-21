## 微服务Mir.WorkServer应用框架
---
## 各组件使用
```csharp
        Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(config);//注入配置文件(必须)
                    services.AddSqlSugar();//SqlSugar
                    services.AddRedis();//Redis
                    //默认主任务
                    services.AddHostedService<MainWorker>();//可以在这里添加另外的工作服务
                })
                .ConfigureLogging(builder => builder.AddFile());//日志写文件
        }


        //使用
        private readonly ILogger<MainWorker> _logger;
        private readonly ISqlSugarDbContextFactory _sugar;
        private readonly RedisDbContext _redis;

        public MainWorker(ILogger<MainWorker> logger, ISqlSugarDbContextFactory sugar, RedisDbContext db)
        {
            _logger = logger;
            _sugar = sugar;
            _redis = db;
        }
```