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
    [Activity(Label = "ProfileActivity")]
    public class ProfileActivity : Activity
    {
        string[] userData = new string[4];
        string myName;

        DBHelperClass myDB;

        EditText userName, userEmail, userPassword, userAge;
        Button btn_Back;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_userProfile);

            userName = FindViewById<EditText>(Resource.Id.displayName);
            userEmail = FindViewById<EditText>(Resource.Id.displayEmail);
            userPassword = FindViewById<EditText>(Resource.Id.displayPassword);
            userAge = FindViewById<EditText>(Resource.Id.displayAge);
            btn_Back = FindViewById<Button>(Resource.Id.btnBack);

            myDB = new DBHelperClass(this);
            myName = Intent.GetStringExtra("userName");
            userData = myDB.getUserData(myName);

            userName.Text = userData[0];
            userEmail.Text = userData[1];
            userPassword.Text = userData[2];
            userAge.Text = userData[3];            

            userName.Enabled = false;
            userEmail.Enabled = false;
            userPassword.Enabled = false;
            userAge.Enabled = false;

            btn_Back.Click += backToList;
        }
        public void backToList(object sender, EventArgs e)
        {
            Intent usersListScreen = new Intent(this, typeof(ListOfUsersActivity)); // on success loading signup page            
            StartActivity(usersListScreen);
        }
    }
}