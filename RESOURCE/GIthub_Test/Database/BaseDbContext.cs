using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace Database
{
    public class BaseDbContext : DbContext
    {
        private readonly ILogger logger;
        private readonly IHttpContextAccessor httpContextAccessor;

        public bool PhysicsDelete { get; set; } = false;

        public BaseDbContext(DbContextOptions options,
          ILogger logger,
          IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        #region SaveChanges

        public override int SaveChanges()
        {
            logger.LogTrace("------------------------ BEGIN:[BaseDbContext.SaveChanges]------------------------");
            this.CheckEntity();
            this.RecordDbChangeLog();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            logger.LogTrace("------------------------ BEGIN:[BaseDbContext.SaveChangesAsync2]------------------------");
            this.CheckEntity();
            this.RecordDbChangeLog();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            logger.LogTrace("------------------------ BEGIN:[BaseDbContext.SaveChangesAsync1]------------------------");
            // this.CheckEntity();
            // this.RecordDbChangeLog();
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 检查数据
        /// </summary>
        private void CheckEntity()
        {
            this.logger.LogTrace("------------------------ BEGIN:[BaseDbContext.CheckEntity]------------------------");
            // AutoLogger.Trace(string.Format("-- CheckEntity BEGIN --"));
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();
            // 获取登陆用户信息
            bool isAuthenticated = this.httpContextAccessor.HttpContext == null ? false : this.httpContextAccessor.HttpContext.User.Identity.IsAuthenticated; // 用户是否登陆了
            // Guid loginUserId = isAuthenticated ? Guid.Parse(this.userManager.GetUserId(this.httpContextAccessor.HttpContext.User)) : Guid.Empty; // 登陆用户id
            Guid loginUserId = Guid.Empty;

            foreach (EntityEntry entity in entries)
            {
                if (entity.Entity is DbBaseModel)
                {
                    DbBaseModel baseModel = entity.Entity as DbBaseModel;
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            this.CheckAdd(isAuthenticated, loginUserId, baseModel);
                            break;
                        case EntityState.Modified:
                            this.CheckModified(isAuthenticated, loginUserId, baseModel);
                            break;
                        case EntityState.Deleted:
                            this.CheckDeleted(isAuthenticated, loginUserId, entity, baseModel);
                            break;
                    }
                }
            }
            this.logger.LogTrace("------------------------ END:[BaseDbContext.CheckEntity]------------------------");
        }


        /// <summary>
        /// 记录数据库日志
        /// </summary>
        private void RecordDbChangeLog()
        {
            this.logger.LogTrace("------------------------ BEGIN:[BaseDbContext.RecordDbChangeLog]------------------------");
            // 比较改变的项目，保存修改日志
            // 所有改变的实体
            IEnumerable<EntityEntry> values = this.ChangeTracker.Entries();
            foreach (EntityEntry value in values)
            {
                // 没有改变的实体不做记录
                if (value.State == EntityState.Detached || value.State == EntityState.Unchanged)
                    continue;

                // 获取表名和字段名
                Type entityType = value.Entity.GetType();
                TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(entityType, typeof(TableAttribute));

                this.logger.LogInformation(string.Format("[{0}]:[修改记录 BEGIN]", entityType.Name));

                PropertyValues dbPropertyValues = value.CurrentValues;
                if (value.State == EntityState.Added)
                {
                    // 新加的数据，数据库中没有元数据
                }
                else
                {
                    // 修改或删除数据，数据库中有元数据
                    dbPropertyValues = value.GetDatabaseValues();
                }

                //// TODO add会被判断为修改
                //if (dbPropertyValues == null)
                //{
                //    dbPropertyValues = value.CurrentValues;
                //}

                foreach (Property property in dbPropertyValues.Properties)
                {
                    //int debug = 0;
                    //debug++;

                    // 获取变更状态
                    ChangedEntity changedEntity = new ChangedEntity();
                    changedEntity.State = value.State;
                    changedEntity.TableName = tableAttribute.Name;
                    changedEntity.ColumnName = property.Name;
                    changedEntity.CurrentValue = value.CurrentValues[property.Name];
                    changedEntity.DatabaseValue = dbPropertyValues[property.Name];
                    changedEntity.OriginalValue = value.OriginalValues[property.Name];
                    this.logger.LogInformation(changedEntity.ToString());
                }
                this.logger.LogInformation(string.Format("[{0}]:[修改记录 END]", entityType.Name));
            }

            this.logger.LogTrace("------------------------ END:[BaseDbContext.RecordDbChangeLog]------------------------");
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckAdd(bool isAuthenticated, Guid loginUserId, DbBaseModel model)
        {
            model.InsertDate = DateTimeOffset.Now;
            model.UpdateDate = DateTimeOffset.Now;
            // 检查当前是否有登陆信息
            if (isAuthenticated)
            {
                model.InsertUser = loginUserId;
                model.UpdateUser = loginUserId;
            }
            else
            {
                logger.LogWarning("未登录用户执行数据库添加操作");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckModified(bool isAuthenticated, Guid loginUserId, DbBaseModel model)
        {
            model.UpdateDate = DateTimeOffset.Now;
            if (isAuthenticated)
            {
                if (model.InsertUser != loginUserId)
                    throw new Exception("不能修改别人的用户数据");
                model.UpdateUser = loginUserId;
            }
            else
            {
                logger.LogWarning("未登录用户执行数据库修改操作");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckDeleted(bool isAuthenticated, Guid loginUserId, EntityEntry entityEntry, DbBaseModel model)
        {
            model.UpdateDate = DateTimeOffset.Now;
            if (isAuthenticated)
            {
                if (model.InsertUser != loginUserId)
                    throw new Exception("不能删除别人的用户数据");
                model.UpdateUser = loginUserId;
                if (!this.PhysicsDelete)
                {
                    // 逻辑删除
                    entityEntry.State = EntityState.Modified;
                    model.DeleteFlag = true;
                }
            }
            else
            {
                logger.LogWarning("未登录用户执行数据库删除操作");
            }
        }

        #endregion
    }
}
