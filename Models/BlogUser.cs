using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Dragon_BlogReal.Models
{
    public class BlogUser : IdentityUser
    {
        #region MyRegion
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        #endregion

        #region MyRegion
        [Required]//I will stop before I even hit the controller if I dont have these fields 
        [StringLength(50)]//I will check for the total length of the string so this is not fool proof

        public string LastName { get; set; }
        public string DisplayName { get; set; }
        #endregion
        public virtual ICollection<Comment> Comments { get; set; } /*= new List<Comment>();*/

        public BlogUser()
        {
            Comments = new HashSet<Comment>();
            DisplayName = "New User";
        }
    }
}
