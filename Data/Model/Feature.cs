using IdentityService.Data.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static IdentityService.Constants;

namespace IdentityService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class Feature: ILoggable
    {
        public int Id { get; set; }

		[Index("FeatureNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]
        [StringLength(MaxStringLength)]
        public string Name { get; set; }

        public string Url { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }
        
    }
}
