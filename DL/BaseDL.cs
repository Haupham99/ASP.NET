using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Entity;

namespace DL
{
    public class BaseDL
    {
        //public virtual IEnumerable<Entity.Student> GetData()
        //{
        //    List<Entity.Student> entities = new List<Entity.Student>();
        //    //Kết nối cơ sở dữ liệu
        //    string _connectionString = @"Data Source=FRESHER-59\MISASME2017;Initial Catalog=pvhau;Integrated Security=True";
        //    SqlConnection _sqlConnection = new SqlConnection(_connectionString);
        //    SqlCommand _sqlCommand = _sqlConnection.CreateCommand();

        //    _sqlCommand.CommandText = "SELECT * FROM Student";
        //    _sqlConnection.Open();
        //    SqlDataReader _sqlDataReader = _sqlCommand.ExecuteReader();

        //    while (_sqlDataReader.Read())
        //    {
        //        Student student = new Student();
        //        for (int i = 0; i < _sqlDataReader.FieldCount; i++)
        //        {
        //            var value = _sqlDataReader.GetValue(i);

        //            var name = _sqlDataReader.GetName(i);
        //            if (student.GetType().GetProperty(name) != null)
        //            {
        //                student.GetType().GetProperty(name).SetValue(student, value);
        //            }
        //        }
        //        entities.Add(student);
        //    }
        //    return entities;
        //}

        public IEnumerable<T> Get<T>()
        {
            var className = Activator.CreateInstance<T>().GetType().Name;
            return GetData<T>("Select * from " + className);
        }
        public IEnumerable<T> Get<T>(string commandText)
        {
            return GetData<T>(commandText);
        }

        public virtual IEnumerable<T> GetData<T>(string commandText) {
            //Kết nối cơ sở dữ liệu
            string _connectionString = @"Data Source=FRESHER-59\MISASME2017;Initial Catalog=pvhau;Integrated Security=True";
            SqlConnection _sqlConnection = new SqlConnection(_connectionString);
            SqlCommand _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandText = commandText;
            _sqlConnection.Open();
            SqlDataReader _sqlDataReader = _sqlCommand.ExecuteReader();
            List<T> entities = new List<T>(); 

            while (_sqlDataReader.Read())
            {
                var entity = Activator.CreateInstance<T>();
                for(var i = 0; i < _sqlDataReader.FieldCount; i++)
                {
                    var name = _sqlDataReader.GetName(i);
                    var value = _sqlDataReader.GetValue(i);
                    if(entity.GetType().GetProperty(name) != null)
                    {
                        entity.GetType().GetProperty(name).SetValue(entity, value);
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }
    }
}
