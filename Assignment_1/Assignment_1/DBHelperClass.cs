using System;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;


namespace Assignment_1
{
    class DBHelperClass : SQLiteOpenHelper
    {
        Context myContex;
        
        // Database Name
        public static string dbName = "myDatabse.db";

        // Database Table Name
        public static string tableName = "Users";

        // Table Columns
        public static string userName = "userName";
        public static string userEmail = "userEmail";
        public static string userPassword = "userPassword";
        public static string userAge = "userAge";


        public string creatTable = string.Format("CREATE TABLE {0} ({1} TEXT, {2} TEXT PRIMARY KEY, {3} TEXT, {4} INTEGER);"
            , tableName, userName, userEmail, userPassword, userAge);

        SQLiteDatabase connectionObj;

        public DBHelperClass(Context context) : base(context, name: dbName, factory: null, version: 1) //Step 5
        {
            myContex = context;
            connectionObj = WritableDatabase;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            System.Console.WriteLine("My Create Table STM \n \n" + creatTable);
            db.ExecSQL(creatTable);
        }

        //My insert function
        public void insertValue(string nameValue, string emailValue, string passwordValue, int ageValue)
        {
            string insertStm = string.Format("INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', {4});",
                tableName, nameValue, emailValue, passwordValue, ageValue);

            System.Console.WriteLine("My SQL  Insert STM \n  \n" + insertStm);
            connectionObj.ExecSQL(insertStm);
        }

        public Boolean checkUser(string emailid, string password)
        {
            string check_query = string.Format("SELECT * FROM {0} WHERE {1} = '"+ emailid +"' and {2} = '"+ password +"'", tableName, userEmail, userPassword);
            ICursor myResult = connectionObj.RawQuery(check_query, null);

            if (myResult.Count > 0)
                return true;
            else
                return false;
        }

        public string[] getUserData(string emailid, string password)
        {
            string check_query = string.Format("SELECT * FROM {0} WHERE {1} = '"+ emailid +"' and {2} = '"+ password +"'", tableName, userEmail, userPassword);
            ICursor myResult = connectionObj.RawQuery(check_query, null);

            string[] userData = new string[4];
            if (myResult.Count > 0)
            {
                while (myResult.MoveToNext())
                {
                    userData[0] = myResult.GetString(myResult.GetColumnIndexOrThrow("userName"));
                    userData[1] = myResult.GetString(myResult.GetColumnIndexOrThrow("userEmail"));
                    userData[2] = myResult.GetString(myResult.GetColumnIndexOrThrow("userPassword"));
                    userData[3] = myResult.GetInt(myResult.GetColumnIndexOrThrow("userAge")).ToString();                    
                }
            }
            return userData;
        }

        // My Select data from table function
        public void selectMydata()
        {
            string selectStm = string.Format("select * from {0}", tableName);
            ICursor myResult = connectionObj.RawQuery(selectStm, null);

            while (myResult.MoveToNext())
            {
                var myEmail = myResult.GetString(myResult.GetColumnIndexOrThrow(userEmail));
                System.Console.WriteLine("Email from DB: " + myEmail);

                var myName = myResult.GetString(myResult.GetColumnIndexOrThrow(userName));
                System.Console.WriteLine("Name from DB: " + myName);
            }
        }
        public string[] getList()
        {
            string[] ListUsers; int i = 0;
            string selQuery = string.Format("SELECT * FROM {0}", tableName);
            ICursor myResult = connectionObj.RawQuery(selQuery, null);
            ListUsers = new string[myResult.Count];

            while (myResult.MoveToNext())
            {
                ListUsers[i] = myResult.GetString(myResult.GetColumnIndexOrThrow("userName"));
                ++i;
            }
            return ListUsers;
        }
        public void updateData(string name, string email_id, string password, string age)
        {
            string updateQuery = string.Format("UPDATE {0} SET {1} = '"+ name +"', {2} = '"+ age +"', {3} = '"+ password +"' WHERE {4} = '"+ email_id +"'", tableName, userName, userAge, userPassword, userEmail);
            Console.WriteLine(updateQuery);
            connectionObj.ExecSQL(updateQuery);
        }
        public string[] getUserData(string email)
        {
            string selectUserQuery = string.Format("SELECT * FROM {0} WHERE {1} = '"+ email +"'", tableName, userEmail);
            Console.WriteLine(selectUserQuery);
            ICursor myResult = connectionObj.RawQuery(selectUserQuery, null);
            string[] userInfo = new string[4];

            while (myResult.MoveToNext())
            {
                userInfo[0] = myResult.GetString(myResult.GetColumnIndexOrThrow("Name"));
                userInfo[1] = myResult.GetString(myResult.GetColumnIndexOrThrow("Email"));
                userInfo[2] = myResult.GetString(myResult.GetColumnIndexOrThrow("Password"));
                userInfo[3] = myResult.GetString(myResult.GetColumnIndexOrThrow("Age"));
            }
            return userInfo;
        }
        /*public void deleteUser(string email_id)
        {
            string deleteQuery = string.Format("delete from {0} where {1} = '" + email_id + "'", tableName, userEmail);
            Console.WriteLine(deleteQuery);
            connectionObj.ExecSQL(deleteQuery);
        }*/

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}