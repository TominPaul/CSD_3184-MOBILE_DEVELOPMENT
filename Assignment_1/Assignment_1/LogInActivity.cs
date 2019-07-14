using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Assignment_1
{
    [Activity(Label = "LogInActivity")]
    public class LogInActivity : Activity
    {
        Button btn_home, btn_save, btn_listUser;
        EditText userName, userAge, userEmail, userPassword;

        DBHelperClass myDB;
        Android.App.AlertDialog.Builder alert;

        string name, age, password, email;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            SetContentView(Resource.Layout.activity_logIn);
            myDB = new DBHelperClass(this);
            alert = new Android.App.AlertDialog.Builder(this);

            name = Intent.GetStringExtra("userName");            
            email = Intent.GetStringExtra("userEmail");
            password = Intent.GetStringExtra("userPassword");
            age = Intent.GetStringExtra("userAge");

            userName = FindViewById<EditText>(Resource.Id.displayName);
            userEmail = FindViewById<EditText>(Resource.Id.displayEmail);
            userPassword = FindViewById<EditText>(Resource.Id.displayPassword);
            userAge = FindViewById<EditText>(Resource.Id.displayAge);

            userName.Text = name;            
            userEmail.Text = email;
            userPassword.Text = password;
            userAge.Text = age;

            btn_save = FindViewById<Button>(Resource.Id.btnEditProfile);
            btn_listUser = FindViewById<Button>(Resource.Id.btnListOfUsers);
            btn_home = FindViewById<Button>(Resource.Id.btnBack);

            // Click events
            btn_save.Click += editBtnClickEvent;
            btn_home.Click += backBtnClickEvent;
            btn_listUser.Click += listUsersClickEvent;
        }

        public void listUsersClickEvent(object sender, EventArgs e)
        {
            Intent userListScreen = new Intent(this, typeof(ListOfUsersActivity));
            StartActivity(userListScreen);
        }

        public void backBtnClickEvent(object sender, EventArgs e)
        {
            Intent loginScreen = new Intent(this, typeof(MainActivity));
            StartActivity(loginScreen);
        }

        public void editBtnClickEvent(object sender, EventArgs e)
        {
            alert.SetTitle("Error");
            if (userName.Text.Trim().Equals("") || userName.Text.Length < 0 || userAge.Text.Trim().Equals("") || userAge.Text.Length < 0 || userEmail.Text.Trim().Equals("") || userEmail.Text.Length < 0 || userPassword.Text.Trim().Equals("") || userPassword.Text.Length < 0)
            {
                alert.SetMessage("Enter right values in all fields.");
                alert.SetPositiveButton("OK", alertOKButton);
                alert.SetNegativeButton("Cancel", alertOKButton);
                Dialog myDialog = alert.Create();
                myDialog.Show();
            }
            /*else if (userCPassword.Text.Trim() != userCPassword.Text.Trim())
            {
                alert.SetMessage("Passwords doesn't match");
                alert.SetPositiveButton("OK", alertOKButton);
                alert.SetNegativeButton("Cancel", alertOKButton);
                Dialog myDialog = alert.Create();
                myDialog.Show();
            }*/
            else
            {
                myDB.updateData(name, email, password, age);
                alert.SetTitle("Info");
                alert.SetMessage("User details updated successfully!");
                alert.SetPositiveButton("OK", alertSuccessOKButton);
                Dialog myDialog = alert.Create();
                myDialog.Show();
            }
        }

        public void alertOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            System.Console.WriteLine("OK Button Pressed");
        }

        /*public void deleteBtnClicEvent(object sender, EventArgs e)
        {
            alert.SetTitle("Info");
            alert.SetMessage("Do you want to delete this account?");
            alert.SetPositiveButton("Yes", alertYesButton);
            alert.SetNegativeButton("No", alertNoButton);
            Dialog myDialog = alert.Create();
            myDialog.Show();
        }*/

        public void alertSuccessOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            Intent newScreen = new Intent(this, typeof(LogInActivity));
            string[] userData = myDB.getUserData(email, password);
            newScreen.PutExtra("userName", userData[0]);            
            newScreen.PutExtra("userEmail", userData[1]);
            newScreen.PutExtra("userPassword", userData[2]);
            newScreen.PutExtra("userAge", userData[3]);            
            StartActivity(newScreen);
        }
    }
}