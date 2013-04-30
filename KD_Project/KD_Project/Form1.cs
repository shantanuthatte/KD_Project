using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using Iveonik.Stemmers;
using LemmaSharp;


namespace KD_Project
{
    
    public partial class Form1 : Form
    {
        public string BaseAddr = "http://en.wikipedia.org/w/index.php?";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private static bool stopWordTest(string s)
        {
            string stopword = "a a's able about above according accordingly across actually after afterwards again against ain't all allow allows almost alone along already also although always am among amongst an and another any anybody anyhow anyone anything anyway anyways anywhere apart appear appreciate appropriate are aren't around as aside ask asking associated at available away awfully b be became because become becomes becoming been before beforehand behind being believe below beside besides best better between beyond both brief but by c c'mon c's came can can't cannot cant cause causes certain certainly changes clearly co com come comes concerning consequently consider considering contain containing contains corresponding could couldn't course currently d definitely described despite did didn't different do does doesn't doing don't done down downwards during e each edu eg eight either else elsewhere enough entirely especially et etc even ever every everybody everyone everything everywhere ex exactly example except f far few fifth first five followed following follows for former formerly forth four from further furthermore g get gets getting given gives go goes going gone got gotten greetings h had hadn't happens hardly has hasn't have haven't having he he's hello help hence her here here's hereafter hereby herein hereupon hers herself hi him himself his hither hopefully how howbeit however i i'd i'll i'm i've ie if ignored immediate in inasmuch inc indeed indicate indicated indicates inner insofar instead into inward is isn't it it'd it'll it's its itself j just k keep keeps kept know knows known l last lately later latter latterly least less lest let let's like liked likely little look looking looks ltd m mainly many may maybe me mean meanwhile merely might more moreover most mostly much must my myself n name namely nd near nearly necessary need needs neither never nevertheless new next nine no nobody non none noone nor normally not nothing novel now nowhere o obviously of off often oh ok okay old on once one ones only onto or other others otherwise ought our ours ourselves out outside over overall own p particular particularly per perhaps placed please plus possible presumably probably provides q que quite qv r rather rd re really reasonably regarding regardless regards relatively respectively right s said same saw say saying says second secondly see seeing seem seemed seeming seems seen self selves sensible sent serious seriously seven several shall she should shouldn't since six so some somebody somehow someone something sometime sometimes somewhat somewhere soon sorry specified specify specifying still sub such sup sure t t's take taken tell tends th than thank thanks thanx that that's thats the their theirs them themselves then thence there there's thereafter thereby therefore therein theres thereupon these they they'd they'll they're they've think third this thorough thoroughly those though three through throughout thru thus to together too took toward towards tried tries truly try trying twice two u un under unfortunately unless unlikely until unto up upon us use used useful uses using usually uucp v value various very via viz vs w want wants was wasn't way we we'd we'll we're we've welcome well went were weren't what what's whatever when whence whenever where where's whereafter whereas whereby wherein whereupon wherever whether which while whither who who's whoever whole whom whose why will willing wish with within without won't wonder would would wouldn't x y yes yet you you'd you'll you're you've your yours yourself yourselves z zero";
            List<string> swords = new List<string>(stopword.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            if (swords.IndexOf(s.ToLower()) >= 0)
                return true;
            else
                return false;
        }

        private static bool commonWordTest(string s)
        {
            string common = "the be and of a in to have to it I that for you he with on do say this they at but we his from that not n't n't by she or as what go their can who get if would her all my make about know will as up one time there year so think when which them some me people take out into just see him your come could now than like other how then its our two more these want way look first also new because day more use no man find here thing give many well only those tell one very her even back any good woman through us life child there work down may after should call world over school still try in as last ask need too feel three when state never become between high really something most another much family own out leave put old while mean on keep student why let great same big group begin seem country help talk where turn problem every start hand might American show part about against place over such again few case most week company where system each right program hear so question during work play government run small number off always move like night live Mr point believe hold today bring happen next without before large all million must home under water room write mother area national money story young fact month different lot right study book eye job word though business issue side kind four head far black long both little house yes after since long provide service around friend important father sit away until power hour game often yet line political end among ever stand bad lose however member pay law meet car city almost include continue set later community much name five once white least president learn real change team minute best several idea kid body information nothing ago right lead social understand whether back watch together follow around parent only stop face anything create public already speak others read level allow add office spend door health person art sure such war history party within grow result open change morning walk reason low win research girl guy early food before moment himself air teacher force offer enough both education across although remember foot second boy maybe toward able age off policy everything love process music including consider appear actually buy probably human wait serve market die send expect home sense build stay fall oh nation plan cut college interest death course someone experience behind reach local kill six remain effect use yeah suggest class control raise care perhaps little late hard field else pass former sell major sometimes require along development themselves report role better economic effort up decide rate strong possible heart drug show leader light voice wife whole police mind finally pull return free military price report less according decision explain son hope even develop view relationship carry town road drive arm true federal break better difference thank receive";
            HashSet<string> hash = new HashSet<string>(common.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            return(hash.Contains(s));
        }

        private void DownloadComplete(Object sender, DownloadStringCompletedEventArgs e)
        {
            //Console.WriteLine(e.Result);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(e.Result);

            var nodes = doc.DocumentNode.SelectNodes("//script|//style|//img|//table|//dl|//sup|//pre|//div[contains(concat(' ',@class,' '),' thumb ')]|//div[@class='dablink']|//div[@id='siteSub']|//div[@id='jump-to-nav']|//div[contains(concat(' ',@class,' '),' metadata ')]|//div[contains(concat(' ',@class,' '),' rellink ')]|//ol[@class='references']|//div[contains(concat(' ',@class,' '),' refbegin ')]|//div[@class='printfooter']|//div[@id='catlinks']|//div[@id='mw-navigation']|//div[@id='footer']");

            foreach (var node in nodes)
                node.ParentNode.RemoveChild(node);

            string htmlOutput = doc.DocumentNode.OuterHtml;

            string paragraph = "Simple computers are small enough to fit into mobile devices, and mobile computers can be powered by small batteries. Personal computers in their various forms are icons of the Information Age and are what most people think of as “computers.” However, the embedded computers found in many devices from MP3 players to fighter aircraft and from toys to industrial robots are the most numerous.";
            //string stopword = "the a";
            string[] words = paragraph.Split(new char[] { ' ', ',', '.', '(', ')', '[', ']', '“', '”', '"' }, StringSplitOptions.RemoveEmptyEntries);

            string[] swords = words.Where(x => !stopWordTest(x)).ToArray();
            List<string> lwords = new List<string>();
            ILemmatizer lemm = new LemmatizerPrebuiltCompact(LanguagePrebuilt.English);
            foreach (string word in swords)
            {
                lwords.Add(lemm.Lemmatize(word.ToLower()));
            }
            List<string> fwords = new List<string>();
            fwords = lwords.Where(x => !commonWordTest(x)).ToList();
            var cwords = lwords.GroupBy(i => i);
            foreach (var w in cwords)
            {
                Console.WriteLine("{0} {1}", w.Key, w.Count());
            }

            Keywords keys = new Keywords();
            for (int i = 0; i < fwords.Count; i++)
            {
                keys.addOcc(fwords[i], i);
            }
            /*List<string> wlist = new List<string>(words);
            List<string> clist = new List<string>();
            //List<string> swords = new List<string>(stopword.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries));
            wlist.RemoveAll(stopWordTest);
            ILemmatizer lemm = new LemmatizerPrebuiltCompact(LemmaSharp.LanguagePrebuilt.English);
            foreach (string word in wlist)
            {
                //Console.WriteLine();
                clist.Add(lemm.Lemmatize(word.ToLower()));
            }
            clist = new List<string>(clist.Except<string>(getCommonHash()));*/

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = BaseAddr;
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadComplete);
            wc.DownloadStringTaskAsync("http://en.wikipedia.org/w/index.php?title=Computer&printable=yes");
            
        }
    }
}
