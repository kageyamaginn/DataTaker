using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DbDataHelper.Attributes
{
    [AttributeUsage( AttributeTargets.Class, AllowMultiple =true)]
    public class StructureMetaAttribute:Attribute, ISqlCreator
    {
        public StructureMetaAttribute(String sqlString)
        {
            this.SqlString = sqlString;
        }

        public String SqlString { get; set; }

        public string ToSql(object sender)
        {
            return SqlString;
        }
    }

    public interface ISqlCreator
    {
        String ToSql(object sender);
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class StructureConditionMetaAttribute : Attribute,ISqlCreator
    {
        public StructureConditionMetaAttribute(String conditionString, String clauseKey="WHERE",String ConditionString="", bool conditionNullable=false)
        {
            this.ConditionString = conditionString;
            this.ClauseKey = clauseKey;
            
            this.ConditionNullable = conditionNullable;
        }
        public String ClauseKey { get; set; }
        public String ConditionString { get; set; }

        public bool ConditionNullable { get; set; }

        public string ToSql(object sender)
        {
            String cs = ConditionString;
            if (sender.GetType() != typeof(DbDataHelper.Oracle.Table))
            {
                throw new Exception("");
            }
            DbDataHelper.Oracle.Table table = (sender as DbDataHelper.Oracle.Table);

            Regex propertyReg = new Regex("\\{THIS\\.\\w+\\}");
            foreach (Match m in propertyReg.Matches(cs))
            {
                String propertyName= m.Value.Replace("{THIS.", "");
                propertyName = propertyName.Replace("}", "");

                String replaceM = m.Result(typeof(DbDataHelper.Oracle.Table).GetProperty(propertyName).GetValue(sender).ToString());
                System.Reflection.PropertyInfo pi = typeof(DbDataHelper.Oracle.Table).GetProperty(propertyName);
                switch (pi.PropertyType.Name)
                {
                    case "String":
                        replaceM = String.Format("'{0}'", replaceM);
                        break;
                }

                cs = cs.Replace(m.Value, replaceM);
                
            }
            //typeof(DbDataHelper.Oracle.Table).GetProperty("")
            //ConditionString = 
            return String.Format("{0} {1}", ClauseKey, cs);
        }
    }

    #region Connector

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class OR : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AND : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class NOT : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class LIKE : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class IN : Attribute, ISqlCreator
    {
        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }

    #endregion


    [AttributeUsage( AttributeTargets.Property)]
    public class DbEntityMappingAttribute : Attribute, ISqlCreator
    {
        public DbEntityMappingAttribute(String ColumnName)
        {
            this.ColumnName = ColumnName;
        }

        public String ColumnName { get; set; }

        public string ToSql(object sender)
        {
            throw new NotImplementedException();
        }
    }
}
