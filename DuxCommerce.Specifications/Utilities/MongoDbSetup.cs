using DuxCommerce.Common;
using DuxCommerce.Core.Shared;
using DuxCommerce.Core.Shipping.PublicTypes;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DuxCommerce.Core.Shared.PublicTypes;
using DuxCommerce.Core.Taxes.PublicTypes;

namespace DuxCommerce.Specifications.Utilities
{
    public class MongoDbSetup
    {
        public static async Task ResetAsync()
        {
            IMongoDatabase mongodb = MongoDatabase.GetConnection();

            var storeProfile = mongodb.GetCollection<StoreProfileDto>(CollectionName.StoreProfile);
            var addresses = mongodb.GetCollection<AddressDto>(CollectionName.Address);
            var shippingOrigins = mongodb.GetCollection<ShippingOriginDto>(CollectionName.ShippingOrigin);
            var shippingProfiles = mongodb.GetCollection<ShippingProfileDto>(CollectionName.ShippingProfile);
            var taxRates = mongodb.GetCollection<TaxRateDto>(CollectionName.TaxRate);

            // Todo: any better ways to delete all documents in a collection?
            await storeProfile.DeleteManyAsync(new BsonDocument());
            await addresses.DeleteManyAsync(new BsonDocument());
            await shippingOrigins.DeleteManyAsync(new BsonDocument());
            await shippingProfiles.DeleteManyAsync(new BsonDocument());
            await taxRates.DeleteManyAsync(new BsonDocument());
        }

        public static async Task InitAsync()
        {
            var mongodb = MongoDatabase.GetConnection();

            var countries = mongodb.GetCollection<CountryDto>(CollectionName.Country);
            await countries.InsertManyAsync(GetCountries());

            var states = mongodb.GetCollection<StateDto>(CollectionName.State);
            await states.InsertManyAsync(GetStates());
        }

        private static IEnumerable<CountryDto> GetCountries()
        {
            var input = "[{\"Name\":\"Afghanistan\",\"ISOCode\":\"AF\",\"country-code\":\"004\"},{\"Name\":\"Åland Islands\",\"ISOCode\":\"AX\",\"country-code\":\"248\"},{\"Name\":\"Albania\",\"ISOCode\":\"AL\",\"country-code\":\"008\"},{\"Name\":\"Algeria\",\"ISOCode\":\"DZ\",\"country-code\":\"012\"},{\"Name\":\"American Samoa\",\"ISOCode\":\"AS\",\"country-code\":\"016\"},{\"Name\":\"Andorra\",\"ISOCode\":\"AD\",\"country-code\":\"020\"},{\"Name\":\"Angola\",\"ISOCode\":\"AO\",\"country-code\":\"024\"},{\"Name\":\"Anguilla\",\"ISOCode\":\"AI\",\"country-code\":\"660\"},{\"Name\":\"Antarctica\",\"ISOCode\":\"AQ\",\"country-code\":\"010\"},{\"Name\":\"Antigua and Barbuda\",\"ISOCode\":\"AG\",\"country-code\":\"028\"},{\"Name\":\"Argentina\",\"ISOCode\":\"AR\",\"country-code\":\"032\"},{\"Name\":\"Armenia\",\"ISOCode\":\"AM\",\"country-code\":\"051\"},{\"Name\":\"Aruba\",\"ISOCode\":\"AW\",\"country-code\":\"533\"},{\"Name\":\"Australia\",\"ISOCode\":\"AU\",\"country-code\":\"036\"},{\"Name\":\"Austria\",\"ISOCode\":\"AT\",\"country-code\":\"040\"},{\"Name\":\"Azerbaijan\",\"ISOCode\":\"AZ\",\"country-code\":\"031\"},{\"Name\":\"Bahamas\",\"ISOCode\":\"BS\",\"country-code\":\"044\"},{\"Name\":\"Bahrain\",\"ISOCode\":\"BH\",\"country-code\":\"048\"},{\"Name\":\"Bangladesh\",\"ISOCode\":\"BD\",\"country-code\":\"050\"},{\"Name\":\"Barbados\",\"ISOCode\":\"BB\",\"country-code\":\"052\"},{\"Name\":\"Belarus\",\"ISOCode\":\"BY\",\"country-code\":\"112\"},{\"Name\":\"Belgium\",\"ISOCode\":\"BE\",\"country-code\":\"056\"},{\"Name\":\"Belize\",\"ISOCode\":\"BZ\",\"country-code\":\"084\"},{\"Name\":\"Benin\",\"ISOCode\":\"BJ\",\"country-code\":\"204\"},{\"Name\":\"Bermuda\",\"ISOCode\":\"BM\",\"country-code\":\"060\"},{\"Name\":\"Bhutan\",\"ISOCode\":\"BT\",\"country-code\":\"064\"},{\"Name\":\"Bolivia (Plurinational State of)\",\"ISOCode\":\"BO\",\"country-code\":\"068\"},{\"Name\":\"Bonaire, Sint Eustatius and Saba\",\"ISOCode\":\"BQ\",\"country-code\":\"535\"},{\"Name\":\"Bosnia and Herzegovina\",\"ISOCode\":\"BA\",\"country-code\":\"070\"},{\"Name\":\"Botswana\",\"ISOCode\":\"BW\",\"country-code\":\"072\"},{\"Name\":\"Bouvet Island\",\"ISOCode\":\"BV\",\"country-code\":\"074\"},{\"Name\":\"Brazil\",\"ISOCode\":\"BR\",\"country-code\":\"076\"},{\"Name\":\"British Indian Ocean Territory\",\"ISOCode\":\"IO\",\"country-code\":\"086\"},{\"Name\":\"Brunei Darussalam\",\"ISOCode\":\"BN\",\"country-code\":\"096\"},{\"Name\":\"Bulgaria\",\"ISOCode\":\"BG\",\"country-code\":\"100\"},{\"Name\":\"Burkina Faso\",\"ISOCode\":\"BF\",\"country-code\":\"854\"},{\"Name\":\"Burundi\",\"ISOCode\":\"BI\",\"country-code\":\"108\"},{\"Name\":\"Cabo Verde\",\"ISOCode\":\"CV\",\"country-code\":\"132\"},{\"Name\":\"Cambodia\",\"ISOCode\":\"KH\",\"country-code\":\"116\"},{\"Name\":\"Cameroon\",\"ISOCode\":\"CM\",\"country-code\":\"120\"},{\"Name\":\"Canada\",\"ISOCode\":\"CA\",\"country-code\":\"124\"},{\"Name\":\"Cayman Islands\",\"ISOCode\":\"KY\",\"country-code\":\"136\"},{\"Name\":\"Central African Republic\",\"ISOCode\":\"CF\",\"country-code\":\"140\"},{\"Name\":\"Chad\",\"ISOCode\":\"TD\",\"country-code\":\"148\"},{\"Name\":\"Chile\",\"ISOCode\":\"CL\",\"country-code\":\"152\"},{\"Name\":\"China\",\"ISOCode\":\"CN\",\"country-code\":\"156\"},{\"Name\":\"Christmas Island\",\"ISOCode\":\"CX\",\"country-code\":\"162\"},{\"Name\":\"Cocos (Keeling) Islands\",\"ISOCode\":\"CC\",\"country-code\":\"166\"},{\"Name\":\"Colombia\",\"ISOCode\":\"CO\",\"country-code\":\"170\"},{\"Name\":\"Comoros\",\"ISOCode\":\"KM\",\"country-code\":\"174\"},{\"Name\":\"Congo\",\"ISOCode\":\"CG\",\"country-code\":\"178\"},{\"Name\":\"Congo, Democratic Republic of the\",\"ISOCode\":\"CD\",\"country-code\":\"180\"},{\"Name\":\"Cook Islands\",\"ISOCode\":\"CK\",\"country-code\":\"184\"},{\"Name\":\"Costa Rica\",\"ISOCode\":\"CR\",\"country-code\":\"188\"},{\"Name\":\"Côte d'Ivoire\",\"ISOCode\":\"CI\",\"country-code\":\"384\"},{\"Name\":\"Croatia\",\"ISOCode\":\"HR\",\"country-code\":\"191\"},{\"Name\":\"Cuba\",\"ISOCode\":\"CU\",\"country-code\":\"192\"},{\"Name\":\"Curaçao\",\"ISOCode\":\"CW\",\"country-code\":\"531\"},{\"Name\":\"Cyprus\",\"ISOCode\":\"CY\",\"country-code\":\"196\"},{\"Name\":\"Czechia\",\"ISOCode\":\"CZ\",\"country-code\":\"203\"},{\"Name\":\"Denmark\",\"ISOCode\":\"DK\",\"country-code\":\"208\"},{\"Name\":\"Djibouti\",\"ISOCode\":\"DJ\",\"country-code\":\"262\"},{\"Name\":\"Dominica\",\"ISOCode\":\"DM\",\"country-code\":\"212\"},{\"Name\":\"Dominican Republic\",\"ISOCode\":\"DO\",\"country-code\":\"214\"},{\"Name\":\"Ecuador\",\"ISOCode\":\"EC\",\"country-code\":\"218\"},{\"Name\":\"Egypt\",\"ISOCode\":\"EG\",\"country-code\":\"818\"},{\"Name\":\"El Salvador\",\"ISOCode\":\"SV\",\"country-code\":\"222\"},{\"Name\":\"Equatorial Guinea\",\"ISOCode\":\"GQ\",\"country-code\":\"226\"},{\"Name\":\"Eritrea\",\"ISOCode\":\"ER\",\"country-code\":\"232\"},{\"Name\":\"Estonia\",\"ISOCode\":\"EE\",\"country-code\":\"233\"},{\"Name\":\"Eswatini\",\"ISOCode\":\"SZ\",\"country-code\":\"748\"},{\"Name\":\"Ethiopia\",\"ISOCode\":\"ET\",\"country-code\":\"231\"},{\"Name\":\"Falkland Islands (Malvinas)\",\"ISOCode\":\"FK\",\"country-code\":\"238\"},{\"Name\":\"Faroe Islands\",\"ISOCode\":\"FO\",\"country-code\":\"234\"},{\"Name\":\"Fiji\",\"ISOCode\":\"FJ\",\"country-code\":\"242\"},{\"Name\":\"Finland\",\"ISOCode\":\"FI\",\"country-code\":\"246\"},{\"Name\":\"France\",\"ISOCode\":\"FR\",\"country-code\":\"250\"},{\"Name\":\"French Guiana\",\"ISOCode\":\"GF\",\"country-code\":\"254\"},{\"Name\":\"French Polynesia\",\"ISOCode\":\"PF\",\"country-code\":\"258\"},{\"Name\":\"French Southern Territories\",\"ISOCode\":\"TF\",\"country-code\":\"260\"},{\"Name\":\"Gabon\",\"ISOCode\":\"GA\",\"country-code\":\"266\"},{\"Name\":\"Gambia\",\"ISOCode\":\"GM\",\"country-code\":\"270\"},{\"Name\":\"Georgia\",\"ISOCode\":\"GE\",\"country-code\":\"268\"},{\"Name\":\"Germany\",\"ISOCode\":\"DE\",\"country-code\":\"276\"},{\"Name\":\"Ghana\",\"ISOCode\":\"GH\",\"country-code\":\"288\"},{\"Name\":\"Gibraltar\",\"ISOCode\":\"GI\",\"country-code\":\"292\"},{\"Name\":\"Greece\",\"ISOCode\":\"GR\",\"country-code\":\"300\"},{\"Name\":\"Greenland\",\"ISOCode\":\"GL\",\"country-code\":\"304\"},{\"Name\":\"Grenada\",\"ISOCode\":\"GD\",\"country-code\":\"308\"},{\"Name\":\"Guadeloupe\",\"ISOCode\":\"GP\",\"country-code\":\"312\"},{\"Name\":\"Guam\",\"ISOCode\":\"GU\",\"country-code\":\"316\"},{\"Name\":\"Guatemala\",\"ISOCode\":\"GT\",\"country-code\":\"320\"},{\"Name\":\"Guernsey\",\"ISOCode\":\"GG\",\"country-code\":\"831\"},{\"Name\":\"Guinea\",\"ISOCode\":\"GN\",\"country-code\":\"324\"},{\"Name\":\"Guinea-Bissau\",\"ISOCode\":\"GW\",\"country-code\":\"624\"},{\"Name\":\"Guyana\",\"ISOCode\":\"GY\",\"country-code\":\"328\"},{\"Name\":\"Haiti\",\"ISOCode\":\"HT\",\"country-code\":\"332\"},{\"Name\":\"Heard Island and McDonald Islands\",\"ISOCode\":\"HM\",\"country-code\":\"334\"},{\"Name\":\"Holy See\",\"ISOCode\":\"VA\",\"country-code\":\"336\"},{\"Name\":\"Honduras\",\"ISOCode\":\"HN\",\"country-code\":\"340\"},{\"Name\":\"Hong Kong\",\"ISOCode\":\"HK\",\"country-code\":\"344\"},{\"Name\":\"Hungary\",\"ISOCode\":\"HU\",\"country-code\":\"348\"},{\"Name\":\"Iceland\",\"ISOCode\":\"IS\",\"country-code\":\"352\"},{\"Name\":\"India\",\"ISOCode\":\"IN\",\"country-code\":\"356\"},{\"Name\":\"Indonesia\",\"ISOCode\":\"ID\",\"country-code\":\"360\"},{\"Name\":\"Iran (Islamic Republic of)\",\"ISOCode\":\"IR\",\"country-code\":\"364\"},{\"Name\":\"Iraq\",\"ISOCode\":\"IQ\",\"country-code\":\"368\"},{\"Name\":\"Ireland\",\"ISOCode\":\"IE\",\"country-code\":\"372\"},{\"Name\":\"Isle of Man\",\"ISOCode\":\"IM\",\"country-code\":\"833\"},{\"Name\":\"Israel\",\"ISOCode\":\"IL\",\"country-code\":\"376\"},{\"Name\":\"Italy\",\"ISOCode\":\"IT\",\"country-code\":\"380\"},{\"Name\":\"Jamaica\",\"ISOCode\":\"JM\",\"country-code\":\"388\"},{\"Name\":\"Japan\",\"ISOCode\":\"JP\",\"country-code\":\"392\"},{\"Name\":\"Jersey\",\"ISOCode\":\"JE\",\"country-code\":\"832\"},{\"Name\":\"Jordan\",\"ISOCode\":\"JO\",\"country-code\":\"400\"},{\"Name\":\"Kazakhstan\",\"ISOCode\":\"KZ\",\"country-code\":\"398\"},{\"Name\":\"Kenya\",\"ISOCode\":\"KE\",\"country-code\":\"404\"},{\"Name\":\"Kiribati\",\"ISOCode\":\"KI\",\"country-code\":\"296\"},{\"Name\":\"Korea (Democratic People's Republic of)\",\"ISOCode\":\"KP\",\"country-code\":\"408\"},{\"Name\":\"Korea, Republic of\",\"ISOCode\":\"KR\",\"country-code\":\"410\"},{\"Name\":\"Kuwait\",\"ISOCode\":\"KW\",\"country-code\":\"414\"},{\"Name\":\"Kyrgyzstan\",\"ISOCode\":\"KG\",\"country-code\":\"417\"},{\"Name\":\"Lao People's Democratic Republic\",\"ISOCode\":\"LA\",\"country-code\":\"418\"},{\"Name\":\"Latvia\",\"ISOCode\":\"LV\",\"country-code\":\"428\"},{\"Name\":\"Lebanon\",\"ISOCode\":\"LB\",\"country-code\":\"422\"},{\"Name\":\"Lesotho\",\"ISOCode\":\"LS\",\"country-code\":\"426\"},{\"Name\":\"Liberia\",\"ISOCode\":\"LR\",\"country-code\":\"430\"},{\"Name\":\"Libya\",\"ISOCode\":\"LY\",\"country-code\":\"434\"},{\"Name\":\"Liechtenstein\",\"ISOCode\":\"LI\",\"country-code\":\"438\"},{\"Name\":\"Lithuania\",\"ISOCode\":\"LT\",\"country-code\":\"440\"},{\"Name\":\"Luxembourg\",\"ISOCode\":\"LU\",\"country-code\":\"442\"},{\"Name\":\"Macao\",\"ISOCode\":\"MO\",\"country-code\":\"446\"},{\"Name\":\"Madagascar\",\"ISOCode\":\"MG\",\"country-code\":\"450\"},{\"Name\":\"Malawi\",\"ISOCode\":\"MW\",\"country-code\":\"454\"},{\"Name\":\"Malaysia\",\"ISOCode\":\"MY\",\"country-code\":\"458\"},{\"Name\":\"Maldives\",\"ISOCode\":\"MV\",\"country-code\":\"462\"},{\"Name\":\"Mali\",\"ISOCode\":\"ML\",\"country-code\":\"466\"},{\"Name\":\"Malta\",\"ISOCode\":\"MT\",\"country-code\":\"470\"},{\"Name\":\"Marshall Islands\",\"ISOCode\":\"MH\",\"country-code\":\"584\"},{\"Name\":\"Martinique\",\"ISOCode\":\"MQ\",\"country-code\":\"474\"},{\"Name\":\"Mauritania\",\"ISOCode\":\"MR\",\"country-code\":\"478\"},{\"Name\":\"Mauritius\",\"ISOCode\":\"MU\",\"country-code\":\"480\"},{\"Name\":\"Mayotte\",\"ISOCode\":\"YT\",\"country-code\":\"175\"},{\"Name\":\"Mexico\",\"ISOCode\":\"MX\",\"country-code\":\"484\"},{\"Name\":\"Micronesia (Federated States of)\",\"ISOCode\":\"FM\",\"country-code\":\"583\"},{\"Name\":\"Moldova, Republic of\",\"ISOCode\":\"MD\",\"country-code\":\"498\"},{\"Name\":\"Monaco\",\"ISOCode\":\"MC\",\"country-code\":\"492\"},{\"Name\":\"Mongolia\",\"ISOCode\":\"MN\",\"country-code\":\"496\"},{\"Name\":\"Montenegro\",\"ISOCode\":\"ME\",\"country-code\":\"499\"},{\"Name\":\"Montserrat\",\"ISOCode\":\"MS\",\"country-code\":\"500\"},{\"Name\":\"Morocco\",\"ISOCode\":\"MA\",\"country-code\":\"504\"},{\"Name\":\"Mozambique\",\"ISOCode\":\"MZ\",\"country-code\":\"508\"},{\"Name\":\"Myanmar\",\"ISOCode\":\"MM\",\"country-code\":\"104\"},{\"Name\":\"Namibia\",\"ISOCode\":\"NA\",\"country-code\":\"516\"},{\"Name\":\"Nauru\",\"ISOCode\":\"NR\",\"country-code\":\"520\"},{\"Name\":\"Nepal\",\"ISOCode\":\"NP\",\"country-code\":\"524\"},{\"Name\":\"Netherlands\",\"ISOCode\":\"NL\",\"country-code\":\"528\"},{\"Name\":\"New Caledonia\",\"ISOCode\":\"NC\",\"country-code\":\"540\"},{\"Name\":\"New Zealand\",\"ISOCode\":\"NZ\",\"country-code\":\"554\"},{\"Name\":\"Nicaragua\",\"ISOCode\":\"NI\",\"country-code\":\"558\"},{\"Name\":\"Niger\",\"ISOCode\":\"NE\",\"country-code\":\"562\"},{\"Name\":\"Nigeria\",\"ISOCode\":\"NG\",\"country-code\":\"566\"},{\"Name\":\"Niue\",\"ISOCode\":\"NU\",\"country-code\":\"570\"},{\"Name\":\"Norfolk Island\",\"ISOCode\":\"NF\",\"country-code\":\"574\"},{\"Name\":\"North Macedonia\",\"ISOCode\":\"MK\",\"country-code\":\"807\"},{\"Name\":\"Northern Mariana Islands\",\"ISOCode\":\"MP\",\"country-code\":\"580\"},{\"Name\":\"Norway\",\"ISOCode\":\"NO\",\"country-code\":\"578\"},{\"Name\":\"Oman\",\"ISOCode\":\"OM\",\"country-code\":\"512\"},{\"Name\":\"Pakistan\",\"ISOCode\":\"PK\",\"country-code\":\"586\"},{\"Name\":\"Palau\",\"ISOCode\":\"PW\",\"country-code\":\"585\"},{\"Name\":\"Palestine, State of\",\"ISOCode\":\"PS\",\"country-code\":\"275\"},{\"Name\":\"Panama\",\"ISOCode\":\"PA\",\"country-code\":\"591\"},{\"Name\":\"Papua New Guinea\",\"ISOCode\":\"PG\",\"country-code\":\"598\"},{\"Name\":\"Paraguay\",\"ISOCode\":\"PY\",\"country-code\":\"600\"},{\"Name\":\"Peru\",\"ISOCode\":\"PE\",\"country-code\":\"604\"},{\"Name\":\"Philippines\",\"ISOCode\":\"PH\",\"country-code\":\"608\"},{\"Name\":\"Pitcairn\",\"ISOCode\":\"PN\",\"country-code\":\"612\"},{\"Name\":\"Poland\",\"ISOCode\":\"PL\",\"country-code\":\"616\"},{\"Name\":\"Portugal\",\"ISOCode\":\"PT\",\"country-code\":\"620\"},{\"Name\":\"Puerto Rico\",\"ISOCode\":\"PR\",\"country-code\":\"630\"},{\"Name\":\"Qatar\",\"ISOCode\":\"QA\",\"country-code\":\"634\"},{\"Name\":\"Réunion\",\"ISOCode\":\"RE\",\"country-code\":\"638\"},{\"Name\":\"Romania\",\"ISOCode\":\"RO\",\"country-code\":\"642\"},{\"Name\":\"Russian Federation\",\"ISOCode\":\"RU\",\"country-code\":\"643\"},{\"Name\":\"Rwanda\",\"ISOCode\":\"RW\",\"country-code\":\"646\"},{\"Name\":\"Saint Barthélemy\",\"ISOCode\":\"BL\",\"country-code\":\"652\"},{\"Name\":\"Saint Helena, Ascension and Tristan da Cunha\",\"ISOCode\":\"SH\",\"country-code\":\"654\"},{\"Name\":\"Saint Kitts and Nevis\",\"ISOCode\":\"KN\",\"country-code\":\"659\"},{\"Name\":\"Saint Lucia\",\"ISOCode\":\"LC\",\"country-code\":\"662\"},{\"Name\":\"Saint Martin (French part)\",\"ISOCode\":\"MF\",\"country-code\":\"663\"},{\"Name\":\"Saint Pierre and Miquelon\",\"ISOCode\":\"PM\",\"country-code\":\"666\"},{\"Name\":\"Saint Vincent and the Grenadines\",\"ISOCode\":\"VC\",\"country-code\":\"670\"},{\"Name\":\"Samoa\",\"ISOCode\":\"WS\",\"country-code\":\"882\"},{\"Name\":\"San Marino\",\"ISOCode\":\"SM\",\"country-code\":\"674\"},{\"Name\":\"Sao Tome and Principe\",\"ISOCode\":\"ST\",\"country-code\":\"678\"},{\"Name\":\"Saudi Arabia\",\"ISOCode\":\"SA\",\"country-code\":\"682\"},{\"Name\":\"Senegal\",\"ISOCode\":\"SN\",\"country-code\":\"686\"},{\"Name\":\"Serbia\",\"ISOCode\":\"RS\",\"country-code\":\"688\"},{\"Name\":\"Seychelles\",\"ISOCode\":\"SC\",\"country-code\":\"690\"},{\"Name\":\"Sierra Leone\",\"ISOCode\":\"SL\",\"country-code\":\"694\"},{\"Name\":\"Singapore\",\"ISOCode\":\"SG\",\"country-code\":\"702\"},{\"Name\":\"Sint Maarten (Dutch part)\",\"ISOCode\":\"SX\",\"country-code\":\"534\"},{\"Name\":\"Slovakia\",\"ISOCode\":\"SK\",\"country-code\":\"703\"},{\"Name\":\"Slovenia\",\"ISOCode\":\"SI\",\"country-code\":\"705\"},{\"Name\":\"Solomon Islands\",\"ISOCode\":\"SB\",\"country-code\":\"090\"},{\"Name\":\"Somalia\",\"ISOCode\":\"SO\",\"country-code\":\"706\"},{\"Name\":\"South Africa\",\"ISOCode\":\"ZA\",\"country-code\":\"710\"},{\"Name\":\"South Georgia and the South Sandwich Islands\",\"ISOCode\":\"GS\",\"country-code\":\"239\"},{\"Name\":\"South Sudan\",\"ISOCode\":\"SS\",\"country-code\":\"728\"},{\"Name\":\"Spain\",\"ISOCode\":\"ES\",\"country-code\":\"724\"},{\"Name\":\"Sri Lanka\",\"ISOCode\":\"LK\",\"country-code\":\"144\"},{\"Name\":\"Sudan\",\"ISOCode\":\"SD\",\"country-code\":\"729\"},{\"Name\":\"Suriname\",\"ISOCode\":\"SR\",\"country-code\":\"740\"},{\"Name\":\"Svalbard and Jan Mayen\",\"ISOCode\":\"SJ\",\"country-code\":\"744\"},{\"Name\":\"Sweden\",\"ISOCode\":\"SE\",\"country-code\":\"752\"},{\"Name\":\"Switzerland\",\"ISOCode\":\"CH\",\"country-code\":\"756\"},{\"Name\":\"Syrian Arab Republic\",\"ISOCode\":\"SY\",\"country-code\":\"760\"},{\"Name\":\"Taiwan, Province of China\",\"ISOCode\":\"TW\",\"country-code\":\"158\"},{\"Name\":\"Tajikistan\",\"ISOCode\":\"TJ\",\"country-code\":\"762\"},{\"Name\":\"Tanzania, United Republic of\",\"ISOCode\":\"TZ\",\"country-code\":\"834\"},{\"Name\":\"Thailand\",\"ISOCode\":\"TH\",\"country-code\":\"764\"},{\"Name\":\"Timor-Leste\",\"ISOCode\":\"TL\",\"country-code\":\"626\"},{\"Name\":\"Togo\",\"ISOCode\":\"TG\",\"country-code\":\"768\"},{\"Name\":\"Tokelau\",\"ISOCode\":\"TK\",\"country-code\":\"772\"},{\"Name\":\"Tonga\",\"ISOCode\":\"TO\",\"country-code\":\"776\"},{\"Name\":\"Trinidad and Tobago\",\"ISOCode\":\"TT\",\"country-code\":\"780\"},{\"Name\":\"Tunisia\",\"ISOCode\":\"TN\",\"country-code\":\"788\"},{\"Name\":\"Turkey\",\"ISOCode\":\"TR\",\"country-code\":\"792\"},{\"Name\":\"Turkmenistan\",\"ISOCode\":\"TM\",\"country-code\":\"795\"},{\"Name\":\"Turks and Caicos Islands\",\"ISOCode\":\"TC\",\"country-code\":\"796\"},{\"Name\":\"Tuvalu\",\"ISOCode\":\"TV\",\"country-code\":\"798\"},{\"Name\":\"Uganda\",\"ISOCode\":\"UG\",\"country-code\":\"800\"},{\"Name\":\"Ukraine\",\"ISOCode\":\"UA\",\"country-code\":\"804\"},{\"Name\":\"United Arab Emirates\",\"ISOCode\":\"AE\",\"country-code\":\"784\"},{\"Name\":\"United Kingdom of Great Britain and Northern Ireland\",\"ISOCode\":\"GB\",\"country-code\":\"826\"},{\"Name\":\"United States of America\",\"ISOCode\":\"US\",\"country-code\":\"840\"},{\"Name\":\"United States Minor Outlying Islands\",\"ISOCode\":\"UM\",\"country-code\":\"581\"},{\"Name\":\"Uruguay\",\"ISOCode\":\"UY\",\"country-code\":\"858\"},{\"Name\":\"Uzbekistan\",\"ISOCode\":\"UZ\",\"country-code\":\"860\"},{\"Name\":\"Vanuatu\",\"ISOCode\":\"VU\",\"country-code\":\"548\"},{\"Name\":\"Venezuela (Bolivarian Republic of)\",\"ISOCode\":\"VE\",\"country-code\":\"862\"},{\"Name\":\"Viet Nam\",\"ISOCode\":\"VN\",\"country-code\":\"704\"},{\"Name\":\"Virgin Islands (British)\",\"ISOCode\":\"VG\",\"country-code\":\"092\"},{\"Name\":\"Virgin Islands (U.S.)\",\"ISOCode\":\"VI\",\"country-code\":\"850\"},{\"Name\":\"Wallis and Futuna\",\"ISOCode\":\"WF\",\"country-code\":\"876\"},{\"Name\":\"Western Sahara\",\"ISOCode\":\"EH\",\"country-code\":\"732\"},{\"Name\":\"Yemen\",\"ISOCode\":\"YE\",\"country-code\":\"887\"},{\"Name\":\"Zambia\",\"ISOCode\":\"ZM\",\"country-code\":\"894\"},{\"Name\":\"Zimbabwe\",\"ISOCode\":\"ZW\",\"country-code\":\"716\"}]";
            return JsonConvert.DeserializeObject<IEnumerable<CountryDto>>(input);
        }

        private static IEnumerable<StateDto> GetStates()
        {
            var result = new List<StateDto>();

            result.AddRange(from country in GetCountryStates()
                            from state in country.States
                            let countryCode = country.CountryCode.Trim()
                            let name = state.Trim()
                            select new StateDto { Name = name, CountryCode = countryCode });

            return result;
        }

        private static IEnumerable<(string CountryCode, List<string> States)> GetCountryStates()
        {
            var countryStates = new List<(string CountryCode, List<string> States)>();

            countryStates.Add(GetUsStates());
            countryStates.Add(GetInStates());
            countryStates.Add(GetAuStates());
            countryStates.Add(GetNzStates());

            return countryStates;
        }

        private static (string, List<string>) GetUsStates()
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

            return ("US", states);
        }

        private static (string, List<string>) GetInStates()
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

            return ("IN", states);
        }

        private static (string, List<string>) GetAuStates()
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

            return ("AU", states);
        }

        private static (string, List<string>) GetNzStates()
        {
            var states = new List<string>
            {
                "Auckland",
                "Bay of Plenty",
                "Canterbury",
                "Gisborne",
                "Hawke’s Bay",
                "Manawatu-Wanganui",
                "Marlborough",
                "Nelson",
                "Northland",
                "Otago",
                "Southland",
                "Taranaki",
                "Tasman",
                "Waikato",
                "Wellington",
                "West Coast"
            };

            return ("NZ", states);
        }
    }
}
