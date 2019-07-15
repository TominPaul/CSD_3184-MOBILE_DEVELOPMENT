using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System;

namespace Assignment_1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText userName, userPassword;
        Button btn_logIn, btn_signUp;

        DBHelperClass myDB;
        Android.App.AlertDialog.Builder alert;        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            userName = FindViewById<EditText>(Resource.Id.myEmail);
            userPassword = FindViewById<EditText>(Resource.Id.myPassword);
            btn_logIn = FindViewById<Button>(Resource.Id.logIn_button1);
            btn_signUp = FindViewById<Button>(Resource.Id.signUp_Button);

            alert = new Android.App.AlertDialog.Builder(this);

            myDB = new DBHelperClass(this);

            btn_logIn.Click += delegate
            {
                var value1 = userName.Text;
                var value2 = userPassword.Text;

                System.Console.WriteLine("UserName: " + value1);
                System.Console.WriteLine("Password: " + value2);

                if (value1.Trim().Equals(" ") || value1.Length < 0 || value2.Trim().Equals(" ") || value2.Length < 0)
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Please Enter Valid Data");
                    alert.SetPositiveButton("OK", alertOKButton);
                    alert.SetNegativeButton("Cancel", alertOKButton);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
                else
                {
                    if (myDB.checkUser(value1, value2))
                    {
                        string[] userData = myDB.getUserData(value1, value2);

                        Intent newScreen = new Intent(this, typeof(LogInActivity));
                        newScreen.PutExtra("userName", userData[0]);                        
                        newScreen.PutExtra("userEmail", userData[1]);
                        newScreen.PutExtra("userPassword", userData[2]);
                        newScreen.PutExtra("userAge", userData[3]);
                        StartActivity(newScreen);
                    }
                    else
                    {
                        alert.SetTitle("Error");
                        alert.SetMessage("Invalid Email id or Password");
                        alert.SetPositiveButton("OK", alertOKButton);
                        Dialog myDialog = alert.Create();
                        myDialog.Show();
                    }
                }
            };

            btn_signUp.Click += delegate
            {
                Intent signUpScreen = new Intent(this, typeof(SignupActivity));
                StartActivity(signUpScreen);
            };
        }

        public void alertOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            System.Console.WriteLine("OK Button Pressed");
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}