using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using LinqToTwitter;
using System.Diagnostics;

namespace TUBES_STIMA_3
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        Program tw = new Program();
        //KAMUS!!
        String kind_of_place = "cfd kawasan daerah wilayah kabupaten simply kwarcab rumah sman stadion stasiun terminal bandara pelabuhan klinik airport sma smu smk sd smp smpn jl masjid gereja jl. lap lapangan lapang balai bale jalan sungai lapas kota waduk taman gedung teluk pantai gunung bukit villa"; //tambah lagi
        String exceptional_keyword = "tweet post minum makan tinggal anniversary order booking event peringatan sel tatar sebarkan sebar launching 1234567890 hati lokasi dunia sana sini satu dua cintai tabrak lakukan tiga empat lima enam tujuh sembilan sepuluh titik dieu acara konter counter kantor"; // tambah lagi
        public class Program
        {
            public class keyword //This is nested class for grouping keyword by category, e.g : Dinas kebersihan, etc.
            {
                public int id;
                public string str;
                public keyword()
                {
                    id = 0;
                    str = "";
                }
            }

            private const int UNDEF = -1; //for now, Forget about it 
            private const int bound = 6;
            private List<keyword> key = new List<keyword>(); //List of keyword for some category of organization e.g : Dinas kebersihan : Sampah; 
            private List<List<Status>> cat = new List<List<Status>>(bound); //This list used for container of tweet
            private List<Status> tweets; //Container of tweet which is retrieved from twitter.com

            public List<keyword> getkey()
            {
                return key;
            }

            public List<List<Status>> getcat()
            {
                return cat;
            }

            public List<Status> gettweets()
            {
                return tweets;
            }

            //Authorize user to access API
            private SingleUserAuthorizer authorizer =
                 new SingleUserAuthorizer
                 {
                     CredentialStore = new
                     SingleUserInMemoryCredentialStore
                     {
                         //You can get this code from your twiiter account
                         ConsumerKey =
                           "wlb7bXrkknmUhqev3FFGyG3Ce",
                         ConsumerSecret =
                          "54KzL3Tt0OBFvD9Ah2tiRCHMT3xClg1MSE0iPNd3ccvw1R5Rw1",
                         AccessToken =
                            "167988305-idP2UDH5ral2b0LVKrsaunE886cZivEAR8pKLdTz",
                         AccessTokenSecret =
                        "TRjpdubS7JmY1sXv9VMOmxQr1fNFk8HnafB1g1d2YKeau"
                     }
                 };

            //Search tweet
            public void SearchTweets(string searchpat)
            {
                var twitterContext = new TwitterContext(authorizer);
                var srch =
                    Enumerable.SingleOrDefault(from search in twitterContext.Search
                                               where search.Type == SearchType.Search &&
                                               search.Query == searchpat && search.Count == 200
                                               select search
                                                );
                if (srch != null && srch.Statuses != null)
                {
                    tweets = srch.Statuses.ToList();
                }
            }

            //Add keyword to List and group it
            public void setKeyword(List<string> keywor)
            {
                for (int i = 0; i < keywor.Count; i++)
                {
                    int startidx = 0;
                    for (int j = 0; j < keywor[i].Length; j++)
                    {
                        if (keywor[i][j] == ';')
                        {
                            keyword keytemp = new keyword();
                            keytemp.str = keywor[i].Substring(startidx, j - startidx);
                            keytemp.id = i;
                            key.Add(keytemp);
                            startidx = j + 1;
                        }
                    }
                }
            }

            //Knuth Morris Prat preprocess Algorithm
            private List<int> fail_func(string pat)
            {
                List<int> arrOfBorder = new List<int>(pat.Length);
                int i, j, k;
                //initialize arrOfBorder with 0
                for (i = 0; i < pat.Length; i++)
                {
                    arrOfBorder.Add(0);
                }
                i = 1; //i = 1 because the arrOfBorder[0] always 0
                while (i < pat.Length)
                {
                    j = i;
                    k = 0;
                    while (j > 0 && Char.ToLowerInvariant(pat[k]) == Char.ToLowerInvariant(pat[j]))
                    { //Checking if the pattern has same prefix and suffix
                        arrOfBorder[i] += 1;
                        k++;
                        j--;
                    }
                    i++;
                }
                return arrOfBorder;
            }

            //This is the main Algorith of Knuth Morris Prat
            public void kmp()
            {
                List<List<int>> ListOfBorder = new List<List<int>>(key.Count);
                //initialization list of list
                for (int ct = 0; ct < bound; ct++)
                {
                    cat.Add(new List<Status>());
                }
                //set the list of list 
                for (int br = 0; br < key.Count; br++)
                {
                    ListOfBorder.Add(fail_func(key[br].str));
                }
                int i = 0, k, j, l, m;
                bool found;
                Status st = new Status();
                string href = "<a target='blank' href='http://twitter.com/";
                int dis = 0;
                while (i < tweets.Count)
                {
                    found = false;
                    bool finish = false, foundAt = false;
                    j = k = l = m = 0;
                    //assign st with information on list of tweets, such as userID, user.ScreenName, Tweets, etc.
                    st = tweets[i];
                    while (j < st.Text.Length && !found)
                    {
                        if (!finish)
                        {
                            if (j < st.Text.Length - 1)
                            {
                                if ((st.Text[j] == '@' || st.Text[j] == '#') && !foundAt)
                                {
                                    if (st.Text[j] == '@')
                                    {
                                        st.Text = st.Text.Insert(j, href);
                                        j += href.Length;
                                    }
                                    else
                                    {
                                        st.Text = st.Text.Insert(j, href);
                                        j += href.Length - 1;
                                        dis--;
                                    }
                                    foundAt = true;
                                }
                                else
                                {
                                    j++;
                                }
                                if (foundAt)
                                {
                                    if (j < st.Text.Length - 1)
                                    {
                                        if (st.Text[j + 1] != ' ' && st.Text[j + 1] != ':' && st.Text[j + 1] != '\n')
                                        {
                                            st.Text = st.Text.Insert(j - dis, st.Text[j + 1].ToString());
                                            j++;
                                            dis++;
                                        }
                                        else
                                        {
                                            while (st.Text[j - dis - 1] == '.' || st.Text[j - dis - 1] == ',')
                                            {
                                                st.Text = st.Text.Remove(j - dis - 1, 1);
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(j - dis, "'>");
                                            j += 2;
                                            dis = 0;
                                            while (st.Text[j - dis] == ' ' || st.Text[j - dis] == '.' || st.Text[j - dis] == ',')
                                            {
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(j - dis + 1, "</a>");
                                            j += "</a>".Length + 1;
                                            foundAt = false;
                                            dis = 0;

                                        }
                                    } // <a target='blank' href='@nawe_mn.
                                    else
                                    {
                                        if (foundAt)
                                        {
                                            while (st.Text[j - dis - 1] == '.' || st.Text[j - dis - 1] == ',')
                                            {
                                                st.Text = st.Text.Remove(j - dis - 1, 1);
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(j - dis, "'>");
                                            dis = 0;
                                            while (st.Text[st.Text.Length - dis - 1] == '.' || st.Text[st.Text.Length - dis - 1] == ',')
                                            {
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(st.Text.Length - dis, "</a>");
                                            foundAt = false;
                                            dis = 0;
                                        }
                                    } //<a href='r'@ridwankamil 
                                }
                            }
                            else
                            {
                                finish = true;
                                j = 0;
                            }
                        }

                        if (finish)
                        {
                            if (Char.ToLower(key[m].str[k]) != Char.ToLower(st.Text[j]))
                            {
                                if (k == 0)
                                { //if mismatch from start, it will cause the text shift by one character to right, like bruteforce
                                    j++;
                                }
                                else if (k > 0)
                                {
                                    k -= k - ListOfBorder[m][k - 1];
                                }
                            }
                            else
                            {
                                k++;
                                j++;
                            }
                            // if k = length of keyword then insert tweet data to list of category, and quit from loop
                            if (k == key[m].str.Length)
                            {
                                if (key[m].id == 0)
                                {
                                    cat[key[m].id].Add(st);
                                }
                                else if (key[m].id == 1)
                                {
                                    cat[key[m].id].Add(st);
                                }
                                found = true;
                            }
                            else
                            {
                                //if k = length of keyword, j = length of tweet and m < Length of list of key -1 than insert it to unknown category
                                if (!found && j == st.Text.Length && m < key.Count - 1)
                                {
                                    m++;
                                    j = 0;
                                }

                                //if k = length of keyword and j = length of tweet m < Length of list of key -1 than insert it to unknown category
                                else if (!found && j == st.Text.Length && m == key.Count - 1)
                                {
                                    cat[bound - 1].Add(st);
                                }
                            }
                        }
                    }
                    i++;
                }
            }

            //IS : pattern not an empty value
            //FS : return the lastest position of character c in pattern, if not found return UNDEF
            int BoyerMoorePreProcess(string pat, char c)
            {
                int returnvalue = UNDEF;
                for (int i = 0; i < pat.Length; i++)
                { //abcd abacab
                    if (Char.ToLowerInvariant(pat[i]) == c)
                    {
                        returnvalue = i;
                    }
                }
                return returnvalue;
            }

            //main algorithm of Boyer Moore
            //IS : text and pattern is not empty
            //FS : return the indexes of text which are matching with pattern
            public void BoyerMoore()
            {
                for (int ct = 0; ct < bound; ct++)
                {
                    cat.Add(new List<Status>());
                }
                //initialization arrOfIndex with undef value
                int i = 0;
                string href = "<a target='blank' href='http://twitter.com/";
                while (i < tweets.Count)
                {
                    int jold = 0;
                    bool found = false;
                    Status st = new Status();
                    st = tweets[i];
                    int j, k, m = 0;
                    j = 0;
                    k = key[m].str.Length - 1;
                    bool finish = false;
                    bool foundAt = false;
                    int dis = 0;
                    while (j < st.Text.Length && !found)
                    {
                        if (!finish)
                        {
                            if (j < st.Text.Length - 1)
                            {
                                if ((st.Text[j] == '@' || st.Text[j] == '#') && !foundAt)
                                {
                                    if (st.Text[j] == '@')
                                    {
                                        st.Text = st.Text.Insert(j, href);
                                        j += href.Length;
                                    }
                                    else
                                    {
                                        st.Text = st.Text.Insert(j, href);
                                        j += href.Length - 1;
                                        dis--;
                                    }
                                    foundAt = true;
                                }
                                else
                                {
                                    j++;
                                }
                                if (foundAt)
                                {
                                    if (j < st.Text.Length - 1)
                                    {
                                        if (st.Text[j + 1] != ' ' && st.Text[j + 1] != ':' && st.Text[j + 1] != '\n')
                                        {
                                            st.Text = st.Text.Insert(j - dis, st.Text[j + 1].ToString());
                                            j++;
                                            dis++;
                                        }
                                        else
                                        {
                                            while (st.Text[j - dis - 1] == '.' || st.Text[j - dis - 1] == ',')
                                            {
                                                st.Text = st.Text.Remove(j - dis - 1, 1);
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(j - dis, "'>");
                                            j += 2;
                                            dis = 0;
                                            while (st.Text[j - dis] == ' ' || st.Text[j - dis] == '.' || st.Text[j - dis] == ',')
                                            {
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(j - dis + 1, "</a>");
                                            j += "</a>".Length;
                                            foundAt = false;
                                            dis = 0;

                                        }
                                    } // <a target='blank' href='@nawe_mn.
                                    else
                                    {
                                        if (foundAt)
                                        {
                                            while (st.Text[j - dis - 1] == '.' || st.Text[j - dis - 1] == ',')
                                            {
                                                st.Text = st.Text.Remove(j - dis - 1, 1);
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(j - dis, "'>");
                                            dis = 0;
                                            while (st.Text[st.Text.Length - dis - 1] == '.' || st.Text[st.Text.Length - dis - 1] == ',')
                                            {
                                                dis++;
                                            }
                                            st.Text = st.Text.Insert(st.Text.Length - dis, "</a>");
                                            foundAt = false;
                                            dis = 0;
                                        }
                                    } //<a href='r'@ridwankamil 
                                }
                            }
                            else
                            {
                                finish = true;
                                j = key[m].str.Length - 1;
                            }
                        }
                        if (finish)
                        {
                            if (Char.ToLowerInvariant(st.Text[j]) == Char.ToLowerInvariant(key[m].str[k]))
                            {
                                if (k == 0)
                                {
                                    if (key[m].id == 0)
                                    {
                                        cat[0].Add(st);
                                    }
                                    else if (key[m].id == 1)
                                    {
                                        cat[1].Add(st);
                                    }
                                    found = true;
                                    i++;
                                }
                                j--;
                                k--;
                                jold++;
                            }
                            else
                            {
                                if (BoyerMoorePreProcess((key[m].str), Char.ToLowerInvariant(st.Text[j])) != -1)
                                {
                                    if (k > BoyerMoorePreProcess(key[m].str, Char.ToLowerInvariant(st.Text[j])))
                                    { //case 1
                                        j += key[m].str.Length - BoyerMoorePreProcess(key[m].str, Char.ToLowerInvariant(st.Text[j])) - 1;
                                        k = key[m].str.Length - 1;
                                    }
                                    else
                                    { //case 2
                                        j += jold + 1;
                                        k = key[m].str.Length - 1;
                                    }
                                }
                                else
                                { //case 3. IF x is not exist on pattern
                                    j += key[m].str.Length;
                                    k = key[m].str.Length - 1;
                                }
                                jold = 0;
                            }

                            if (j >= st.Text.Length && !found && m < key.Count - 1)
                            {
                                m++;
                                j = key[m].str.Length - 1;
                                k = key[m].str.Length - 1;
                                jold = 0;
                            }
                            else if (j >= st.Text.Length && !found && m == key.Count - 1)
                            {
                                i++;
                                cat[bound - 1].Add(st);
                            }
                        }
                    }
                }
            }

            // Prosedur untuk mengubah link pada tweet menjadi click-able
            public void addLink()
            {
                int i = 0;
                string link = "http"; //kata kunci link: "http"
                string href = "<a target='blank' href='";
                while (i < tweets.Count)
                {
                    int jold = 0;
                    Status st = new Status();
                    st = tweets[i];
                    int j, k, dis = 0;
                    j = link.Length - 1;
                    k = link.Length - 1;
                    bool foundHttp = false;
                    while (j < st.Text.Length)
                    {
                        if (Char.ToLowerInvariant(st.Text[j]) == Char.ToLowerInvariant(link[k]))
                        {
                            if (k == 0)
                            {
                                if (!foundHttp)
                                {
                                    tweets[i].Text = tweets[i].Text.Insert(j, href);
                                    j += href.Length;
                                    foundHttp = true;
                                }
                                if (foundHttp)
                                {
                                    bool fin = false;
                                    while (st.Text[j - 1] != ' ' && j < st.Text.Length && st.Text[j - 1] != '\n')
                                    {
                                        tweets[i].Text = tweets[i].Text.Insert(j - dis, st.Text[j].ToString());
                                        j += 2;
                                        dis++;
                                        fin = true;
                                    }
                                    if (fin)
                                    {
                                        tweets[i].Text = tweets[i].Text.Insert(j - dis, "'>");
                                        j += 1;
                                        dis = 0;
                                        while (st.Text[j - dis - 1] == '.' || st.Text[j - dis - 1] == ',')
                                        {
                                            dis++;
                                        }
                                        tweets[i].Text = tweets[i].Text.Insert(j - dis + 1, "</a>");
                                        foundHttp = false;
                                    }
                                }
                            }
                            else
                            {
                                j--;
                                k--;
                                jold++;
                            }
                        }
                        else
                        {
                            if (BoyerMoorePreProcess((link), Char.ToLowerInvariant(st.Text[j])) != -1)
                            {
                                if (k > BoyerMoorePreProcess(link, Char.ToLowerInvariant(st.Text[j])))
                                { //case 1
                                    j += link.Length - BoyerMoorePreProcess(link, Char.ToLowerInvariant(st.Text[j])) - 1;
                                    k = link.Length - 1;
                                }
                                else
                                { //case 2
                                    j += jold + 1;
                                    k = link.Length - 1;
                                }
                            }
                            else
                            { //case 3. IF x is not exist on pattern
                                j += link.Length;
                                k = link.Length - 1;
                            }
                            jold = 0;
                        }
                    }
                    i++;
                }
            }
        }

        // Load the main page
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptResourceDefinition myScriptResDef = new ScriptResourceDefinition();
            myScriptResDef.Path = "~/Scripts/jquery-1.10.2.js";
            myScriptResDef.DebugPath = "~/Scripts/jquery-1.10.2.js";
            myScriptResDef.CdnPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.2.min.js";
            myScriptResDef.CdnDebugPath = "http://ajax.microsoft.com/ajax/jQuery/jquery-1.4.2.js";
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", null, myScriptResDef);

            Literal1.Text = "";
            Literal2.Text = "";
            Literal3.Text = "";
            Literal4.Mode = LiteralMode.Encode;
            Literal4.Mode = LiteralMode.PassThrough;

            Literal5.Mode = LiteralMode.PassThrough;
            Literal6.Mode = LiteralMode.PassThrough;
            Literal7.Mode = LiteralMode.PassThrough;
            Literal8.Mode = LiteralMode.PassThrough;
            Literal9.Mode = LiteralMode.PassThrough;
            Literal10.Mode = LiteralMode.PassThrough;
            Literal4.Text = "<a href = '#Launch' class='page-scroll btn btn-xl'>GETTING STARTED</a>";
            Literal5.Text = "";
            Literal6.Text = "";
            Literal7.Text = "";
            Literal8.Text = "";
            Literal9.Text = "";
            Literal10.Text = "";

        }

        //Fungsi yang mengembalikan lokasi yang terdeteksi dari setiap tweet
        String getLocationFromTweet(String Input_Tweet)
        {
            String Tweet = Input_Tweet.ToLower();
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9 -]");
            Tweet = rgx.Replace(Tweet, "");
            String result = "";
            String _di = " di "; //kata kunci yang menandakan lokasi: "di"
            int init_pos = Tweet.IndexOf(_di);
            if (init_pos != -1)
            {
                int last_temp = Tweet.IndexOf(' ', init_pos + 5);
                String first_word;
                if (last_temp == -1)
                {
                    last_temp = Tweet.Length - 1;
                }
                first_word = Tweet.Substring(init_pos + 4, last_temp - init_pos - 3);

                // Check whether the first word contain 'keyword' about geographical place
                if (kind_of_place.IndexOf(first_word) != -1 && exceptional_keyword.IndexOf(first_word) == -1)
                {
                    //if true, take 2 words
                    int init_second_pos = Tweet.IndexOf(first_word);
                    int last_second_pos = Tweet.IndexOf(' ', init_second_pos + first_word.Length + 1);
                    String second_word = "";
                    if (last_second_pos == -1)
                    {
                        last_second_pos = Tweet.Length - 1;

                    }
                    second_word = Tweet.Substring(init_second_pos + first_word.Length, last_second_pos - init_second_pos - first_word.Length + 1);
                    // ACRONYM and SPECIAL PLACE HANDLER (HARDCODE WKWKWK)
                    String kata_sekitar = "depan belakang sekitaran luar";
                    if (first_word == "kwarcab")
                        first_word = "Kwartir Cabang";
                    else if (first_word == "rumah" && second_word == "sakit")
                    {
                        if (last_second_pos != Tweet.Length - 1)
                        {
                            int last_third = Tweet.IndexOf(' ', last_second_pos + 1 + second_word.Length + 1);
                            if (last_third != -1)
                            {
                                String temp = Tweet.Substring(last_second_pos + 1 + second_word.Length, last_second_pos + 1 - second_word.Length + 1);
                                second_word += temp;
                            }
                            else
                            {
                                first_word = "";
                                second_word = "-";
                            }
                        }

                    }
                    else if (first_word == "kota" && (second_word == "orang" || second_word == "lain"))
                    {
                        first_word = "";
                        second_word = "-";

                    }
                    else if (kata_sekitar.IndexOf(first_word) != -1)
                    {
                        if ( kind_of_place.IndexOf(second_word) != -1)
                        {
                            int last_third = Tweet.IndexOf(' ', last_second_pos + 1 + second_word.Length + 1);
                            if (last_third != -1)
                            {
                                String temp = Tweet.Substring(last_second_pos + 1 + second_word.Length, last_second_pos + 1 - second_word.Length + 1);
                                second_word += temp;
                            }
                            else
                            {
                                first_word = "";
                                second_word = "-";
                            }
                        }
                        else
                        {
                            first_word = "";
                            second_word = "-";
                        }
                    }

                    result = first_word + second_word;
                }
                else if (exceptional_keyword.IndexOf(first_word) != -1)
                {
                    result = "-";
                }
                else
                {
                    //take 1 word only
                    result = first_word;
                }
            }
            else
            {
                result = "-";
            }
            return result;
        }

        //Prosedur untuk menampilkan seluruh tweet menurut kategorinya
        void printTweet()
        {
            for (int i = 0; i < 6; i++)
            {
                //DBMP
                if (i == 0)
                {
                    Literal3.Text += "<p class='item-intro text-muted'>Jumlah tweets: " + tw.getcat()[i].Count + "</p>";
                    for (int j = 0; j < tw.getcat()[i].Count; j++)
                    {
                        if (tw.getcat()[i][j] != null)
                        {
                            if (j % 2 == 0)
                            {
                                Literal3.Text += "<div class='row bg-darkest-gray'><font color='white'> <br />";
                            }
                            String loc = getLocationFromTweet(Convert.ToString(tw.getcat()[i][j].Text));
                            Literal3.Text += "<div class = 'col-sm-2' align='left'><img class = 'img-rounded img-centered ' src=" + Convert.ToString(tw.getcat()[i][j].User.ProfileImageUrlHttps) + " > </img>";
                            Literal3.Text += "<a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "/status/" + tw.getcat()[i][j].StatusID + "' target='_blank'> <button type='button' class='btn btn-primary btn-xs'>View Tweet</button></a><br /><br />";
                            if (loc != "-")
                            {
                                Literal3.Text += "<button type='button' class='btn btn-primary btn-xs' data-toggle='collapse' data-target='#loc" + Convert.ToString(j + 1) + "'>Show Location</button>";
                                Literal3.Text += "<div id='loc" + Convert.ToString(j + 1) + "' class='collapse'>";
                                Literal3.Text += "<iframe width = '400' height = '300' frameborder = '0' style = 'border:0'";
                                Literal3.Text += " src = 'https://www.google.com/maps/embed/v1/place?key=AIzaSyCXP63tH_vO05b5LJW0rqCEjAb8afNQEJs&q=";
                                for (int counter = 0; counter < loc.Length; counter++)
                                {
                                    if (loc[counter] == ' ')
                                    {
                                        Literal3.Text += "+";
                                    }
                                    else
                                    {
                                        Literal3.Text += loc[counter];
                                    }
                                }
                                Literal3.Text += ",Bandung+Indonesia";
                                Literal3.Text += "' allowfullscreen></iframe>";
                                Literal3.Text += "</div>";
                            }
                            Literal3.Text += "</div>";
                            Literal3.Text += "<div class = 'col-sm-4' align='left'><p><strong>User :</strong> <a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "' target='_blank'>@" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "</a>" + "</p><p> <strong>Tweet :</strong> " + Convert.ToString(tw.getcat()[i][j].Text) + "</p>";
                            Literal3.Text += "</div>";
                            if (j % 2 == 1 || j == tw.getcat()[i].Count - 1)
                            {
                                Literal3.Text += "</font></div> <br />";
                            }
                        }
                        else
                        {
                            break;
                        }

                    }
                    if (tw.getcat()[i].Count == 0)
                    {
                        Literal3.Text = "<p>Tidak ada tweet untuk Dinas ini</p>";
                    }
                }
                //Dinas Pendidikan
                else if (i == 1)
                {
                    Literal6.Text += "<p class='item-intro text-muted'>Jumlah tweets: " + tw.getcat()[i].Count + "</p>";
                    for (int j = 0; j < tw.getcat()[i].Count; j++)
                    {
                        if (tw.getcat()[i][j] != null)
                        {
                            if (j % 2 == 0)
                            {
                                Literal6.Text += "<div class='row bg-darkest-gray'><font color='white'> <br />";
                            }
                            String loc = getLocationFromTweet(Convert.ToString(tw.getcat()[i][j].Text));
                            Literal6.Text += "<div class = 'col-sm-2' align='left'><img class = 'img-rounded img-centered ' src=" + Convert.ToString(tw.getcat()[i][j].User.ProfileImageUrlHttps) + " > </img>";
                            Literal6.Text += "<a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "/status/" + tw.getcat()[i][j].StatusID + "' target='_blank'> <button type='button' class='btn btn-primary btn-xs'>View Tweet</button></a><br /><br />";
                            if (loc != "-")
                            {
                                Literal6.Text += "<button type='button' class='btn btn-primary btn-xs' data-toggle='collapse' data-target='#loc" + Convert.ToString(j + 1) + "'>Show Location</button>";
                                Literal6.Text += "<div id='loc" + Convert.ToString(j + 1) + "' class='collapse'>";
                                Literal6.Text += "<iframe width = '400' height = '300' frameborder = '0' style = 'border:0'";
                                Literal6.Text += " src = 'https://www.google.com/maps/embed/v1/place?key=AIzaSyCXP63tH_vO05b5LJW0rqCEjAb8afNQEJs&q=";
                                for (int counter = 0; counter < loc.Length; counter++)
                                {
                                    if (loc[counter] == ' ')
                                    {
                                        Literal6.Text += "+";
                                    }
                                    else
                                    {
                                        Literal6.Text += loc[counter];
                                    }
                                }
                                Literal6.Text += ",Bandung+Indonesia";
                                Literal6.Text += "' allowfullscreen></iframe>";
                                Literal6.Text += "</div>";
                            }
                            Literal6.Text += "</div>";
                            Literal6.Text += "<div class = 'col-sm-4' align='left'><p><strong>User :</strong> <a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "' target='_blank'>@" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "</a>" + "</p><p><strong> Tweet :</strong> " + Convert.ToString(tw.getcat()[i][j].Text) + "</p>";
                            Literal6.Text += "</div>";
                            if (j % 2 == 1 || j == tw.getcat()[i].Count - 1)
                            {
                                Literal6.Text += "</font></div> <br />";
                            }
                        }
                        else
                            break;
                    }
                    if (tw.getcat()[i].Count == 0)
                    {
                        Literal6.Text = "<p>Tidak ada tweet untuk Dinas ini</p>";
                    }
                }
                //Dinas Kebakaran
                else if (i == 2)
                {
                    Literal9.Text += "<p class='item-intro text-muted'>Jumlah tweets: " + tw.getcat()[i].Count + "</p>";
                    for (int j = 0; j < tw.getcat()[i].Count; j++)
                    {
                        if (tw.getcat()[i][j] != null)
                        {
                            if (j % 2 == 0)
                            {
                                Literal9.Text += "<div class='row bg-darkest-gray'><font color='white'> <br />";
                            }
                            String loc = getLocationFromTweet(Convert.ToString(tw.getcat()[i][j].Text));
                            Literal9.Text += "<div class = 'col-sm-2' align='left'><img class = 'img-rounded img-centered ' src=" + Convert.ToString(tw.getcat()[i][j].User.ProfileImageUrlHttps) + " > </img>";
                            Literal9.Text += "<a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "/status/" + tw.getcat()[i][j].StatusID + "' target='_blank'> <button type='button' class='btn btn-primary btn-xs'>View Tweet</button></a><br /><br />";
                            if (loc != "-")
                            {
                                Literal9.Text += "<button type='button' class='btn btn-primary btn-xs' data-toggle='collapse' data-target='#loc" + Convert.ToString(j + 1) + "'>Show Location</button>";
                                Literal9.Text += "<div id='loc" + Convert.ToString(j + 1) + "' class='collapse'>";
                                Literal9.Text += "<iframe width = '400' height = '300' frameborder = '0' style = 'border:0'";
                                Literal9.Text += " src = 'https://www.google.com/maps/embed/v1/place?key=AIzaSyCXP63tH_vO05b5LJW0rqCEjAb8afNQEJs&q=";
                                for (int counter = 0; counter < loc.Length; counter++)
                                {
                                    if (loc[counter] == ' ')
                                    {
                                        Literal9.Text += "+";
                                    }
                                    else
                                    {
                                        Literal9.Text += loc[counter];
                                    }
                                }
                                Literal9.Text += ",Bandung+Indonesia";
                                Literal9.Text += "' allowfullscreen></iframe>";
                                Literal9.Text += "</div>";
                            }
                            Literal9.Text += "</div>";
                            Literal9.Text += "<div class = 'col-sm-4' align='left'><p><strong>User :</strong> <a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "' target='_blank'>@" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "</a>" + "</p><p> <strong>Tweet : </strong>" + Convert.ToString(tw.getcat()[i][j].Text) + "</p>";
                            Literal9.Text += "</div>";
                            if (j % 2 == 1 || j == tw.getcat()[i].Count - 1)
                            {
                                Literal9.Text += "</font></div> <br />";
                            }
                        }
                        else
                            break;
                    }
                    if (tw.getcat()[i].Count == 0)
                    {
                        Literal9.Text = "<p>Tidak ada tweet untuk Dinas ini</p>";
                    }
                }
                //PDAM
                else if (i == 3)
                {
                    Literal10.Text += "<p class='item-intro text-muted'>Jumlah tweets: " + tw.getcat()[i].Count + "</p>";
                    for (int j = 0; j < tw.getcat()[i].Count; j++)
                    {
                        if (tw.getcat()[i][j] != null)
                        {
                            if (j % 2 == 0)
                            {
                                Literal10.Text += "<div class='row bg-darkest-gray'><font color='white'> <br />";
                            }
                            String loc = getLocationFromTweet(Convert.ToString(tw.getcat()[i][j].Text));
                            Literal10.Text += "<div class = 'col-sm-2' align='left'><img class = 'img-rounded img-centered ' src=" + Convert.ToString(tw.getcat()[i][j].User.ProfileImageUrlHttps) + " > </img>";
                            Literal10.Text += "<a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "/status/" + tw.getcat()[i][j].StatusID + "' target='_blank'> <button type='button' class='btn btn-primary btn-xs'>View Tweet</button></a><br /><br />";
                            if (loc != "-")
                            {
                                Literal10.Text += "<button type='button' class='btn btn-primary btn-xs' data-toggle='collapse' data-target='#loc" + Convert.ToString(j + 1) + "'>Show Location</button>";
                                Literal10.Text += "<div id='loc" + Convert.ToString(j + 1) + "' class='collapse'>";
                                Literal10.Text += "<iframe width = '400' height = '300' frameborder = '0' style = 'border:0'";
                                Literal10.Text += " src = 'https://www.google.com/maps/embed/v1/place?key=AIzaSyCXP63tH_vO05b5LJW0rqCEjAb8afNQEJs&q=";
                                for (int counter = 0; counter < loc.Length; counter++)
                                {
                                    if (loc[counter] == ' ')
                                    {
                                        Literal10.Text += "+";
                                    }
                                    else
                                    {
                                        Literal10.Text += loc[counter];
                                    }
                                }
                                Literal10.Text += ",Bandung+Indonesia";
                                Literal10.Text += "' allowfullscreen></iframe>";
                                Literal10.Text += "</div>";
                            }
                            Literal10.Text += "</div>";
                            Literal10.Text += "<div class = 'col-sm-4' align='left'><p><strong>User :</strong> <a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "' target='_blank'>@" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "</a>" + "</p><p> <strong>Tweet :</strong> " + Convert.ToString(tw.getcat()[i][j].Text) + "</p>";
                            Literal10.Text += "</div>";
                            if (j % 2 == 1 || j == tw.getcat()[i].Count - 1)
                            {
                                Literal10.Text += "</font></div> <br />";
                            }
                        }
                        else
                            break;
                    }

                    if (tw.getcat()[i].Count == 0)
                    {
                        Literal10.Text = "<p>Tidak ada tweet untuk Departemen ini</p>";
                    }
                }
                //Dinas Perhubungan
                else if (i == 4)
                {
                    Literal8.Text += "<p class='item-intro text-muted'>Jumlah tweets: " + tw.getcat()[i].Count + "</p>";
                    for (int j = 0; j < tw.getcat()[i].Count; j++)
                    {
                        if (tw.getcat()[i][j] != null)
                        {
                            if (j % 2 == 0)
                            {
                                Literal8.Text += "<div class='row bg-darkest-gray'><font color='white'> <br />";
                            }
                            String loc = getLocationFromTweet(Convert.ToString(tw.getcat()[i][j].Text));
                            Literal8.Text += "<div class = 'col-sm-2' align='left'><img class = 'img-rounded img-centered ' src=" + Convert.ToString(tw.getcat()[i][j].User.ProfileImageUrlHttps) + " > </img>";
                            Literal8.Text += "<a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "/status/" + tw.getcat()[i][j].StatusID + "' target='_blank'> <button type='button' class='btn btn-primary btn-xs'>View Tweet</button></a><br /><br />";
                            if (loc != "-")
                            {
                                Literal8.Text += "<button type='button' class='btn btn-primary btn-xs' data-toggle='collapse' data-target='#loc" + Convert.ToString(j + 1) + "'>Show Location</button>";
                                Literal8.Text += "<div id='loc" + Convert.ToString(j + 1) + "' class='collapse'>";
                                Literal8.Text += "<iframe width = '400' height = '300' frameborder = '0' style = 'border:0'";
                                Literal8.Text += " src = 'https://www.google.com/maps/embed/v1/place?key=AIzaSyCXP63tH_vO05b5LJW0rqCEjAb8afNQEJs&q=";
                                for (int counter = 0; counter < loc.Length; counter++)
                                {
                                    if (loc[counter] == ' ')
                                    {
                                        Literal8.Text += "+";
                                    }
                                    else
                                    {
                                        Literal8.Text += loc[counter];
                                    }
                                }
                                Literal8.Text += ",Bandung+Indonesia";
                                Literal8.Text += "' allowfullscreen></iframe>";
                                Literal8.Text += "</div>";
                            }
                            Literal8.Text += "</div>";
                            Literal8.Text += "<div class = 'col-sm-4' align='left'><p><strong>User :</strong> <a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "' target='_blank'>@" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "</a>" + "</p><p> <strong>Tweet :</strong> " + Convert.ToString(tw.getcat()[i][j].Text) + "</p>";
                            Literal8.Text += "</div>";
                            if (j % 2 == 1 || j == tw.getcat()[i].Count - 1)
                            {
                                Literal8.Text += "</font></div> <br />";
                            }
                        }
                        else
                            break;
                    }

                    if (tw.getcat()[i].Count == 0)
                    {
                        Literal8.Text = "<p>Tidak ada tweet untuk Dinas ini</p>";
                    }
                }
                //Unknown
                else if (i == 5)
                {
                    Literal7.Text += "<p class='item-intro text-muted'>Jumlah tweets: " + tw.getcat()[i].Count + "</p>";
                    for (int j = 0; j < tw.getcat()[i].Count; j++)
                    {
                        if (tw.getcat()[i][j] != null)
                        {
                            if (j % 2 == 0)
                            {
                                Literal7.Text += "<div class='row bg-darkest-gray'><font color='white'> <br />";
                            }
                            String loc = getLocationFromTweet(Convert.ToString(tw.getcat()[i][j].Text));
                            Literal7.Text += "<div class = 'col-sm-2' align='left'><img class = 'img-rounded img-centered ' src=" + Convert.ToString(tw.getcat()[i][j].User.ProfileImageUrlHttps) + " > </img>";
                            Literal7.Text += "<a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "/status/" + tw.getcat()[i][j].StatusID + "' target='_blank'> <button type='button' class='btn btn-primary btn-xs'>View Tweet</button></a><br /><br />";
                            if (loc != "-")
                            {
                                Literal7.Text += "<button type='button' class='btn btn-primary btn-xs' data-toggle='collapse' data-target='#loc" + Convert.ToString(j + 1) + "'>Show Location</button>";
                                Literal7.Text += "<div id='loc" + Convert.ToString(j + 1) + "' class='collapse'>";
                                Literal7.Text += "<iframe width = '400' height = '300' frameborder = '0' style = 'border:0'";
                                Literal7.Text += " src = 'https://www.google.com/maps/embed/v1/place?key=AIzaSyCXP63tH_vO05b5LJW0rqCEjAb8afNQEJs&q=";
                                for (int counter = 0; counter < loc.Length; counter++)
                                {
                                    if (loc[counter] == ' ')
                                    {
                                        Literal7.Text += "+";
                                    }
                                    else
                                    {
                                        Literal7.Text += loc[counter];
                                    }
                                }
                                Literal7.Text += ",Bandung+Indonesia";
                                Literal7.Text += "' allowfullscreen></iframe>";
                                Literal7.Text += "</div>";
                            }
                            Literal7.Text += "</div>";
                            Literal7.Text += "<div class = 'col-sm-4' align='left'><p><strong>User :</strong> <a href='http://twitter.com/" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "' target='_blank'>@" + Convert.ToString(tw.getcat()[i][j].User.ScreenNameResponse) + "</a>" + "</p><p> <strong>Tweet :</strong> " + Convert.ToString(tw.getcat()[i][j].Text) + "</p>";
                            Literal7.Text += "</div>";
                            if (j % 2 == 1 || j == tw.getcat()[i].Count - 1)
                            {
                                Literal7.Text += "</font></div> <br />";
                            }
                        }
                        else
                            break;
                    }

                    if (tw.getcat()[i].Count == 0)
                    {
                        Literal7.Text = "<p>Tidak ada tweet untuk Departemen ini</p>";
                    }
                }
            }
        }

        //Prosedur untuk memulai string matching
        protected void Run_Click(object sender, EventArgs e)
        {
            //Seluruh text box harus terisi
            if (IsValid && TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "" && TextBox4.Text != "" && TextBox5.Text != "" && TextBox6.Text != "")
            {
                Literal4.Text = "<a href = '#portfolio' class='page-scroll btn btn-xl'>SHOW RESULT</a>";
                Literal5.Text = "<li> <a class='page-scroll' href='#portfolio'>Result</a></li>";
                //Keyword twitter
                string input = Convert.ToString(TextBox1.Text);
                
                //Pencarian tweet berdasarkan keyword yang telah dimasukkan
                tw.SearchTweets(input);

                List<string> lstring = new List<string>();
                //Keyword untuk textbox2: DBMP
                string keyword = Convert.ToString(TextBox2.Text);
                if (keyword.LastIndexOf(';') != keyword.Length - 1)
                {
                    keyword += ";";
                }
                lstring.Add(keyword);
                //Keyword untuk textbox3: Dinas Pendidikan
                keyword = Convert.ToString(TextBox3.Text);
                if (keyword.LastIndexOf(';') != keyword.Length - 1)
                {
                    keyword += ";";
                }
                lstring.Add(keyword);
                //Keyword untuk textbox4: Dinas Kebakaran
                keyword = Convert.ToString(TextBox4.Text);
                if (keyword.LastIndexOf(';') != keyword.Length - 1)
                {
                    keyword += ";";
                }
                lstring.Add(keyword);
                //Keyword untuk textbox5: PDAM
                keyword = Convert.ToString(TextBox5.Text);
                if (keyword.LastIndexOf(';') != keyword.Length - 1)
                {
                    keyword += ";";
                }
                lstring.Add(keyword);
                //Keyword untuk textbox6: DInas Perhubungan
                keyword = Convert.ToString(TextBox6.Text);
                if (keyword.LastIndexOf(';') != keyword.Length - 1)
                {
                    keyword += ";";
                }
                lstring.Add(keyword);
                tw.setKeyword(lstring);
                tw.addLink(); //menambahkan link untuk seluruh tweets

                Literal2.Text += "                <section id = 'portfolio' class='bg-light-gray'>";
                Literal2.Text += "        <div class='container'>";
                Literal2.Text += "            <div class='row'>";
                Literal2.Text += "<div class='col-lg-12 text-center'>";
                Literal2.Text += "<h2 class='section-heading'>Result</h2>";
                //Memilih metode pencarian: Knuth-Morris-Pratt
                if (radioB1.Checked)
                {
                    Literal2.Text += "<h3 class='section-subheading text-muted'>This is the result of Knuth - Morris - Pratt Algorithm</h3>";

                    tw.kmp();
                }
                //Memilih metode pencarian: Boyer Moore
                else if (radioB2.Checked)
                {
                    Literal2.Text += "<h3 class='section-subheading text-muted'>This is the result of Boyer Moore Algorithm</h3>";

                    tw.BoyerMoore();
                }
                //Default metode pencarian: Knuth-Morris-Pratt
                else
                {
                    Literal2.Text += "<h3 class='section-subheading text-muted'>This is the result of Knuth - Morris - Pratt Algorithm <br /> Chosen by default</h3>";

                    tw.kmp();
                }
                Literal2.Text += "    </div>";
                Literal2.Text += "      </div>";
                Literal2.Text += "       <div class='row'>";

                //DBMP
                Literal2.Text += "          <div class='col-md-4 col-sm-6 portfolio-item'>";
                Literal2.Text += "               <a href = '#portfolioModal1' class='portfolio-link' data-toggle='modal'>";
                Literal2.Text += "            <div class='portfolio-hover'>";
                Literal2.Text += "                       <div class='portfolio-hover-content'>";
                Literal2.Text += "                <i class='fa fa-plus fa-3x'></i>";
                Literal2.Text += "            </div>";
                Literal2.Text += "         </div>";
                Literal2.Text += "         <img src = 'img/portfolio/dbmp.jpg' class='img-responsive' alt=''>";
                Literal2.Text += "     </a>";
                Literal2.Text += "    <div class='portfolio-caption'>";
                Literal2.Text += "       <h4>DBMP</h4>";
                Literal2.Text += "      <p class='text-muted'>Tweets untuk Dinas Bina Marga dan Pengairan Kota Bandung</p>";
                Literal2.Text += "    </div>";
                Literal2.Text += " </div>";

                //Dinas Pendidikan
                Literal2.Text += "<div class='col-md-4 col-sm-6 portfolio-item'>";
                Literal2.Text += "    <a href = '#portfolioModal2' class='portfolio-link' data-toggle='modal'>";
                Literal2.Text += "       <div class='portfolio-hover'>";
                Literal2.Text += "           <div class='portfolio-hover-content'>";
                Literal2.Text += "               <i class='fa fa-plus fa-3x'></i>";
                Literal2.Text += "          </div>";
                Literal2.Text += "      </div>";
                Literal2.Text += "      <img src = 'img/portfolio/disdik.jpg' class='img-responsive' alt=''>";
                Literal2.Text += "   </a>";
                Literal2.Text += "  <div class='portfolio-caption'>";
                Literal2.Text += "      <h4>Dinas Pendidikan</h4>";
                Literal2.Text += "     <p class='text-muted'>Tweets untuk Dinas Pendidikan Kota Bandung</p>";
                Literal2.Text += "  </div>";
                Literal2.Text += "   </div>";

                //Dinas Kebakaran
                Literal2.Text += "<div class='col-md-4 col-sm-6 portfolio-item'>";
                Literal2.Text += "    <a href = '#portfolioModal3' class='portfolio-link' data-toggle='modal'>";
                Literal2.Text += "       <div class='portfolio-hover'>";
                Literal2.Text += "           <div class='portfolio-hover-content'>";
                Literal2.Text += "               <i class='fa fa-plus fa-3x'></i>";
                Literal2.Text += "          </div>";
                Literal2.Text += "      </div>";
                Literal2.Text += "      <img src = 'img/portfolio/diskar.jpg' class='img-responsive' alt=''>";
                Literal2.Text += "   </a>";
                Literal2.Text += "  <div class='portfolio-caption'>";
                Literal2.Text += "      <h4>Dinas Kebakaran</h4>";
                Literal2.Text += "     <p class='text-muted'>Tweets untuk Dinas Kebakaran Kota Bandung</p>";
                Literal2.Text += "  </div>";
                Literal2.Text += "   </div>";

                //PDAM
                Literal2.Text += "<div class='col-md-4 col-sm-6 portfolio-item'>";
                Literal2.Text += "    <a href = '#portfolioModal4' class='portfolio-link' data-toggle='modal'>";
                Literal2.Text += "       <div class='portfolio-hover'>";
                Literal2.Text += "           <div class='portfolio-hover-content'>";
                Literal2.Text += "               <i class='fa fa-plus fa-3x'></i>";
                Literal2.Text += "          </div>";
                Literal2.Text += "      </div>";
                Literal2.Text += "      <img src = 'img/portfolio/pdam.jpg' class='img-responsive' alt=''>";
                Literal2.Text += "   </a>";
                Literal2.Text += "  <div class='portfolio-caption'>";
                Literal2.Text += "      <h4>PDAM</h4>";
                Literal2.Text += "     <p class='text-muted'>Tweets untuk PDAM Kota Bandung</p>";
                Literal2.Text += "  </div>";
                Literal2.Text += "   </div>";

                //Dinas Perhubungan
                Literal2.Text += "<div class='col-md-4 col-sm-6 portfolio-item'>";
                Literal2.Text += "    <a href = '#portfolioModal5' class='portfolio-link' data-toggle='modal'>";
                Literal2.Text += "       <div class='portfolio-hover'>";
                Literal2.Text += "           <div class='portfolio-hover-content'>";
                Literal2.Text += "               <i class='fa fa-plus fa-3x'></i>";
                Literal2.Text += "          </div>";
                Literal2.Text += "      </div>";
                Literal2.Text += "      <img src = 'img/portfolio/dishub.jpg' class='img-responsive' alt=''>";
                Literal2.Text += "   </a>";
                Literal2.Text += "  <div class='portfolio-caption'>";
                Literal2.Text += "      <h4>Dinas Perhubungan</h4>";
                Literal2.Text += "     <p class='text-muted'>Tweets untuk Dinas Perhubungan Kota Bandung</p>";
                Literal2.Text += "  </div>";
                Literal2.Text += "   </div>";

                //Unknown
                Literal2.Text += "  <div class='col-md-4 col-sm-6 portfolio-item'>";
                Literal2.Text += "       <a href = '#portfolioModal6' class='portfolio-link' data-toggle='modal'>";
                Literal2.Text += "           <div class='portfolio-hover'>";
                Literal2.Text += "                <div class='portfolio-hover-content'>";
                Literal2.Text += "                  <i class='fa fa-plus fa-3x'></i>";
                Literal2.Text += "             </div>";
                Literal2.Text += "          </div>";
                Literal2.Text += "         <img src = 'img/portfolio/roundicons.png' class='img-responsive' alt=''>";
                Literal2.Text += "     </a>";
                Literal2.Text += "     <div class='portfolio-caption'>";
                Literal2.Text += "          <h4>Unknown</h4>";
                Literal2.Text += "          <p class='text-muted'>Tweets yang tidak cocok untuk departemen manapun</p>";
                Literal2.Text += "    </div>";
                Literal2.Text += "     </div>";
                Literal2.Text += "   </div>";
                Literal2.Text += " </div>";
                Literal2.Text += "   </section>";
                printTweet(); //Mencetak seluruh tweets
            }
        }
    }
}