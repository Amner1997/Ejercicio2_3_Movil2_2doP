using Ejercicio2_3_Movil2_2doP.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio2_3_Movil2_2doP.Controllers
{
    public class DBProcess
    {
        readonly SQLiteAsyncConnection _connection;

        public DBProcess(String dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);
            _connection.CreateTableAsync<Photograph>().Wait();
        }

        public Task<int> AddPhoto(Photograph photograph)
        {
            if (photograph.Id == 0)
            {
                return _connection.InsertAsync(photograph);
            }
            else
            {
                return _connection.UpdateAsync(photograph);
            }
        }

        public Task<List<Photograph>> GetAll()
        {
            return _connection.Table<Photograph>().ToListAsync();
        }

    }
}
