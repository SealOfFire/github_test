using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    internal class ChangedEntity
    {
        public EntityState State { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public object OriginalValue { get; set; }
        public object CurrentValue { get; set; }
        public object DatabaseValue { get; set; }

        /// <summary>
        /// 检查是否改变了数据
        /// </summary>
        /// <returns></returns>
        public bool IsChanged()
        {
            if (DatabaseValue != CurrentValue)
                return true;
            return false;
        }

        public override string ToString()
        {
            return string.Format("[{2}]|[{0}].[{1}]:([{3}]|[{4}]) -> [{5}]",
                this.TableName,
                this.ColumnName,
                Enum.GetName(typeof(EntityState), this.State),
                this.DatabaseValue,
                this.OriginalValue,
                this.CurrentValue);
        }
    }
}
