using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFGetStarted
{
    static class Program
    {
        private static int QuantityOfEntities = 10;
        private static Stopwatch _stopWatch = new Stopwatch();
        private static BloggingContext _blogginContext = new BloggingContext();
        private static int InputOption = 0;
        private static string Option => InputOption == 0 ? "Linq" : "RawSQL";

        public static void Main(string[] args)
        {
            if (args.Count() > 0 && !String.IsNullOrWhiteSpace(args[0])) InputOption = Int32.Parse(args[0]);
            if (args.Count() > 1 && !String.IsNullOrWhiteSpace(args[1])) QuantityOfEntities = Int32.Parse(args[1]);

            Console.WriteLine($"Chosen option: {Option}");
            Console.WriteLine($"Quantity of interations: {QuantityOfEntities}");

            switch (InputOption)
            {
                case 0: LinqExecution(); break;
                case 1: RawSqlExecution(); break;
                default: break;
            }
        }

        #region Linq
        public static void LinqExecution()
        {
            _stopWatch.Start();

            for (int i = 0; i < QuantityOfEntities; i++) CreateEntityLinq();

            var blogs = GetAllEntityLinq();

            foreach (var blog in blogs)
            {
                var b = GetByIdEntityLinq(blog.BlogId);
                DeleteLinq(b);
            }

            _stopWatch.Stop();

            TimeSpan ts = _stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Linq > RunTime " + elapsedTime);
        }
        public static void CreateEntityLinq()
        {
            _blogginContext.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            _blogginContext.SaveChanges();
        }
        public static IList<Blog> GetAllEntityLinq() => _blogginContext.Blogs.ToList();
        public static Blog GetByIdEntityLinq(int id) => _blogginContext.Blogs.Where(x => x.BlogId == id).FirstOrDefault();
        public static void DeleteLinq(Blog b)
        {
            _blogginContext.Blogs.Remove(b);
            _blogginContext.SaveChanges();
        }
        #endregion

        #region RawSql
        public static void RawSqlExecution()
        {
            _stopWatch.Start();

            for (int i = 0; i < QuantityOfEntities; i++) CreateEntityRawSql();

            var blogs = GetAllEntityRawSql();

            foreach (var blog in blogs)
            {
                var b = GetByIdEntityRawSql(blog.BlogId);
                DeleteRawSql(b);
            }

            _stopWatch.Stop();

            TimeSpan ts = _stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RawSQL > RunTime " + elapsedTime);
        }
        public static void CreateEntityRawSql()
        {
            _blogginContext.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
            _blogginContext.SaveChanges();
        }
        public static IList<Blog> GetAllEntityRawSql() => _blogginContext.Blogs.FromSqlRaw("SELECT * FROM dbo.Blogs").ToList();
        public static Blog GetByIdEntityRawSql(int id) => _blogginContext.Blogs.FromSqlRaw($"SELECT * FROM dbo.Blogs WHERE BlogId = {id}").FirstOrDefault();
        public static void DeleteRawSql(Blog b) => _blogginContext.Blogs.FromSqlRaw($"DELETE FROM dbo.Blogs WHERE BlogId={b.BlogId}");
        #endregion
    }
}