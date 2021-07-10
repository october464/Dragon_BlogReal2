using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Models
{
    
        //This Model is used to associate individual post with custom tags
        public class Tag
        {
            #region key
            public int Id { get; set; }
            public int PostId { get; set; }

            public string Name { get; set; }
            #endregion

            #region Navigation
            public virtual Post Post { get; set; }
            #endregion
        }
    
}
