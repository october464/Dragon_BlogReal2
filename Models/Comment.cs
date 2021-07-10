using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Models
{
    //This model is attached to an individual post
    public class Comment
    {
        //If there is an Id at the end it is a forign key if its jsut Id then that is the primary key
        #region Key
        public int Id { get; set; }
        public int PostId { get; set; }
        public string BlogUserId { get; set; }//Is who wrote a comment
        #endregion

        #region Comment Properties
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        #endregion

        #region Navigation

        public virtual Post Post { get; set; }
        public virtual BlogUser BlogUser { get; set; }
        #endregion
    }
}
