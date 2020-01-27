using Pacman.Models;
using Pacman.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Data
{
    public class Seeder
    {
        private PacmanContext context { get; set; }

        public Seeder(PacmanContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// http://country.io/data/
        /// </summary>
        public static List<Country> GetCountries()
        {
            var countryIso = new List<string>(){
                "BD,Bangladesh", "BE,Belgium", "BF,Burkina Faso", "BG,Bulgaria", "BA,Bosnia and Herzegovina", "BB,Barbados", "WF,Wallis and Futuna", "BL,Saint Barthelemy", "BM,Bermuda", "BN,Brunei", "BO,Bolivia", "BH,Bahrain", "BI,Burundi", "BJ,Benin", "BT,Bhutan", "JM,Jamaica", "BV,Bouvet Island", "BW,Botswana", "WS,Samoa", "BQ,Bonaire, Saint Eustatius and Saba ", "BR,Brazil", "BS,Bahamas", "JE,Jersey", "BY,Belarus", "BZ,Belize", "RU,Russia", "RW,Rwanda", "RS,Serbia", "TL,East Timor", "RE,Reunion", "TM,Turkmenistan", "TJ,Tajikistan", "RO,Romania", "TK,Tokelau", "GW,Guinea-Bissau", "GU,Guam", "GT,Guatemala", "GS,South Georgia and the South Sandwich Islands", "GR,Greece", "GQ,Equatorial Guinea", "GP,Guadeloupe", "JP,Japan", "GY,Guyana", "GG,Guernsey", "GF,French Guiana", "GE,Georgia", "GD,Grenada", "GB,United Kingdom", "GA,Gabon", "SV,El Salvador", "GN,Guinea", "GM,Gambia", "GL,Greenland", "GI,Gibraltar", "GH,Ghana", "OM,Oman", "TN,Tunisia", "JO,Jordan", "HR,Croatia", "HT,Haiti", "HU,Hungary", "HK,Hong Kong", "HN,Honduras", "HM,Heard Island and McDonald Islands", "VE,Venezuela", "PR,Puerto Rico", "PS,Palestinian Territory", "PW,Palau", "PT,Portugal", "SJ,Svalbard and Jan Mayen", "PY,Paraguay", "IQ,Iraq", "PA,Panama", "PF,French Polynesia", "PG,Papua New Guinea", "PE,Peru", "PK,Pakistan", "PH,Philippines", "PN,Pitcairn", "PL,Poland", "PM,Saint Pierre and Miquelon", "ZM,Zambia", "EH,Western Sahara", "EE,Estonia", "EG,Egypt", "ZA,South Africa", "EC,Ecuador", "IT,Italy", "VN,Vietnam", "SB,Solomon Islands", "ET,Ethiopia", "SO,Somalia", "ZW,Zimbabwe", "SA,Saudi Arabia", "ES,Spain", "ER,Eritrea", "ME,Montenegro", "MD,Moldova", "MG,Madagascar", "MF,Saint Martin", "MA,Morocco", "MC,Monaco", "UZ,Uzbekistan", "MM,Myanmar", "ML,Mali", "MO,Macao", "MN,Mongolia", "MH,Marshall Islands", "MK,Macedonia", "MU,Mauritius", "MT,Malta", "MW,Malawi", "MV,Maldives", "MQ,Martinique", "MP,Northern Mariana Islands", "MS,Montserrat", "MR,Mauritania", "IM,Isle of Man", "UG,Uganda", "TZ,Tanzania", "MY,Malaysia", "MX,Mexico", "IL,Israel", "FR,France", "IO,British Indian Ocean Territory", "SH,Saint Helena", "FI,Finland", "FJ,Fiji", "FK,Falkland Islands", "FM,Micronesia", "FO,Faroe Islands", "NI,Nicaragua", "NL,Netherlands", "NO,Norway", "NA,Namibia", "VU,Vanuatu", "NC,New Caledonia", "NE,Niger", "NF,Norfolk Island", "NG,Nigeria", "NZ,New Zealand", "NP,Nepal", "NR,Nauru", "NU,Niue", "CK,Cook Islands", "XK,Kosovo", "CI,Ivory Coast", "CH,Switzerland", "CO,Colombia", "CN,China", "CM,Cameroon", "CL,Chile", "CC,Cocos Islands", "CA,Canada", "CG,Republic of the Congo", "CF,Central African Republic", "CD,Democratic Republic of the Congo", "CZ,Czech Republic", "CY,Cyprus", "CX,Christmas Island", "CR,Costa Rica", "CW,Curacao", "CV,Cape Verde", "CU,Cuba", "SZ,Swaziland", "SY,Syria", "SX,Sint Maarten", "KG,Kyrgyzstan", "KE,Kenya", "SS,South Sudan", "SR,Suriname", "KI,Kiribati", "KH,Cambodia", "KN,Saint Kitts and Nevis", "KM,Comoros", "ST,Sao Tome and Principe", "SK,Slovakia", "KR,South Korea", "SI,Slovenia", "KP,North Korea", "KW,Kuwait", "SN,Senegal", "SM,San Marino", "SL,Sierra Leone", "SC,Seychelles", "KZ,Kazakhstan", "KY,Cayman Islands", "SG,Singapore", "SE,Sweden", "SD,Sudan", "DO,Dominican Republic", "DM,Dominica", "DJ,Djibouti", "DK,Denmark", "VG,British Virgin Islands", "DE,Germany", "YE,Yemen", "DZ,Algeria", "US,United States", "UY,Uruguay", "YT,Mayotte", "UM,United States Minor Outlying Islands", "LB,Lebanon", "LC,Saint Lucia", "LA,Laos", "TV,Tuvalu", "TW,Taiwan", "TT,Trinidad and Tobago", "TR,Turkey", "LK,Sri Lanka", "LI,Liechtenstein", "LV,Latvia", "TO,Tonga", "LT,Lithuania", "LU,Luxembourg", "LR,Liberia", "LS,Lesotho", "TH,Thailand", "TF,French Southern Territories", "TG,Togo", "TD,Chad", "TC,Turks and Caicos Islands", "LY,Libya", "VA,Vatican", "VC,Saint Vincent and the Grenadines", "AE,United Arab Emirates", "AD,Andorra", "AG,Antigua and Barbuda", "AF,Afghanistan", "AI,Anguilla", "VI,U.S. Virgin Islands", "IS,Iceland", "IR,Iran", "AM,Armenia", "AL,Albania", "AO,Angola", "AQ,Antarctica", "AS,American Samoa", "AR,Argentina", "AU,Australia", "AT,Austria", "AW,Aruba", "IN,India", "AX,Aland Islands", "AZ,Azerbaijan", "IE,Ireland", "ID,Indonesia", "UA,Ukraine", "QA,Qatar", "MZ,Mozambique"
            };

            var isoToIso = new List<string>(){
                "BD,BGD", "BE,BEL", "BF,BFA", "BG,BGR", "BA,BIH", "BB,BRB", "WF,WLF", "BL,BLM", "BM,BMU", "BN,BRN", "BO,BOL", "BH,BHR", "BI,BDI", "BJ,BEN", "BT,BTN", "JM,JAM", "BV,BVT", "BW,BWA", "WS,WSM", "BQ,BES", "BR,BRA", "BS,BHS", "JE,JEY", "BY,BLR", "BZ,BLZ", "RU,RUS", "RW,RWA", "RS,SRB", "TL,TLS", "RE,REU", "TM,TKM", "TJ,TJK", "RO,ROU", "TK,TKL", "GW,GNB", "GU,GUM", "GT,GTM", "GS,SGS", "GR,GRC", "GQ,GNQ", "GP,GLP", "JP,JPN", "GY,GUY", "GG,GGY", "GF,GUF", "GE,GEO", "GD,GRD", "GB,GBR", "GA,GAB", "SV,SLV", "GN,GIN", "GM,GMB", "GL,GRL", "GI,GIB", "GH,GHA", "OM,OMN", "TN,TUN", "JO,JOR", "HR,HRV", "HT,HTI", "HU,HUN", "HK,HKG", "HN,HND", "HM,HMD", "VE,VEN", "PR,PRI", "PS,PSE", "PW,PLW", "PT,PRT", "SJ,SJM", "PY,PRY", "IQ,IRQ", "PA,PAN", "PF,PYF", "PG,PNG", "PE,PER", "PK,PAK", "PH,PHL", "PN,PCN", "PL,POL", "PM,SPM", "ZM,ZMB", "EH,ESH", "EE,EST", "EG,EGY", "ZA,ZAF", "EC,ECU", "IT,ITA", "VN,VNM", "SB,SLB", "ET,ETH", "SO,SOM", "ZW,ZWE", "SA,SAU", "ES,ESP", "ER,ERI", "ME,MNE", "MD,MDA", "MG,MDG", "MF,MAF", "MA,MAR", "MC,MCO", "UZ,UZB", "MM,MMR", "ML,MLI", "MO,MAC", "MN,MNG", "MH,MHL", "MK,MKD", "MU,MUS", "MT,MLT", "MW,MWI", "MV,MDV", "MQ,MTQ", "MP,MNP", "MS,MSR", "MR,MRT", "IM,IMN", "UG,UGA", "TZ,TZA", "MY,MYS", "MX,MEX", "IL,ISR", "FR,FRA", "IO,IOT", "SH,SHN", "FI,FIN", "FJ,FJI", "FK,FLK", "FM,FSM", "FO,FRO", "NI,NIC", "NL,NLD", "NO,NOR", "NA,NAM", "VU,VUT", "NC,NCL", "NE,NER", "NF,NFK", "NG,NGA", "NZ,NZL", "NP,NPL", "NR,NRU", "NU,NIU", "CK,COK", "XK,XKX", "CI,CIV", "CH,CHE", "CO,COL", "CN,CHN", "CM,CMR", "CL,CHL", "CC,CCK", "CA,CAN", "CG,COG", "CF,CAF", "CD,COD", "CZ,CZE", "CY,CYP", "CX,CXR", "CR,CRI", "CW,CUW", "CV,CPV", "CU,CUB", "SZ,SWZ", "SY,SYR", "SX,SXM", "KG,KGZ", "KE,KEN", "SS,SSD", "SR,SUR", "KI,KIR", "KH,KHM", "KN,KNA", "KM,COM", "ST,STP", "SK,SVK", "KR,KOR", "SI,SVN", "KP,PRK", "KW,KWT", "SN,SEN", "SM,SMR", "SL,SLE", "SC,SYC", "KZ,KAZ", "KY,CYM", "SG,SGP", "SE,SWE", "SD,SDN", "DO,DOM", "DM,DMA", "DJ,DJI", "DK,DNK", "VG,VGB", "DE,DEU", "YE,YEM", "DZ,DZA", "US,USA", "UY,URY", "YT,MYT", "UM,UMI", "LB,LBN", "LC,LCA", "LA,LAO", "TV,TUV", "TW,TWN", "TT,TTO", "TR,TUR", "LK,LKA", "LI,LIE", "LV,LVA", "TO,TON", "LT,LTU", "LU,LUX", "LR,LBR", "LS,LSO", "TH,THA", "TF,ATF", "TG,TGO", "TD,TCD", "TC,TCA", "LY,LBY", "VA,VAT", "VC,VCT", "AE,ARE", "AD,AND", "AG,ATG", "AF,AFG", "AI,AIA", "VI,VIR", "IS,ISL", "IR,IRN", "AM,ARM", "AL,ALB", "AO,AGO", "AQ,ATA", "AS,ASM", "AR,ARG", "AU,AUS", "AT,AUT", "AW,ABW", "IN,IND", "AX,ALA", "AZ,AZE", "IE,IRL", "ID,IDN", "UA,UKR", "QA,QAT", "MZ,MOZ"
            };

            if (countryIso.Count != isoToIso.Count)
            {
                throw new FormatException("Count is different");
            }

            var countries = new List<Country>();
            for (var i = 0; i < countryIso.Count; i++)
            {
                var splitCountryIso = countryIso[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var iso2 = splitCountryIso[0];
                var name = splitCountryIso[1];

                var splitIsoToIso = isoToIso[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var iso2Check = splitIsoToIso[0];
                if (iso2Check != iso2)
                {
                    throw new FormatException("Iso2 codes are different");
                }
                var iso3 = splitIsoToIso[1];

                countries.Add(new Country
                {
                    Name = name,
                    ISO2 = iso2,
                    ISO3 = iso3
                });
            }

            return countries;
        }

        public static List<Anecdote> GetAnecdotes()
        {
            var anectodesArr = new string[]
            {
                "Жена звъни на мъжа си в работата:" +
                "– Веднага се прибирай вкъщи! Твоите деца бият моите, а скоро ще се доберат и до нашите!",

                "Двама шопи разговарят на брега на язовир “Искър”:"+
                "- Ей, глей колко много вода! Цела София пие вода от тойо язовир. Страшно много е т'ва!"+
                "- Абе, ами ако това не беше вода, а ракия?"+
                "- Е, че колко ракия е това?!",
                "Когато съм в прекалено добро настроение, разглеждам цените на имотите...",
                "Ако пътя ви пресече черна мишка, след нея черна котка, след нея черно куче, а след него черно внуче, значи дядото е издърпал не ряпа, а кабел с високо напрежение.",
                "Имаше времена, когато нямаше мобилни телефони, но всеки знаеше къде са приятелите му!",
                "Животът е като тетрис: успехите изчезват, а неуспехите се натрупват.",
                "Не е срамно да те е страх от гората, срамно е да се оправдаваш с мечките.",
                "Времето нашепва: купи палто. Заплатата нашепва: и така е топло.",
            };

            var anecdotes = new List<Anecdote>();
            foreach (var a in anectodesArr)
            {
                anecdotes.Add(new Anecdote
                {
                    AnecdoteNote = a
                });
            }
            return anecdotes;
        }

        public void SeedDatabase()
        {
            var extracter = new ExtractAllLevels();
            var levels = extracter.ExctractLevels();
            context.Levels.AddRange(levels);

            var countries = GetCountries();
            context.Countries.AddRange(countries);

            var anectodes = GetAnecdotes();
            context.Anecdotes.AddRange(anectodes);

            context.SaveChanges();
        }
    }
}
