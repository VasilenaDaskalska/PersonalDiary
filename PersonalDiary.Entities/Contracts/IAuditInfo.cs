﻿using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.Entities.Contracts
{
    public interface IAuditInfo
    {
        long UserCreatorID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        DateTime CreationDate { get; set; }

        long LastUserModifiedID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        DateTime LastModifiedDate { get; set; }
    }
}
