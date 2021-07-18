using Mirle.DataBase;

namespace Mirle.BigDataCollection.Define
{
    public class DBConfig : IDBConfig
    {
        public DBTypes DBType { get; } = DBTypes.SqlServer;
        public string DbServer { get; } = string.Empty;
        public string FODBServer { get; } = string.Empty;
        public int DbPort { get; } = 0;
        public string DbName { get; } = string.Empty;
        public string DbUser { get; } = string.Empty;
        public string DbPassword { get; } = string.Empty;
        public int MinPoolSize => 1;
        public int MaxPoolSize => 100;

        public int CommandTimeOut { get; } = 10;
        public int ConnectTimeOut { get; } = 30;
        public bool WriteLog { get; } = false;

        public DBConfig(IDataBase config)
        {
            DBType = (DBTypes)config.DBMS;
            DbServer = config.DbServer;
            FODBServer = config.FODBServer;
            DbPort = config.DbPort;
            DbName = config.DbName;
            DbUser = config.DbUser;
            DbPassword = config.DbPassword;
        }
    }
}