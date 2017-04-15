using System;
using AccountService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace AccountService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Service: ILoggable
    {
        public int Id { get; set; }
        
		[Index("NameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }        
    }
}
