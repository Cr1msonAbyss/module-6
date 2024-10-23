using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfApp1
{
    public class TaskContext : DbContext
    {
        public TaskContext() : base("name=Module6DataBase")
        {
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
