using System;
using Android.Support.V7.App;
using Android.Runtime;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Assignment_1
{
    [Activity(Label = "SignUpActivity", Theme = "@style/AppTheme")]
    public class SignupActivity : AppCompatActivity
    {
        EditText userName, userEmail, userPassword, userAge;
        Button btn_submit;

        DBHelperClass myDB;
        Android.App.AlertDialog.Builder alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_signUp);
            
            alert = new Android.App.AlertDialog.Builder(this);
            myDB = new DBHelperClass(this);

            userName = FindViewById<EditText>(Resource.Id.inputName);
            userEmail = FindViewById<EditText>(Resource.Id.inputEmail);
            userPassword = FindViewById<EditText>(Resource.Id.inputPassword);
            userAge = FindViewById<EditText>(Resource.Id.inputAge);

            btn_submit = FindViewById<Button>(Resource.Id.btnSignUp);

            btn_submit.Click += delegate
            {
                var nameValue = userName.Text;
                var emailValue = userEmail.Text;
                var passwordValue = userPassword.Text;
                var ageValue = userAge.Text;

                if ((nameValue.Trim().Equals("") || nameValue.Length < 0) ||
                (emailValue.Trim().Equals("") || emailValue.Length < 0) ||
                (passwordValue.Trim().Equals("") || passwordValue.Length < 0) ||
                (ageValue.Trim().Equals("") || ageValue.Length < 0))
                {
                    alert.SetTitle("Error");
                    alert.SetMessage("Enter right values in all fields.");
                    alert.SetPositiveButton("OK", alertOKButton);
                    alert.SetNegativeButton("Cancel", alertOKButton);
                    Dialog myDialog = alert.Create();
                    myDialog.Show();
                }
                else
                {                    
                    myDB.insertValue(nameValue, emailValue, passwordValue, Convert.ToInt32(ageValue));
                    Console.WriteLine("Insertion Success");

                    Intent newScreen = new Intent(this, typeof(MainActivity));
                    StartActivity(newScreen);
                }
            };
        }
        public void alertOKButton(object sender, Android.Content.DialogClickEventArgs e)
        {
            System.Console.WriteLine("OK Button Pressed");
        }
    }
}