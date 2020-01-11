using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace YiFlag.Model
{
   public class DbContextFactory
    {
        public static YiFlagContext Create()
        {
            YiFlagContext dbContext = CallContext.GetData("YiFlagContext") as YiFlagContext;
            if (dbContext == null)
            {
                dbContext = new YiFlagContext();
                CallContext.SetData("YiFlagContext", dbContext);
            }
            return dbContext;
        }
    }
}
