using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Bencode;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;
using MaxMind.GeoIP2;

namespace BitTorrent_Artifact_Analyzer
{
    public class Comments
    {
        public Comments()
        {
            this.aboutBtDHT = @"about dht.dat  http://www.bittorrent.org/beps/bep_0005.html";
            
            this.aboutBtResume = @"about resume.dat";
            this.aboutBtSettings = @"about settings.dat";
            this.aboutBtTorrent = @"about torrent file http://www.bittorrent.org/beps/bep_0003.html";
        }

        [XmlIgnore]
        public string analyzerStartDateAndTime { get; set; }

        [XmlIgnore]
        public string analyzerEndDateAndTime { get; set; }
        [XmlIgnore]
        public string aboutBtDHT { get; set; }

        [XmlIgnore]
        public string aboutBtResume { get; set; }

        [XmlIgnore]
        public string aboutBtSettings { get; set; }

        [XmlIgnore]
        public string aboutBtTorrent { get; set; }
    }
    public class Users
    {
        public Users()
        {
            this.userDirectoryNames = new List<User>();
        }
        [XmlArray("usersDirectory", Order = 1)]
        [XmlArrayItem("userDirectoryName")]
        public List<User> userDirectoryNames;
    }
    public class User
    {
        [XmlAttribute()]
        public string userDirectoryName { get; set; }
    }

    [XmlRoot(ElementName = "BitTorrent_dht.dat_file_information")]
    public class BtDht
    {
        public BtDht()
        {
            this.nodes = new List<Node>();
        }

        [XmlAttribute()]
        public string dhtFileName { get; set; }

        [XmlElementAttribute(Order = 1)]
        public DateTime dhtFileCreated { get; set; }

        [XmlElementAttribute(Order = 2)]
        public DateTime dhtFileModified { get; set; }

        [XmlElementAttribute(Order = 3)]
        public DateTime dhtFileAccessed { get; set; }

        [XmlElement(Order = 4)]
        public string dhtAgeParameter { get; set; }

        [XmlElement(Order = 5)]
        public string dhtIdParameter { get; set; }

        [XmlElement(Order = 6)]
        public string dhtIpParameter { get; set; }

        [XmlElementAttribute(Order = 7)]
        public int dhtTotalNodesNumber { get; set; }

        [XmlArray("dhtNodes", Order = 8)]
        [XmlArrayItem("dhtNode")]
        public List<Node> nodes;

        [XmlElementAttribute(Order = 9)]
        public int dhtTableDepth { get; set; }
    }
    [XmlRoot(ElementName = "BitTorrent_settings.dat_file_information")]
    public class BtSettings
    {
        public BtSettings()
        {
            this.directoryMaps = new List<DirectoryMap>();
            this.ruleMaps = new List<RuleMap>();
        }

        [XmlAttribute()]
        public string settingsFileName { get; set; }

        [XmlElementAttribute(Order = 1)]
        public DateTime settingsFileCreated { get; set; }

        [XmlElementAttribute(Order = 2)]
        public DateTime settingsFileModified { get; set; }

        [XmlElementAttribute(Order = 3)]
        public DateTime settingsFileAccessed { get; set; }

        [XmlElement(Order = 4)]
        public Int64 settingsBindPortParameter { get; set; }

        [XmlElement(Order = 5)]
        public string settingsGuiLastBundleVisit { get; set; }

        [XmlElement(Order = 6)]
        public string settingsIspPeerPolicyExpy { get; set; }

        [XmlArray("settingsLabelDirectoryMap", Order = 7)]
        [XmlArrayItem("DirectoryMap")]
        public List<DirectoryMap> directoryMaps;

        [XmlArray("settingsLabelRuleMap", Order = 8)]
        [XmlArrayItem("RuleMap")]
        public List<RuleMap> ruleMaps;

        [XmlElementAttribute(Order = 9)]
        public string settingsSavedSystime { get; set; }
    }
    public class DirectoryMap
    {
        [XmlElement]
        public string settingsDirectoryMap { get; set; }

    }
    public class RuleMap
    {
        [XmlElement]
        public string settingsRuleMap { get; set; }

    }
    [XmlRoot(ElementName = "BitTorrent_resume.dat_file_information")]
    public class BtResumeFile
    {
        public BtResumeFile()
        {
            this.files = new List<BtResume>();
        }
        [XmlAttribute()]
        public string resumeFileName { get; set; }

        [XmlElementAttribute(Order = 1)]
        public DateTime resumeFileCreated { get; set; }

        [XmlElementAttribute(Order = 2)]
        public DateTime resumeFileModified { get; set; }

        [XmlElementAttribute(Order = 3)]
        public DateTime resumeFileAccessed { get; set; }

        [XmlArray("resumeDownloadedFiles", Order = 4)]
        [XmlArrayItem("resumeDownloadedFile")]
        public List<BtResume> files;
    }
    public class BtResume
    {
        public BtResume()
        {
            this.peers = new List<Peer>();
            this.trackers = new List<Tracker>();
        }

        [XmlAttribute()]
        public string resumeCaptionParameter { get; set; }

        [XmlAttribute()]
        public string resumePathParameter { get; set; }

        [XmlElement(Order = 1)]
        public string resumeAdded_onParameter { get; set; }

        [XmlElement(Order = 2)]
        public string resumeBlocksizeParameter { get; set; }

        [XmlElementAttribute(Order = 3)]
        public string resumeCompleted_onParameter { get; set; }

        [XmlElementAttribute(Order = 4)]
        public string resumeDhtParameter { get; set; }

        [XmlElementAttribute(Order = 5)]
        public string resumeDownloadedParameter { get; set; }

        [XmlElementAttribute(Order = 6)]
        public string resumeLastSeenCompleteParameter { get; set; }

        [XmlElementAttribute(Order = 7)]
        public int resumePeersNumber { get; set; }

        [XmlArray("resumePeers", Order = 8)]
        [XmlArrayItem("resumePeer")]
        public List<Peer> peers;

        [XmlElementAttribute(Order = 9)]
        public int resumeTotalTrackersNumber { get; set; }

        [XmlArray("resumeTracker_List", Order = 10)]
        [XmlArrayItem("resumeTracker")]
        public List<Tracker> trackers;
    }

    public class Peer
    {
        public Peer()
        {
            this.resumeGeoLite2City = new GeoLite2City();
        }

        [XmlElement]
        public int resumePeer { get; set; }

        [XmlElement]
        public string resumePeerIP { get; set; }

        [XmlElement]
        public string resumePeerHostName { get; set; }

        [XmlElement]
        public uint resumePeerPort { get; set; }

        [XmlElement]
        public GeoLite2City resumeGeoLite2City { get; set; }

        [XmlElement]
        public XElement resumeRIPE { get; set; }
    }
    public class Tracker
    {
        [XmlElement]
        public int resumeTrackerITEM { get; set; }

        [XmlElement]
        public string resumeTracker { get; set; }

    }
    public class GeoLite2City
    {

        [XmlAttribute()]
        public string IP { get; set; }

        [XmlElementAttribute(Order = 1)]
        public string CountryIsoCode { get; set; }

        [XmlElementAttribute(Order = 2)]
        public string CountryName { get; set; }

        [XmlElementAttribute(Order = 3)]
        public string MostSpecificSubdivisionName { get; set; }

        [XmlElement(Order = 4)]
        public string MostSpecificSubdivisionIsoCode { get; set; }

        [XmlElement(Order = 5)]
        public string CityName { get; set; }

        [XmlElement(Order = 6)]
        public string PostalCode { get; set; }

        [XmlElement(Order = 7)]
        public double LocationLatitude { get; set; }

        [XmlElement(Order = 8)]
        public double LocationLongitude { get; set; }
    }
    public class Node
    {
        public Node()
        {
            this.dhtGeoLite2City = new GeoLite2City();
        }
        [XmlElement]
        public int dhtNode { get; set; }

        [XmlElement]
        public string dhtNodeId { get; set; }

        [XmlElement]
        public string dhtNodeIP { get; set; }

        [XmlElement]
        public string dhtHostName { get; set; }

        [XmlElement]
        public uint dhtNodePort { get; set; }

        [XmlElement]
        public GeoLite2City dhtGeoLite2City { get; set; }

        [XmlElement]
        public XElement dhtRIPE { get; set; }
    }

    [XmlRoot(ElementName = "BitTorrent_.torrent_file_information")]
    public class BtTorrent
    {
        public BtTorrent()
        {
            this.announces = new List<Announce>();
        }

        [XmlAttribute()]
        public string torrentFileName { get; set; }

        [XmlElementAttribute(Order = 1)]
        public DateTime torrentFileCreated { get; set; }

        [XmlElementAttribute(Order = 2)]
        public DateTime torrentFileModified { get; set; }

        [XmlElementAttribute(Order = 3)]
        public DateTime torrentFileAccessed { get; set; }

        [XmlElement(Order = 4)]
        public string torrentAnnounceParameter { get; set; }

        [XmlElementAttribute(Order = 5)]
        public int torrentTotalAnnouncesNumber { get; set; }

        [XmlArray("torrentAnounce_List", Order = 6)]
        [XmlArrayItem("torrentAnnounce")]
        public List<Announce> announces;

        [XmlElement(Order = 7)]
        public string torrentCommentParameter { get; set; }

        [XmlElement(Order = 8)]
        public string torrentCreatedByParameter { get; set; }

        [XmlElementAttribute(Order = 9)]
        public string torrentCreationDateParameter { get; set; }

        [XmlElementAttribute(Order = 10)]
        public string torrentEncodingParameter { get; set; }

        [XmlElement(Order = 11)]
        public string torrentLengthParameter { get; set; }

        [XmlElement(Order = 12)]
        public string torrentNameParameter { get; set; }

        [XmlElement(Order = 13)]
        public string torrentPiecelengthParameter { get; set; }

        [XmlElement(Order = 14)]
        public string torrentPiecesParameter { get; set; }
    }
    public class Announce
    {
        [XmlElement]
        public int torrentAnnounceITEM { get; set; }

        [XmlElement]
        public string torrentAnnounce { get; set; }

    }
    public class HelperMethods
    {
        const string bitTorrentPath = "\\AppData\\Roaming\\BitTorrent";
        public string GetBitTorrentInstallationPath(string dev, string userName)
        {
            StringBuilder installPath = new StringBuilder(256);
            installPath.Append(dev);
            installPath.Append("\\Users\\");
            installPath.Append(userName);
            installPath.Append(bitTorrentPath);
            return installPath.ToString(); ;
        }
        public DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        public string ToAddr(long address)
        {
            return IPAddress.Parse(address.ToString()).ToString();
            // This also works:
            // return new IPAddress((uint) IPAddress.HostToNetworkOrder(
            //    (int) address)).ToString();
        }
        public string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();
            byte[] array = Encoding.Default.GetBytes(data);
            foreach (byte c in array)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }
        public string BinToHex(string bin)
        {
            StringBuilder binary = new StringBuilder(bin);
            bool isNegative = false;
            if (binary[0] == '-')
            {
                isNegative = true;
                binary.Remove(0, 1);
            }
            for (int i = 0, length = binary.Length; i < (4 - length % 4) % 4; i++) //padding leading zeros
            {
                binary.Insert(0, '0');
            }
            StringBuilder hexadecimal = new StringBuilder();
            StringBuilder word = new StringBuilder("0000");
            for (int i = 0; i < binary.Length; i += 4)
            {
                for (int j = i; j < i + 4; j++)
                {
                    word[j % 4] = binary[j];
                }

                switch (word.ToString())
                {
                    case "0000": hexadecimal.Append('0'); break;
                    case "0001": hexadecimal.Append('1'); break;
                    case "0010": hexadecimal.Append('2'); break;
                    case "0011": hexadecimal.Append('3'); break;
                    case "0100": hexadecimal.Append('4'); break;
                    case "0101": hexadecimal.Append('5'); break;
                    case "0110": hexadecimal.Append('6'); break;
                    case "0111": hexadecimal.Append('7'); break;
                    case "1000": hexadecimal.Append('8'); break;
                    case "1001": hexadecimal.Append('9'); break;
                    case "1010": hexadecimal.Append('A'); break;
                    case "1011": hexadecimal.Append('B'); break;
                    case "1100": hexadecimal.Append('C'); break;
                    case "1101": hexadecimal.Append('D'); break;
                    case "1110": hexadecimal.Append('E'); break;
                    case "1111": hexadecimal.Append('F'); break;
                    default:
                        return "Invalid number";
                }
            }
            if (isNegative)
            {
                hexadecimal.Insert(0, '-');
            }
            return hexadecimal.ToString();
        }
        public void btFillDht(string dhtString, BtDht btDhtEntity, bool demo)
        {
            int cyclesNumber;
            string dhtRIPEquery = "http://rest.db.ripe.net/search?source=ripe&query-string=";
            string temp;
            string temp1;
            uint portNumber = 0;
            int first = dhtString.IndexOf("id20") + "id20".Length + 1;
            temp = dhtString.Substring(first, 20);
            temp1 = StringToBinary(temp);
            btDhtEntity.dhtIdParameter = "0x" + BinToHex(temp1);
            first = dhtString.IndexOf("age") + "age".Length + 1;
            string age = dhtString.Substring(first, 10);
            double dateAsInt = 0;
            if (!double.TryParse(age, out dateAsInt))
                Console.WriteLine("Error converting age integer");
            DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
            btDhtEntity.dhtAgeParameter = dhtDate.ToString() + "(GMT +0:00)";
            int second = dhtString.IndexOf("ip4:") + "ip4:".Length;
            string ip = dhtString.Substring(second, 4);
            string ipBinary = StringToBinary(ip);
            string ipHex = BinToHex(ipBinary);
            uint ipLong = uint.Parse(ipHex, System.Globalization.NumberStyles.HexNumber);
            btDhtEntity.dhtIpParameter = ToAddr(ipLong);
            first = dhtString.IndexOf("table_depth") + "table_depth".Length + 1;
            temp1 = dhtString.Substring(first, 1);
            temp = "";
            while(temp1!="e")
            {
                temp = temp + temp1;
                first++;
                temp1 = dhtString.Substring(first, 1);
            }
            int tableDepth = 0;
            if (!int.TryParse(temp, out tableDepth))
                Console.WriteLine("Error converting tableDepth integer");
            btDhtEntity.dhtTableDepth = tableDepth;
            first = dhtString.IndexOf("nodes") + "nodes".Length;
            second = dhtString.IndexOf(":", first, 6);
            int lenght = second - first;
            string nodesByteCount = dhtString.Substring(first, lenght);
            int intNodesByteCount;
            int.TryParse(nodesByteCount, out intNodesByteCount);
            int nodesNumber = intNodesByteCount / 26;
            btDhtEntity.dhtTotalNodesNumber = nodesNumber;
            if (nodesNumber != 0)
            {
                int indexOfNode = second + 1;
                ///TODO 01 In DEMO mode only few first nodes IP are checked in RIPE and GeoLite2-City.mmdb
                if (demo)
                {
                    Console.WriteLine("\nDEMO mode: will be analyzed only first few nodes in file dht.dat," +
                        "\ntotal number of nodes is {0}", nodesNumber);
                    if (nodesNumber > 4) cyclesNumber = 4;
                    else cyclesNumber = nodesNumber;
                }
                else
                {
                    Console.WriteLine("\nWill be analyzed {0} nodes which we found in file dht.dat", nodesNumber);
                    cyclesNumber = nodesNumber;
                }
                for (int i = 0; i < cyclesNumber; i++)
                {
                    Node btDhtNode = new Node();
                    btDhtNode.dhtNode = i;
                    temp = dhtString.Substring(indexOfNode, 20);
                    temp1 = StringToBinary(temp);
                    btDhtNode.dhtNodeId = "0x" + BinToHex(temp1);
                    ip = dhtString.Substring(indexOfNode + 20, 4);
                    ipBinary = StringToBinary(ip);
                    ipHex = BinToHex(ipBinary);
                    ipLong = uint.Parse(ipHex, System.Globalization.NumberStyles.HexNumber);
                    string ipAddress = ToAddr(ipLong);
                    btDhtNode.dhtNodeIP = ipAddress;
                    Console.WriteLine("\nAnalyzing node {0} IP address {1}", i, ipAddress);
                    IPAddress addr = IPAddress.Parse(ipAddress);
                    try
                    {
                        IPHostEntry entry = Dns.GetHostEntry(addr);
                        btDhtNode.dhtHostName = entry.HostName;
                    }
                    catch
                    {
                        btDhtNode.dhtHostName = "";
                    }
                    finally
                    {
                        using (var reader = new DatabaseReader(@"GeoLite2-City.mmdb"))
                        {
                            // Replace "City" with the appropriate method for your database, e.g.,
                            // "Country"
                            btDhtNode.dhtGeoLite2City.IP = ipAddress;
                            try
                            {
                                var city = reader.City(ipAddress);

                                btDhtNode.dhtGeoLite2City.CountryIsoCode = city.Country.IsoCode;
                                btDhtNode.dhtGeoLite2City.CountryName = city.Country.Name;

                                btDhtNode.dhtGeoLite2City.MostSpecificSubdivisionName = city.MostSpecificSubdivision.Name;
                                btDhtNode.dhtGeoLite2City.MostSpecificSubdivisionIsoCode = city.MostSpecificSubdivision.IsoCode;

                                btDhtNode.dhtGeoLite2City.CityName = city.City.Name;

                                btDhtNode.dhtGeoLite2City.PostalCode = city.Postal.Code;

                                btDhtNode.dhtGeoLite2City.LocationLatitude = (double)city.Location.Latitude;

                                btDhtNode.dhtGeoLite2City.LocationLongitude = (double)city.Location.Longitude;
                            }
                            catch (Exception ex)
                            {
                                btDhtNode.dhtGeoLite2City.CountryIsoCode = "Not Found";
                                btDhtNode.dhtGeoLite2City.CountryName = "Not Found";

                                btDhtNode.dhtGeoLite2City.MostSpecificSubdivisionName = "Not Found";
                                btDhtNode.dhtGeoLite2City.MostSpecificSubdivisionIsoCode = "Not Found";

                                btDhtNode.dhtGeoLite2City.CityName = "Not Found";

                                btDhtNode.dhtGeoLite2City.PostalCode = "Not Found";

                                btDhtNode.dhtGeoLite2City.LocationLatitude = (double)0.0;

                                btDhtNode.dhtGeoLite2City.LocationLongitude = (double)0.0;
                            }
                        }
                        btDhtNode.dhtRIPE = XElement.Load(dhtRIPEquery + ipAddress);
                    }
                    temp = dhtString.Substring(indexOfNode + 24, 2);
                    temp1 = StringToBinary(temp);
                    temp = BinToHex(temp1);
                    uint.TryParse(temp, System.Globalization.NumberStyles.HexNumber, null, out portNumber);
                    btDhtNode.dhtNodePort = portNumber;

                    btDhtEntity.nodes.Add(btDhtNode);

                    indexOfNode = indexOfNode + 26;
                }
            }
        }

        public void btFillResume(string resumeString, BtResumeFile btResumeFile, bool demo)
        {
            int cyclesNumber;
            string resumeRIPEquery = "http://rest.db.ripe.net/search?source=ripe&query-string=";
            int first;
            int indexFirst;
            int second;
            int index;
            int length;
            int indexOfPeer = 0;
            uint portNumber = 0;
            string resumeTorrentFileDic = "";
            string temp = "";
            string temp1 = "";
            string ip = "";
            string ipBinary = "";
            string ipHex = "";
            uint ipLong = 0;
            bool isTorrent = false;
            //GeoLite2City geoCity = new GeoLite2City();
            first = resumeString.IndexOf(".torrentd");
            if (first != -1) isTorrent = true;
            first = first + ".torrentd".Length;
            while (isTorrent)
            {
                BtResume btResumeEntity = new BtResume();
                indexFirst = resumeString.LastIndexOf(":", first);
                second = resumeString.IndexOf(".torrentd", first);
                if (second == -1) second = resumeString.Length - 20;
                index = resumeString.LastIndexOf("ee", second);
                length = index - first + 2;
                resumeTorrentFileDic = resumeString.Substring(first - 1, length + 1);
                byte[] array = Encoding.Default.GetBytes(resumeTorrentFileDic);
                //Console.WriteLine(Encoding.Default.GetString(array));
                var blist = Bencode.BencodeUtility.DecodeDictionary(array);
                byte[] aa = new byte[256];
                foreach (var bl in blist)
                {
                    if (bl.Key == "added_on")
                    {
                        var creationDate = bl.Value;
                        temp = creationDate.ToString();
                        double dateAsInt = 0;
                        if (!double.TryParse(temp, out dateAsInt))
                            Console.WriteLine("Error converting creation date integer");
                        DateTime resumeDate = ConvertFromUnixTimestamp(dateAsInt);
                        //Console.WriteLine("added_on {0}", dhtDate.ToString());
                        btResumeEntity.resumeAdded_onParameter = resumeDate.ToString() + "(GMT +0:00)";
                    }
                    if (bl.Key == "blocksize")
                    {
                        var bb = bl.Value;
                        //Console.WriteLine("blocksize {0}", bb);
                        btResumeEntity.resumeBlocksizeParameter = bb.ToString();
                    }
                    if (bl.Key == "caption")
                    {
                        aa = (byte[])bl.Value;
                        Console.WriteLine("\nAnalyzing downloaded file {0} artifats found in resume.dat", Encoding.Default.GetString(aa));
                        btResumeEntity.resumeCaptionParameter = Encoding.UTF8.GetString(aa, 0, aa.Length);
                    }
                    if (bl.Key == "path")
                    {
                        aa = (byte[])bl.Value;
                        //Console.WriteLine("caption {0}", Encoding.Default.GetString(aa));
                        btResumeEntity.resumePathParameter = Encoding.UTF8.GetString(aa, 0, aa.Length);
                    }
                    if (bl.Key == "completed_on")
                    {
                        var creationDate = bl.Value;
                        temp = creationDate.ToString();
                        double dateAsInt = 0;
                        if (!double.TryParse(temp, out dateAsInt))
                            Console.WriteLine("Error converting creation date integer");
                        if (dateAsInt == 0.0) btResumeEntity.resumeCompleted_onParameter = "Partially downloaded";
                        else
                        {
                            DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
                            //Console.WriteLine("completed_on {0}", dhtDate.ToString());
                            btResumeEntity.resumeCompleted_onParameter = dhtDate.ToString() + "(GMT +0:00)";
                        }
                    }
                    if (bl.Key == "dht")
                    {
                        var bb = bl.Value;
                        //Console.WriteLine("dht {0}", bb);
                        btResumeEntity.resumeDhtParameter = bb.ToString();
                    }
                    if (bl.Key == "downloaded")
                    {
                        temp = bl.Value.ToString();
                        btResumeEntity.resumeDownloadedParameter = temp + " bytes";
                    }
                    if (bl.Key == "last seen complete")
                    {
                        var lastSeenCompleteDate = bl.Value;
                        temp = lastSeenCompleteDate.ToString();
                        double dateAsInt = 0;
                        if (!double.TryParse(temp, out dateAsInt))
                            Console.WriteLine("Error converting creation date integer");
                        if (dateAsInt == 0.0) btResumeEntity.resumeCompleted_onParameter = "Not known";
                        else
                        {
                            DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
                            //Console.WriteLine("completed_on {0}", dhtDate.ToString());
                            btResumeEntity.resumeLastSeenCompleteParameter = dhtDate.ToString() + "(GMT +0:00)";
                        }
                    }
                    if (bl.Key == "peers6")
                    {
                        indexOfPeer = 0;
                        aa = (byte[])bl.Value;
                        int intPeersNumber = aa.Length / 18;
                        btResumeEntity.resumePeersNumber = intPeersNumber;
                        if (intPeersNumber != 0)
                        {
                            second = 12;
                            string peers6String = Encoding.Default.GetString(aa, 0, aa.Length);
                            ///TODO 02 In DEMO mode only few first peers IP are checked in RIPE and GeoLite2-City.mmdb
                            if (demo)
                            {
                                Console.WriteLine("\nDEMO mode: will be analyzed only few first peers in file resume.dat," +
                                    "\ntotal number of peers is {0}", intPeersNumber);
                                if (intPeersNumber > 4) cyclesNumber = 4;
                                else cyclesNumber = intPeersNumber;
                            }
                            else
                            {
                                Console.WriteLine("\nWill be analyzed {0} peers which we found in file resume.dat", intPeersNumber);
                                cyclesNumber = intPeersNumber;
                            }
                            for (int i = 0; i < cyclesNumber; i++)
                            {
                                Peer btResumePeer = new Peer();
                                btResumePeer.resumePeer = i;
                                ip = peers6String.Substring(indexOfPeer + 12, 4);
                                ipBinary = StringToBinary(ip);
                                ipHex = BinToHex(ipBinary);
                                ipLong = uint.Parse(ipHex, System.Globalization.NumberStyles.HexNumber);
                                string ipAddress = ToAddr(ipLong);
                                btResumePeer.resumePeerIP = ipAddress;
                                Console.WriteLine("\nAnalyzing peer {0} IP address {1}", i, ipAddress);
                                if (ipAddress != "127.0.0.1")
                                {
                                    IPAddress addr = IPAddress.Parse(ipAddress);
                                    try
                                    {
                                        IPHostEntry entry = Dns.GetHostEntry(addr);
                                        btResumePeer.resumePeerHostName = entry.HostName;
                                    }
                                    catch
                                    {
                                        btResumePeer.resumePeerHostName = "Not found";
                                    }
                                    finally
                                    {
                                        using (var reader = new DatabaseReader(@"GeoLite2-City.mmdb"))
                                        {
                                            // Replace "City" with the appropriate method for your database, e.g.,
                                            // "Country"
                                            btResumePeer.resumeGeoLite2City.IP = ipAddress;
                                            try
                                            {
                                                var city = reader.City(ipAddress);

                                                btResumePeer.resumeGeoLite2City.CountryIsoCode = city.Country.IsoCode;
                                                btResumePeer.resumeGeoLite2City.CountryName = city.Country.Name;

                                                btResumePeer.resumeGeoLite2City.MostSpecificSubdivisionName = city.MostSpecificSubdivision.Name;
                                                btResumePeer.resumeGeoLite2City.MostSpecificSubdivisionIsoCode = city.MostSpecificSubdivision.IsoCode;

                                                btResumePeer.resumeGeoLite2City.CityName = city.City.Name;

                                                btResumePeer.resumeGeoLite2City.PostalCode = city.Postal.Code;

                                                btResumePeer.resumeGeoLite2City.LocationLatitude = (double)city.Location.Latitude;

                                                btResumePeer.resumeGeoLite2City.LocationLongitude = (double)city.Location.Longitude;
                                            }
                                            catch (Exception ex)
                                            {
                                                btResumePeer.resumeGeoLite2City.CountryIsoCode = "Not Found";
                                                btResumePeer.resumeGeoLite2City.CountryName = "Not Found";

                                                btResumePeer.resumeGeoLite2City.MostSpecificSubdivisionName = "Not Found";
                                                btResumePeer.resumeGeoLite2City.MostSpecificSubdivisionIsoCode = "Not Found";

                                                btResumePeer.resumeGeoLite2City.CityName = "Not Found";

                                                btResumePeer.resumeGeoLite2City.PostalCode = "Not Found";

                                                btResumePeer.resumeGeoLite2City.LocationLatitude = (double)0.0;

                                                btResumePeer.resumeGeoLite2City.LocationLongitude = (double)0.0;
                                            }
                                        }
                                        btResumePeer.resumeRIPE = XElement.Load(resumeRIPEquery + ipAddress);
                                    }
                                }
                                else
                                {
                                    btResumePeer.resumePeerHostName = "localhost";
                                }
                                temp = peers6String.Substring(indexOfPeer + 16, 2);
                                temp1 = StringToBinary(temp);
                                temp = BinToHex(temp1);
                                uint.TryParse(temp, System.Globalization.NumberStyles.HexNumber, null, out portNumber);
                                btResumePeer.resumePeerPort = portNumber;
                                btResumeEntity.peers.Add(btResumePeer);
                                indexOfPeer = indexOfPeer + 18;
                            }
                        }
                    }
                    if (bl.Key == "trackers")
                    {
                        List<object> bb = (List<object>)bl.Value;
                        length = bb.Count;
                        btResumeEntity.resumeTotalTrackersNumber = length;
                        int ik = 0;
                        foreach (var value in bb)
                        {
                            Tracker btResumeTracker = new Tracker();
                            btResumeTracker.resumeTrackerITEM = ik;
                            aa = (byte[])value;
                            temp = Encoding.Default.GetString(aa, 0, aa.Length);
                            btResumeTracker.resumeTracker = temp;
                            ik = ik + 1;
                            btResumeEntity.trackers.Add(btResumeTracker);
                        }
                    }
                }
                btResumeFile.files.Add(btResumeEntity);
                first = resumeString.IndexOf(".torrentd", first);
                if (first == -1)
                {
                    isTorrent = false;
                }
                else
                {
                    first = first + ".torrentd".Length;
                }
            }
        }

        public void btFillTorrent(string torrentString, BtTorrent btTorrentEntity)
        {
            string temp;
            int second;
            int first;
            int lenght;
            byte[] array = Encoding.Default.GetBytes(torrentString);
            var blist = Bencode.BencodeUtility.DecodeDictionary(array);
            byte[] aa = new byte[256];
            foreach (var bl in blist)
            {
                if (bl.Key == "announce")
                {
                    aa = (byte[])bl.Value;
                    btTorrentEntity.torrentAnnounceParameter = Encoding.UTF8.GetString(aa, 0, aa.Length);
                }
                if (bl.Key == "announce-list")
                {
                    List<object> bb = (List<object>)bl.Value;
                    lenght = bb.Count;
                    btTorrentEntity.torrentTotalAnnouncesNumber = lenght;
                    int ik = 0;
                    foreach (var value in bb)
                    {
                        Announce btTorrentAnnounce = new Announce();
                        btTorrentAnnounce.torrentAnnounceITEM = ik;
                        BinaryFormatter bf = new BinaryFormatter();
                        using (var ms = new MemoryStream())
                        {
                            bf.Serialize(ms, value);
                            aa = ms.ToArray();
                            temp = Encoding.UTF8.GetString(aa, 220, aa.Length - 221);
                            btTorrentAnnounce.torrentAnnounce = temp;
                        }
                        ik = ik + 1;
                        btTorrentEntity.announces.Add(btTorrentAnnounce);
                    }
                }
                if (bl.Key == "comment")
                {
                    aa = (byte[])bl.Value;
                    btTorrentEntity.torrentCommentParameter = Encoding.UTF8.GetString(aa, 0, aa.Length);
                }
                if (bl.Key == "created by")
                {
                    aa = (byte[])bl.Value;
                    btTorrentEntity.torrentCreatedByParameter = Encoding.UTF8.GetString(aa, 0, aa.Length);
                }
                if (bl.Key == "creation date")
                {
                    var creationDate = bl.Value;
                    temp = creationDate.ToString();
                    double dateAsInt = 0;
                    if (!double.TryParse(temp, out dateAsInt))
                        Console.WriteLine("Error converting creation date integer");
                    DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
                    btTorrentEntity.torrentCreationDateParameter = dhtDate.ToString() + "(GMT +0:00)";
                }
                if (bl.Key == "encoding")
                {
                    aa = (byte[])bl.Value;
                    btTorrentEntity.torrentEncodingParameter = Encoding.UTF8.GetString(aa, 0, aa.Length);
                }
                if (bl.Key == "info")
                {
                    first = torrentString.IndexOf("length") + "length".Length + 1;
                    second = torrentString.IndexOf("e", first, 20);
                    lenght = second - first;
                    temp = torrentString.Substring(first, lenght);
                    btTorrentEntity.torrentLengthParameter = temp;

                    first = torrentString.IndexOf("name") + "name".Length;
                    second = torrentString.IndexOf(":", first, 20);
                    lenght = second - first;
                    temp = torrentString.Substring(first, lenght);
                    int.TryParse(temp, out lenght);
                    temp = torrentString.Substring(second + 1, lenght);
                    btTorrentEntity.torrentNameParameter = temp;

                    first = torrentString.IndexOf("piece length") + "piece length".Length + 1;
                    second = torrentString.IndexOf("e", first, 20);
                    lenght = second - first;
                    temp = torrentString.Substring(first, lenght);
                    btTorrentEntity.torrentPiecelengthParameter = temp;

                    first = torrentString.IndexOf("pieces") + "pieces".Length;
                    second = torrentString.IndexOf(":", first, 20);
                    lenght = second - first;
                    temp = torrentString.Substring(first, lenght);
                    btTorrentEntity.torrentPiecesParameter = temp;
                }
            }
        }

        public void btFillSettings(string settingsString, BtSettings btSettingsEntity)
        {
            string temp;
            byte[] array = Encoding.Default.GetBytes(settingsString);
            var blist = Bencode.BencodeUtility.DecodeDictionary(array);
            byte[] aa = new byte[256];
            foreach (var bl in blist)
            {
                if (bl.Key == "bind_port")
                {
                    btSettingsEntity.settingsBindPortParameter = (Int64)bl.Value;
                }
                if (bl.Key == "gui.last_bundle_visit")
                {
                    var creationDate = bl.Value;
                    temp = creationDate.ToString();
                    double dateAsInt = 0;
                    if (!double.TryParse(temp, out dateAsInt))
                        Console.WriteLine("Error converting creation date integer");
                    DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
                    btSettingsEntity.settingsGuiLastBundleVisit = dhtDate.ToString();
                }
                if (bl.Key == "isp.peer_policy_expy")
                {
                    var creationDate = bl.Value;
                    temp = creationDate.ToString();
                    double dateAsInt = 0;
                    if (!double.TryParse(temp, out dateAsInt))
                        Console.WriteLine("Error converting creation date integer");
                    DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
                    btSettingsEntity.settingsIspPeerPolicyExpy = dhtDate.ToString();
                }
                if (bl.Key == "labelDirectoryMap")
                {
                    Dictionary<string, object> bd = (Dictionary<string, object>)bl.Value;
                    foreach (var value in bd)
                    {
                        DirectoryMap btSettingsDirectoryMap = new DirectoryMap();

                        aa = (byte[])value.Value;
                        temp = Encoding.Default.GetString(aa, 0, aa.Length);
                        btSettingsDirectoryMap.settingsDirectoryMap = temp;
                        btSettingsEntity.directoryMaps.Add(btSettingsDirectoryMap);

                    }
                }
                if (bl.Key == "labelRuleMap")
                {
                    Dictionary<string, object> bd = (Dictionary<string, object>)bl.Value;
                    foreach (var value in bd)
                    {
                        RuleMap btSettingsRuleMap = new RuleMap();

                        aa = (byte[])value.Value;
                        temp = Encoding.Default.GetString(aa, 0, aa.Length);
                        btSettingsRuleMap.settingsRuleMap = temp;
                        btSettingsEntity.ruleMaps.Add(btSettingsRuleMap);

                    }
                }
                if (bl.Key == "settings_saved_systime")
                {
                    var creationDate = bl.Value;
                    temp = creationDate.ToString();
                    double dateAsInt = 0;
                    if (!double.TryParse(temp, out dateAsInt))
                        Console.WriteLine("Error converting creation date integer");
                    DateTime dhtDate = ConvertFromUnixTimestamp(dateAsInt);
                    btSettingsEntity.settingsSavedSystime = dhtDate.ToString();
                }
            }
        }
    }
}
