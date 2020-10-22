using Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestDal
{
   public class PersonDal
    {
        public List<Person> GetPeople()
        {
            List<Person> list = new List<Person>();
            using (TestDbContext dbContext=new TestDbContext ())
            {
                 list = dbContext.Person.ToList();
            }
            return list;
        }
    }
}
