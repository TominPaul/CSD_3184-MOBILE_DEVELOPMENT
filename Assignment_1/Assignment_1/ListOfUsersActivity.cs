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
    [Activity(Label = "ListOfUsersActivity")]
    public class ListOfUsersActivity : Activity
    {
        DBHelperClass myDB;

        ListView myList;
        string[] users;
        ArrayAdapter<string> Myadapter;
        SearchView mySearch;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_listOfUsers);

            // Create your application here
            mySearch = FindViewById<SearchView>(Resource.Id.searchBtn);
            myList = FindViewById<ListView>(Resource.Id.listUsers);

            myDB = new DBHelperClass(this);
            users = myDB.getList();

            // Defined Array values to show in ListView
            Myadapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, users);

            // Assign adapter to ListView
            myList.Adapter = Myadapter;
            myList.ItemClick += List1_ItemClick;
            mySearch.QueryTextChange += userSearch;
        }

        private void List1_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Intent userScreen = new Intent(this, typeof(ProfileActivity)); // on success loading signup page   
            userScreen.PutExtra("userName", users[e.Position]);
            userScreen.PutExtra("userEmail", users[e.Position]);
            userScreen.PutExtra("userPassword", users[e.Position]);
            userScreen.PutExtra("userAge", users[e.Position]);
            StartActivity(userScreen);
        }
        public void userSearch(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            var mySearchValue = e.NewText;
            string temp;
            List<string> arrayTemp = new List<string>();
            for (int i = 0; i < users.Length; ++i)
            {
                temp = users[i].ToLower();
                if (temp.Contains(mySearchValue.ToLower()))
                {
                    arrayTemp.Add(users[i]);
                }
            }
            if (arrayTemp.Count > 0)
            {
                Myadapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arrayTemp);
                myList.Adapter = Myadapter;
            }
        }
    }
}