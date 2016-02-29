using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace DmvInfoApp.Models
{
    public class CollisionInfo
    {
        static string ConnectionString = ConfigurationManager.ConnectionStrings["DmvInfoConnectionString"].ConnectionString;

        public int Id { get; set; }
        public string Type { get { return "CollisionInfo"; } }
        public string Borough { get; set; }
        public int ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OnStreetName { get; set; }
        public string CrossStreetName { get; set; }

        public static IEnumerable<CollisionInfo> GetCollisionInfoByBorough(string borough)
        {
            var collisionInfoList = new List<CollisionInfo>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select DATE, TIME, ZIP_CODE, LATITUDE, LONGITUDE, ON_STREET_NAME, CROSS_STREET_NAME, UNIQUE_KEY ");
                sb.Append("from dbo.collisions ");
                sb.Append("where BOROUGH = '");
                sb.Append(borough);
                sb.Append("'; ");

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sb.ToString();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var collisionInfo = new CollisionInfo();
                        string date = reader.GetString(0);
                        string time = reader.GetString(1);

                        collisionInfo.Borough = borough;
                        collisionInfo.ZipCode = int.Parse(reader.GetString(2));
                        collisionInfo.Latitude = double.Parse(reader.GetString(3));
                        collisionInfo.Longitude = double.Parse(reader.GetString(4));
                        collisionInfo.OnStreetName = reader.GetString(5);
                        collisionInfo.CrossStreetName = reader.GetString(6);
                        collisionInfo.Id = int.Parse(reader.GetString(7));
                        collisionInfoList.Add(collisionInfo);
                    }
                }
                connection.Close();
            }

            return collisionInfoList;
        }

        public static IEnumerable<CollisionInfo> GetCollisionInfoByZip(string zip)
        {
            var collisionInfoList = new List<CollisionInfo>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("select DATE, TIME, BOROUGH, LATITUDE, LONGITUDE, ON_STREET_NAME, CROSS_STREET_NAME, UNIQUE_KEY ");
                sb.Append("from dbo.collisions ");
                sb.Append("where ZIP_CODE = '");
                sb.Append(zip);
                sb.Append("'; ");

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sb.ToString();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;

                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var collisionInfo = new CollisionInfo();
                        string date = reader.GetString(0);
                        string time = reader.GetString(1);
                        
                        collisionInfo.ZipCode = int.Parse(zip);
                        collisionInfo.Borough = reader.GetString(2);
                        collisionInfo.Latitude = double.Parse(reader.GetString(3));
                        collisionInfo.Longitude = double.Parse(reader.GetString(4));
                        collisionInfo.OnStreetName = reader.GetString(5);
                        collisionInfo.CrossStreetName = reader.GetString(6);
                        collisionInfo.Id = int.Parse(reader.GetString(7));
                        collisionInfoList.Add(collisionInfo);
                    }
                }
                connection.Close();
            }

            return collisionInfoList;
        }
    }
}