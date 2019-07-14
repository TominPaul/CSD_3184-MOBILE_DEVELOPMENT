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
        string userName;

        DBHelperClass myDB;

        EditText myName, myEmail, myPassword, myAge;
        Button btn_Back;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_userProfile);

            myDB = new DBHelperClass(this);
            myName = FindViewById<EditText>(Resource.Id.displayName);
            myEmail = FindViewById<EditText>(Resource.Id.displayEmail);
            myPassword = FindViewById<EditText>(Resource.Id.displayPassword);
            myAge = FindViewById<EditText>(Resource.Id.displayAge);
            btn_Back = FindViewById<Button>(Resource.Id.btnBack);

            userName = Intent.GetStringExtra(myEmail.Text);
            userData = myDB.getUserData(userName);

            myName.Text = userData[0];
            myEmail.Text = userData[1];
            myAge.Text = userData[2];
            myPassword.Text = userData[3];

            btn_Back.Click += backToList;
        }
        public void backToList(object sender, EventArgs e)
        {
            Intent usersListScreen = new Intent(this, typeof(ListOfUsersActivity)); // on success loading signup page            
            StartActivity(usersListScreen);
        }
    }
}