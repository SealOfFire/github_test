using Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace GIthub_Test.Controllers
{
    public class BaseController : Controller
    {
        #region

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger<BaseController> logger;

        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected GithubTestDbContext dbContext;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="dbContext">数据库上下文</param>
        /// <param name="dataDictionaryManager">数据字典管理</param>
        public BaseController(
            ILogger<BaseController> logger,
            GithubTestDbContext dbContext)
        {
            this.logger = logger;
            this.dbContext = dbContext;
        }

        #endregion
    }
}
