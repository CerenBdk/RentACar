using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class RecapProjectDBContext : DbContext
    {
        public RecapProjectDBContext():base("server=.;database=RecapProjectDB;user id=cerenbudak; password=123123")
        {

        }

        public DbSet<Car> Car { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Color> Color { get; set; }

    }
}
