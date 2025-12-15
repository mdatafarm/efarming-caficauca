using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using System.Web.Http.Validation;

namespace EFarming.Integration.Models
{
    public class CustomBodyModelValidator : DefaultBodyModelValidator
    {
        public override bool ShouldValidateType(Type type)
        {
            return type != typeof(DbGeography) && base.ShouldValidateType(type);
        }
    }
}