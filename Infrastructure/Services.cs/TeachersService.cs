using Npgsql;
using Dapper;
using Domain.DTOs;

public class TeachersService
{
    //private List<TeacherDto> _teacher;
    string connectionString = "Server =localhost;Port=5432;Database=EXAM1;User Id=postgres;Password=215130;";
    public TeachersService()
    {

    }
    
    //Get all teachers
    public List<teachersDTO> GetTeachers(string? name, string? surname)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            var sql = "select teacher_id as Id, first_name FirstName, last_name LastName, email_address as Email from teachers";
            if (name != null)
                sql += $" where lower(first_name) like '%@Name%'";
            var result = conn.Query<teachersDTO>(sql,new {Name = name});
            return result.ToList();
        }
    }
    
    //Get by Id
    public teachersDTO GetTeacherById(int id)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            var sql = $"select teacher_id as Id, first_name FirstName, last_name LastName, email_address as Email from teachers where teacher_id = @Id";
            var result = conn.QuerySingle<teachersDTO>(sql, new {Id = id});
            return result;
        }
    }
    
    //insert 
    public teachersDTO AddTeacher(teachersDTO teacher)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            var sql = $"insert into teachers (first_name, last_name, email_address) VALUES (@FirstName,@LastName,@EmailAddress,@TeacherId) returning teacher_id";
            var result = conn.ExecuteScalar<int>(sql, teacher);
            teacher.TeacherId = result;
            return teacher;
        }
    }
}
