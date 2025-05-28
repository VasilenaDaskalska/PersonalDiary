﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalDiary.Entities.Contracts;

namespace PersonalDiary.Entities.Base
{
    public abstract class AuditInfo : BaseEntity, IAuditInfo
    {
        public virtual User UserCreator { get; set; }

        [ForeignKey(nameof(UserCreator))]
        public long UserCreatorID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public virtual User LastUserModified { get; set; }

        [ForeignKey(nameof(LastUserModified))]
        public long LastUserModifiedID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }
    }
}
