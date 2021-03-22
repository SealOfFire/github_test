using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Database;

namespace Database
{
    /// <summary>
    /// 数据库检索上下文
    /// </summary>
    public class GithubTestDbContext : DbContext
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<GithubTestDbContext> logger;

        #region 数据库 models

        /// <summary>
        /// 右部分列表
        /// </summary>
        public virtual DbSet<Right> Rights { get; set; }

        /// <summary>
        /// 做部分列表
        /// </summary>
        public virtual DbSet<Left> Lefts { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public GithubTestDbContext(
            DbContextOptions<GithubTestDbContext> options,
            ILogger<GithubTestDbContext> logger,
            IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.logger = logger;
            this.logger.LogInformation("初始化 数据库");
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region tbl_left
            modelBuilder.Entity<Left>(left =>
            {
                // PK
                left.HasKey(r => r.Id);

                // FK

                // 全局查询筛选器
                //left.HasQueryFilter(l => !l.DeleteFlag);
            });
            #endregion

            #region tbl_right
            modelBuilder.Entity<Right>(right =>
            {
                // PK
                right.HasKey(r => r.Id);

                // FK

                // 全局查询筛选器
                //right.HasQueryFilter(r => !r.DeleteFlag);
            });
            #endregion
        }

    }
}
