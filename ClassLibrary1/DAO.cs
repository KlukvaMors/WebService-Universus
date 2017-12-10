﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLib
{
    public abstract class DAO
    {
        protected static string connectionString = "server=127.0.0.1;" +
            "uid=root;" +
            "pwd=3616;" +
            "database=universus;";

        public DAO()
        {

        }

        protected static MySqlConnection getConnection()
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }

    }

    public class StudentDAO : DAO
    {
        public static List<Student> getAll()
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECTid, name, surname, email, phone_number, group_id from student";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                MySqlDataReader reader = cmd.ExecuteReader();
                List<Student> students = new List<Student>();
                while (reader.Read())
                {
                    Student student = new Student();
                    student.Id = reader.GetInt32(0);
                    student.Name = reader.GetString(1);
                    student.Surname = reader.GetString(2);
                    student.Email = reader.GetString(3);
                    student.PhoneNumber = reader.GetString(4);
                    students.Add(student);
                }
                return students;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static int update(int studentId, Student s)
        {
            if (s == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"UPDATE student 
                                SET name=@name, surname=@sname, email = @email, phone_number=@phone, group_id = @group
                                WHERE id = @id;";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@id", studentId);
                cmd.Parameters.AddWithValue("@name", s.Name);
                cmd.Parameters.AddWithValue("@sname", s.Surname);
                cmd.Parameters.AddWithValue("@email", s.Email);
                cmd.Parameters.AddWithValue("@phone", s.PhoneNumber);
                cmd.Parameters.AddWithValue("@group", s.GroupId);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static Student getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, name, surname, email, phone_number, group_id FROM student WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                Student student = new Student();
                if (reader.Read())
                {
                    student.Id = reader.GetInt32(0);
                    student.Name = reader.GetString(1);
                    student.Surname = reader.GetString(2);
                    student.Email = reader.GetString(3);
                    student.PhoneNumber = reader.GetString(4);
                    student.GroupId = reader.GetInt32(5);
                }
                return student;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static int deleteById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = "DELETE FROM student WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                return cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static int deleteAll()
        {
            MySqlConnection conn = null;
            string sqlQuery = "DELETE FROM Student";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("delete_all_students", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                return cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }
    }


    public class AccountDAO : DAO
    {
        public static Account getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, username, password_md5, role_id FROM account WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                Account account = new Account();
                if (reader.Read())
                {
                    account.Id = reader.GetInt32(0);
                    account.Username = reader.GetString(1);
                    account.PasswordMD5 = reader.GetString(2);
                    account.RoleId = reader.GetInt32(3);

                }
                return account;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static Account getByUsername(string username)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, username, password_md5, role_id FROM account WHERE id = @uname";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@uname", username);
                MySqlDataReader reader = cmd.ExecuteReader();
                Account account = new Account();
                if (reader.Read())
                {
                    account.Id = reader.GetInt32(0);
                    account.Username = reader.GetString(1);
                    account.PasswordMD5 = reader.GetString(2);
                    account.RoleId = reader.GetInt32(3);

                }
                return account;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static int add(string username, string password, int role_id)
        {
            if (username == null || password == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO account (username, password_md5, role_id)
                VALUES(@uname, @pwd, @role)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@uname", username);
                cmd.Parameters.AddWithValue("@pwd", getMD5(password));
                cmd.Parameters.AddWithValue("@role", role_id);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static string getMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }

    public class MessageDAO : DAO
    {
        public static int add(int fromUserId, int toUserName, string message)
        {
            if (message == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO message (from_user_id, to_user_id, message, date_of_message)
                VALUES(@from, @to, @msg, @date)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@from", fromUserId);
                cmd.Parameters.AddWithValue("@to", toUserName);
                cmd.Parameters.AddWithValue("@msg", message);
                cmd.Parameters.AddWithValue("@date", DateTime.UtcNow);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static Message getMessageFrom(int userId)
        {
            MySqlConnection conn = null;
            string sqlQuery = @"SELECT id, from_user_id, to_user_id, message_content, date_of_message, it_read
                                FROM Message WHERE from_user_id = @id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@id", userId);
                MySqlDataReader reader = cmd.ExecuteReader();
                Message message = new Message();
                if (reader.Read())
                {
                    message.Id = reader.GetInt32(0);
                    message.FromUserId = reader.GetInt32(1);
                    message.ToUserId = reader.GetInt32(2);
                    message.MessageContent = reader.GetString(3);
                    message.DateOfMessage = reader.GetDateTime(4);
                    message.ItRead = reader.GetBoolean(5);
                }
                return message;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static Message getMessageTo(int userId)
        {
            MySqlConnection conn = null;
            string sqlQuery = @"SELECT id, from_user_id, to_user_id, message_content, date_of_message, it_read
                                FROM Message WHERE to_user_id = @id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@id", userId);
                MySqlDataReader reader = cmd.ExecuteReader();
                Message message = new Message();
                if (reader.Read())
                {
                    message.Id = reader.GetInt32(0);
                    message.FromUserId = reader.GetInt32(1);
                    message.ToUserId = reader.GetInt32(2);
                    message.MessageContent = reader.GetString(3);
                    message.DateOfMessage = reader.GetDateTime(4);
                    message.ItRead = reader.GetBoolean(5);
                }
                return message;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }
    }

    public class RoleDAO : DAO
    {
        public static int add(string name)
        {
            if (name == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO role (name)
                VALUES(@name)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@name", name);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static Role getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, name FROM role WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                Role role = new Role();
                if (reader.Read())
                {
                    role.Id = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                }
                return role;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static Role getByName(string name)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, name FROM role WHERE name = @name";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@name", name);
                MySqlDataReader reader = cmd.ExecuteReader();
                Role role = new Role();
                if (reader.Read())
                {
                    role.Id = reader.GetInt32(0);
                    role.Name = reader.GetString(1);
                }
                return role;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }
    }

    class SpecialityDAO : DAO
    {
        public static int add(string name, string specialityCode, string description)
        {
            if (name == null && specialityCode == null && description == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO speciality (name, speciality_code, description)
                VALUES(@name, @code, @descr)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@code", specialityCode);
                cmd.Parameters.AddWithValue("@descr", description);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static Speciality getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, name, speciality_code, description FROM speciality WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                Speciality speciality = new Speciality();
                if (reader.Read())
                {
                    speciality.Id = reader.GetInt32(0);
                    speciality.Name = reader.GetString(1);
                    speciality.SpecialityCode = reader.GetString(2);
                    speciality.Description = reader.GetString(3);
                }
                return speciality;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }

        public static Speciality getBySpecialityCode(string specialityCode)
        {
            MySqlConnection conn = null;
            string sqlQuery = "SELECT id, name, speciality_code, description FROM speciality WHERE speciality_code = @code";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@code", specialityCode);
                MySqlDataReader reader = cmd.ExecuteReader();
                Speciality speciality = new Speciality();
                if (reader.Read())
                {
                    speciality.Id = reader.GetInt32(0);
                    speciality.Name = reader.GetString(1);
                    speciality.SpecialityCode = reader.GetString(2);
                    speciality.Description = reader.GetString(3);
                }
                return speciality;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }
    }


    class UniversityDAO : DAO
    {
        public static int add(string name, string address, bool has_state_accreditation)
        {
            if (name == null && address == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO university (name, address, has_state_accreditation)
                VALUES(@name, @address, @acc)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@acc", has_state_accreditation);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static Unisersity getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = @"SELECT id, name, address, has_state_accreditation FROM university WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                Unisersity unisersity = new Unisersity();
                if (reader.Read())
                {
                    unisersity.Id = reader.GetInt32(0);
                    unisersity.Name = reader.GetString(1);
                    unisersity.Address = reader.GetString(2);
                    unisersity.HasStateAccreditation = reader.GetBoolean(3);
                }
                return unisersity;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }
    }

    class DepartmentDAO : DAO
    {
        public static int add(Department d)
        {
            if (d == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO department (name, phone_number, email)
                VALUES(@name, @phone, @email)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@name", d.Name);
                cmd.Parameters.AddWithValue("@phone", d.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", d.Email);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static Department getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = @"SELECT id, name, phone_number, email FROM department WHERE id = @Id";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                Department department = new Department();
                if (reader.Read())
                {
                    department.Id = reader.GetInt32(0);
                    department.Name = reader.GetString(1);
                    department.PhoneNumber = reader.GetString(2);
                    department.Email = reader.GetString(3);
                }
                return department;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }
    }

    public class StudentGroupDAO : DAO
    {
        public static int add(StudentGroup s)
        {
            if (s == null) return 0;
            MySqlConnection conn = null;
            string sqlQuery = @"INSERT INTO student_group (name, created_date, email, leader_id, manager_id)
                VALUES(@name, @date, @email, @leader, @manager)";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@name", s.Name);
                cmd.Parameters.AddWithValue("@date", s.CreatedDate);
                cmd.Parameters.AddWithValue("@email", s.Email);
                cmd.Parameters.AddWithValue("@leader", s.LeaderID);
                cmd.Parameters.AddWithValue("@manager", s.ManagerID);
                return cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return -1;
        }

        public static void addStudent(int studentId, int groupId)
        {

        }

        public static StudentGroup getById(int id)
        {
            MySqlConnection conn = null;
            string sqlQuery = @"SELECT name, created_date, email, leader_id, manager_id
                                FROM student_group WHERE id = @Id;";
            try
            {
                conn = getConnection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlQuery;
                cmd.Parameters.AddWithValue("@Id", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                StudentGroup studentGroup = new StudentGroup();
                if (reader.Read())
                {
                    studentGroup.Id = reader.GetInt32(0);
                    studentGroup.Name = reader.GetString(1);
                    studentGroup.CreatedDate = reader.GetDateTime(2);
                    studentGroup.Email = reader.GetString(3);
                    studentGroup.LeaderID = reader.GetInt32(4);
                    studentGroup.ManagerID = reader.GetInt32(5);
                }
                return studentGroup;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return null;
        }
    }
}
