using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace InventoryManagementSystem.Models
{
    public class IMSContext: DbContext 
    {
        public IMSContext(string conn = "name=IMSEntities") : base(conn)
        {
            Database.SetInitializer<IMSContext>(null);
        }
    }
}