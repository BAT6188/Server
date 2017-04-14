using Infrastructure.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resources.Model
{
    /// <summary>
    /// 武器装备
    /// </summary>
    public class Materiel
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid MaterielId
        {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string MaterielCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MaterielName { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [Column("Unit")]
        public SystemOption Unit
        {
            get;set;
        }

    }
}
