using Started.Project.RabbitMq.Shared;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Started.Project.RabbitMq.Infra.Repositories
{
    public abstract class Repository
    {

        /// <summary>
        /// Metodo para ler arquivo SQL 
        /// </summary>
        /// <returns></returns>
        public static string GetFileQuery()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var methodName = new StackFrame(1).GetMethod().Name;
            var repositoryName = new StackFrame(1).GetMethod().ReflectedType.Name;
            var fullPath = $"{path}/Resources/{Settings.DbType}/{repositoryName}/{methodName}.sql";

            var script = File.ReadAllText(fullPath);

            return script;
        }
    }
}