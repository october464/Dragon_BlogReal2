using Dragon_BlogReal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Utilities
{
    public class ImageHelper
    {
        
        public static string GetImage(Post post)
        {
            if(post != null)
            {
                if(post.Image != null)
                {
                    var binary = Convert.ToBase64String(post.Image);
                    var ext = Path.GetExtension(post.FileName);
                    string imageDataURL = $"data:image/{ext};base64,{binary}";
                    return imageDataURL;
                }
            }
            return String.Empty;
        }

        
    }
}
