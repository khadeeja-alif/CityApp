
using System;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Linq;
using SeatGeek.PlacesAutocomplete;
using SeatGeek.PlacesAutocomplete.Model;
using Java.Lang;
using SeatGeek.PlacesAutocomplete.Adapter;
using Android.Content;
using SeatGeek.PlacesAutocomplete.History;
using Android.Views;
using CityApp.Helpers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CityApp
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, IOnPlaceSelectedListener, IDetailsCallback
    {
        EditText Name, Mobile;
        Button start;
        DateTimeOffset time;
        System.Double latitude, longitude;
        PlacesAutocompleteTextView mAutoComplete;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.StartPage);
            Name = FindViewById<EditText>(Resource.Id.name);
            Mobile = FindViewById<EditText>(Resource.Id.number);
            start = FindViewById<Button>(Resource.Id.start);
            mAutoComplete = FindViewById<PlacesAutocompleteTextView>(Resource.Id.autocomplete);
            mAutoComplete.SetOnPlaceSelectedListener(this);
            start.Click += async delegate
                          {
                              UserData user = new UserData
                              {
                                  name = Name.Text.ToString(),
                                  phone = Mobile.Text.ToString(),
                                  locationName = Settings.City,
                                  latitude = "11.25",
                                  longitude = "75.78"
                              };
                              string body = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(user));
                              var result = await LoginService.Login(body);
                              if(result.status=="UA100")
                              {
                                  Toast.MakeText(this, "User Added", ToastLength.Short).Show();
                                  var intent = new Intent(this, typeof(HomeActivity));
                                  StartActivity(intent);
                                  Finish();
                              }
                              else
                              {
                                  Toast.MakeText(this, "Sorry,Cant Login!", ToastLength.Short).Show();
                              }
                          };
        }

        void IOnPlaceSelectedListener.OnPlaceSelected(SeatGeek.PlacesAutocomplete.Model.Place place)
        {
            mAutoComplete.GetDetailsFor(place, this);
        }

        void IDetailsCallback.OnSuccess(PlaceDetails details)
        {
            System.Diagnostics.Debug.WriteLine("DEMO: {0} {1}", "SUCCESS", details.Name);
            Settings.Address = details.Name;

            foreach (AddressComponent component in details.AddressComponents)
            {
                foreach (AddressComponentType type in component.Types)
                {
                    if (type == AddressComponentType.Locality)
                    {
                        Settings.City = (component.LongName);
                    }
                    else if (type == AddressComponentType.AdministrativeAreaLevel1)
                    {
                        Settings.State = (component.ShortName);
                    }
                    else if (type == AddressComponentType.PostalCode)
                    {
                        Settings.Zip = (component.LongName);
                    }
                }
            }

        }

        void IDetailsCallback.OnFailure(Throwable failure)
        {
            System.Diagnostics.Debug.WriteLine("DEMO: {0} {1}", "FAILURE", failure.Message);
        }
    }

    public class TestPlacesAutocompleteAdapter : AbstractPlacesAutocompleteAdapter
    {
        public TestPlacesAutocompleteAdapter(
            Context context,
            PlacesApi api,
            AutocompleteResultType resultType,
            IAutocompleteHistoryManager history)
            : base(context, api, resultType, history)
        {

        }

        protected override View NewView(ViewGroup parent)
        {
            return LayoutInflater.From(parent.Context)
                                 .Inflate(Resource.Layout.StartPage, parent, false);
        }

        protected override void BindView(View view, Place item)
        {
            ((TextView)view).Text = (item.Description);

        }
    }
}
