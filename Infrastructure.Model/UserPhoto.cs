using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

namespace Infrastructure.Model
{
    /// <summary>
    /// 用户照片
    /// </summary>
   public  class UserPhoto
    {
        /// <summary>
        /// 照片ID
        /// </summary>
        public Guid UserPhotoId { get;set;}


        /// <summary>
        /// 照片数据
        /// </summary>
        public byte[] PhotoData
        {
            get; set;
        }

        //[JsonIgnore]
        [NotMapped]
        public string PhotoDataString
        {
            get
            {
                return Convert.ToBase64String(PhotoData);
            }
            set
            {
                try
                {
                    PhotoData = Convert.FromBase64String(value);
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// 修改时间
        /// </summary>
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Modified
        {
            get;set;
        }
    }
}
