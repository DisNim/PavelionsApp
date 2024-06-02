using System.Configuration;
using System.Data.SqlClient;

namespace PavelionsApp.Classes
{
    public partial class DataBaseConnection
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["ConnectToDb"].ConnectionString;
        private SqlConnection _connection = null;
        private SqlCommand _executer = null;
        public void Connect()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }
        public void Disconnect()
        {
            _connection.Close();
        }
        private SqlCommand ExecuteCommand(string command)
        {
            _executer = new SqlCommand(command, _connection);
            return _executer;
        }
        public SqlDataReader GetScList(string? search, int sortType)
        {
            return ExecuteCommand($"EXEC dbo.getScList {search}, {sortType}").ExecuteReader();
        }
        public SqlDataReader GetScPavelions(string scName)
        {
            return ExecuteCommand($"EXEC dbo.showPavelions '{scName}'").ExecuteReader();
        }
        public void DeleteSc(string scName)
        {
            ExecuteCommand($"EXEC dbo.removeShoppingCenter '{scName}'").ExecuteNonQuery();
        }
        public void AddEditSc(string id, string scName, string scStatus, string scCity, 
                              string scFloors, string mode, string scAddedVal, string scCast)
        {
            ExecuteCommand($"EXEC dbo.addEditSc {id}, '{scName}', '{scStatus}',"+
                           $"'{scCity}', {scCast}, {scAddedVal}, {scFloors}, {mode}").ExecuteNonQuery();
        }
        public void DeletePav(string idSc, string idPav)
        {
            ExecuteCommand($"EXEC dbo.removePav {idSc}, '{idPav}'").ExecuteNonQuery();
        }
        public void AddPav(string pvId, string pvIdSc, string pvSquare, string pvCost, string pvFloor, string pvAddedVal)
        {
            ExecuteCommand($"EXEC dbo.addPav '{pvId}', {pvIdSc}, {pvSquare}, {pvCost}, {pvAddedVal}, {pvFloor}").ExecuteNonQuery();
        }
        public void AddRent(string tenantryName, string employeesData, string idPav, string idSc, string startDate, string endDate)
        {
            ExecuteCommand($"EXEC dbo.addRent '{tenantryName}', '{employeesData.Split(' ')[1]}', '{employeesData.Split(' ')[0]}',"+
                $"'{employeesData.Split(' ')[2]}', '{idPav}', {idSc}, '{startDate}', '{endDate}'").ExecuteNonQuery();
        }
        public SqlDataReader GetScInfo(string scId)
        {
            return ExecuteCommand($"EXEC dbo.getScInfo {scId}").ExecuteReader();
        }
        public void AddEmployees(string employeesData, string login, string password, string phone, string role, string gender)
        {
            ExecuteCommand($"EXEC dbo.addEmployees '{employeesData.Split(' ', StringSplitOptions.TrimEntries)[0]}', '{employeesData.Split(' ', StringSplitOptions.TrimEntries)[1]}'," +
                $"'{employeesData.Split(' ', StringSplitOptions.TrimEntries)[2]}', '{login}', '{password}', '{gender}', '{phone}', '{role}'").ExecuteNonQuery();
        }
        public void AddTenantry(string tenantryName, string phone, string address)
        {
            ExecuteCommand($"EXEC dbo.addTenantry '{tenantryName}', '{phone}', '{address}'").ExecuteNonQuery();
        }
    }
}
