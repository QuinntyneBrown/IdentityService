using System;
using System.Collections.Generic;
using AccountService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Feature: ILoggable
    {
        public int Id { get; set; }

		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }
        
		public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }
        
    }
}
