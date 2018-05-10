using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DbDataHelper.Oracle;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Table t = new Table();
            t.TableName = "SNT_FI_AUDIT";
            t.Owner = "PLM";

            Console.WriteLine(AnalysisAttribute(t));

            Owner o = new Owner();
            Console.WriteLine(AnalysisAttribute(o));

            Column c = new Column();
            c.Owner = "PLM";
            c.TableName = "SNT_FI_AUDIT";
            Console.WriteLine(AnalysisAttribute(c));
            

            Console.ReadKey();

        }


        static String AnalysisAttribute(object o)
        {
            Type tableType = o.GetType();
            String sql = "";
            List<Attribute> atts = tableType.GetCustomAttributes().ToList();
            for (int attIndex = 0; attIndex < atts.Count(); attIndex++)
            {
                Type attType = atts[attIndex].GetType();
                MethodInfo mi = attType.GetMethod("ToSql");

                sql += " " + mi.Invoke(atts[attIndex], new object[] { o }).ToString();

            }

            return sql;
        }
    }
}
