using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class UserCredentials
    {
        /// <summary>
        /// UserName
        /// </summary>
        
        
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// ConfirmPassword
        /// </summary>
        [Required]
        public string ConfirmPassword { get; set; }

    }
}