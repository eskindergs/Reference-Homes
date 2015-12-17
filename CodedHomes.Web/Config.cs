using System.Configuration;


namespace CodedHomes.Web
{
    public class Config
    {
        public static string ConnectionStringName
        {
            get
            {
                if (ConfigurationManager.AppSettings["ConnectionStringName"] != null)
                {
                    return ConfigurationManager.AppSettings["ConnectionStringName"].ToString();
                }

                return "DefaultConnection";
            }
        }

        public static string UsersTableName
        {
            get
            {
                if (ConfigurationManager.AppSettings["UsersTableName"]
                    != null)
                {
                    return ConfigurationManager
                        .AppSettings["UsersTableName"].ToString();
                }

                return "Users";
            }
        }

        public static string UsersPrimaryKeyColumnName
        {
            get
            {
                if (ConfigurationManager.AppSettings["UsersPrimaryKeyColumnName"]
                    != null)
                {
                    return ConfigurationManager
                        .AppSettings["UsersPrimaryKeyColumnName"].ToString();
                }

                return "Id";
            }
        }

        public static string UsersUserNameColumnName
        {
            get
            {
                if (ConfigurationManager.AppSettings["UsersUserNameColumnName"]
                    != null)
                {
                    return ConfigurationManager
                        .AppSettings["UsersUserNameColumnName"].ToString();
                }

                return "Username";
            }
        }

        public static string ImagesFolderPath
        {
            get
            {
                if (ConfigurationManager.AppSettings["ImagesFolderPath"]
                    != null)
                {
                    return ConfigurationManager
                        .AppSettings["ImagesFolderPath"].ToString();
                }

                return "~/img/homes";
            }
        }

        public static string ImagesUrlPrefix
        {
            get { return Config.ImagesFolderPath.Replace("~", ""); }
        }

    }
}