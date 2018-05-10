using System;
using System.Collections.Generic;
using System.Text;
using DbDataHelper.Attributes;

namespace DbDataHelper.Oracle
{
    public class OracleDataHelper
    {
    }

    public class Connection
    {

    }
    [StructureMeta("SELECT OWNER FROM ALL_OBJECTS GROUP BY OWNER ORDER BY COUNT(OWNER),OWNER")]
    public class Owner
    {

    }
    [StructureMeta("SELECT T.OWNER, TABLE_NAME, T.STATUS, O.OBJECT_TYPE, O.LAST_DDL_TIME FROM ALL_TABLES T LEFT JOIN ALL_OBJECTS O ON T.OWNER = O.OWNER AND T.TABLE_NAME = O.OBJECT_NAME")]
    [Where]
    [StructureConditionMeta("OWNER = {THIS.Owner}")]
    [AND]
    [StructureConditionMeta("TABLE_NAME = {THIS.TableName}")]
    public class Table
    {
        public String Owner { get; set; }
        public String TableName { get; set; }
        public String Status { get; set; }
        public String ObjectType { get; set; }
        public DateTime LastDdlTime { get; set; }
    }
    [StructureMeta("SELECT * FROM ALL_TAB_COLUMNS")]
    [Where]
    [StructureConditionMeta("TABLE_NAME = {THIS.TableName}")]
    [AND]
    [StructureConditionMeta("OWNER = {THIS.Owner}")]
    public class Column
    {
        public string Owner { get; set; }
        public String TableName { get; set; }
        public String ColorName { get; set; }
        public String DataType { get; set; }
        public Decimal DataLength { get; set; }
        public bool Nullable { get; set; }
        
    }

    public class Constraint
    {

    }

    public class Index
    {

    }
}
