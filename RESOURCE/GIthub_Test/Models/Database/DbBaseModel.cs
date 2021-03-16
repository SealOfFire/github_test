using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Database
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class DbBaseModel
    {
        [Required]
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // TODO不要自增主键，不知道github的这个数值会不会重复
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [Column("url")]
        [MaxLength(2000)]
        public string Url { get; set; }

        [Column("html_url")]
        [MaxLength(2000)]
        public string HtmlUrl { get; set; }

        //[Column("last_update")]
        //public DateTimeOffset LastUpdate { get; set; }
        //[Column("description")]
        //[MaxLength(2000)]
        //public string Description { get; set; }

        //[Required]
        [Column("insert_date")]
        [HiddenInput]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset InsertDate { get; set; }

        //[Required]
        [Column("insert_user")]
        [HiddenInput]
        public Guid InsertUser { get; set; }

        //[Required]
        [Column("update_date")]
        [ConcurrencyCheck] // 并发冲突检查
        [HiddenInput]
        // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset UpdateDate { get; set; }

        //[Required]
        [Column("update_user")]
        [HiddenInput]
        public Guid UpdateUser { get; set; }

        //[Required]
        [Column("delete_flag")]
        [DefaultValue(false)]
        [HiddenInput]
        public bool DeleteFlag { get; set; }
    }
}
