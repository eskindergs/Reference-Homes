using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CodedHomes.Models;

namespace CodedHomes.Data
{
    public class HomesRepository : GenericRepository<Home>
    {
        public HomesRepository(DbContext context) : base(context) { }
    }
}
