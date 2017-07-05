using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class NotBadWords : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string Text = value as string;

            if (Text.Contains("Нехороший человек - редиска")|| Text.Contains("Bad Word"))
            {
                return false;
            }
            return true;
        }
    }
}
