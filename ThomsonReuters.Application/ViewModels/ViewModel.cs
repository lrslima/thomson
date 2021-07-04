﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ThomsonReuters.Application.ViewModels
{
    public abstract class ViewModel
    {
        [JsonIgnore]
        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
