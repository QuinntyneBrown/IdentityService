using System;
using System.Collections.Generic;
using IdentityService.Data.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Model
{
    [SoftDelete("IsDeleted")]
    public class Subscription: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Account")]
        public int? AccountId { get; set; }

        [ForeignKey("Feature")]
        public int? FeatureId { get; set; }

        [Index("SubscriptionNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]        
		public string Name { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime ExpiresOn { get; set; }

        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Feature Feature { get; set; }

        public virtual Account Account { get; set; }
    }
}
