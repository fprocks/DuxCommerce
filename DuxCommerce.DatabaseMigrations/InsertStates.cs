using FluentMigrator;
using System.Collections.Generic;

namespace DuxCommerce.DatabaseMigrations
{
    [Migration(202012271011)]
    public class InsertStates : Migration
    {
        public override void Up()
        {
            foreach (var country in GetAllStates())
            {
                foreach (var state in country.States)
                {
                    var countryCode = country.CountryCode.Trim();
                    var name = state.Trim();
                    Insert.IntoTable("State").Row(new { countryCode, name });
                }
            }
        }

        public override void Down()
        {
        }

        private List<CountryState> GetAllStates()
        {
            var allStates = new List<CountryState>();

            allStates.Add(GetUsStates());
            allStates.Add(GetInStates());
            allStates.Add(GetAuStates());

            return allStates;
        }
        private CountryState GetUsStates()
        {
            var states = new List<string>
            {
                "Alabama",
                "Alaska",
                "American Samoa",
                "American Samoa",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Washington DC",
                "Micronesia",
                "Florida",
                "Georgia",
                "Guam",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Marshall Islands",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Northern Mariana Islands",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Palau",
                "Pennsylvania",
                "Puerto Rico",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "U.S. Virgin Islands",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming",
                "Armed Forces Americas",
                "Armed Forces Europe",
                "Armed Forces Pacific"
            };

            return new CountryState("US", states);
        }

        private CountryState GetInStates()
        {
            var states = new List<string>
            {
                "Andaman and Nicobar Islands",
                "Andhra Pradesh",
                "Arunachal Pradesh",
                "Assam",
                "Bihar",
                "Chandigarh",
                "Chhattisgarh",
                "Dadra and Nagar Haveli",
                "Daman and Diu",
                "Delhi",
                "Goa",
                "Gujarat",
                "Haryana",
                "Himachal Pradesh",
                "Jammu and Kashmir",
                "Jharkhand",
                "Karnataka",
                "Kerala",
                "Ladakh",
                "Lakshadweep",
                "Madhya Pradesh",
                "Maharashtra",
                "Manipur",
                "Meghalaya",
                "Mizoram",
                "Nagaland",
                "Odisha",
                "Puducherry",
                "Punjab",
                "Rajasthan",
                "Sikkim",
                "Tamil Nadu",
                "Telangana",
                "Tripura",
                "Uttar Pradesh",
                "Uttarakhand",
                "West Bengal"
            };

            return new CountryState("IN", states);
        }

        private CountryState GetAuStates()
        {
            var states = new List<string>
            {
                "Australian Capital Territory",
                "New South Wales",
                "Northern Territory",
                "Queensland",
                "South Australia",
                "Tasmania",
                "Victoria",
                "Western Australia"
            };

            return new CountryState("AU", states);
        }

        class CountryState
        {
            private string _CountryCode;
            private List<string> _States;

            public CountryState(string countryCode, List<string> states)
            {
                CountryCode = countryCode;
                States = states;
            }

            public List<string> States { get => _States; set => _States = value; }
            public string CountryCode { get => _CountryCode; set => _CountryCode = value; }
        }
    }
}
