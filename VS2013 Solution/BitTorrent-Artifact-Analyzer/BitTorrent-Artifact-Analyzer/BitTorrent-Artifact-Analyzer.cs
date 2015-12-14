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
    public class BitTorrent_Artifact_Analyzer
    {
        string deviceForAnalyzer;
        string fullPath;
        string reportPathAndName;
        public BitTorrent_Artifact_Analyzer(string deviceToAnalize, string pathToSaveReport, string analyzerReportFileName)
        {
            this.deviceForAnalyzer = deviceToAnalize;
            this.fullPath = pathToSaveReport;
            this.reportPathAndName = Path.Combine(fullPath, analyzerReportFileName);
        }
        public void WriteProgreesToConsole(DateTime dtNow, string device, string userName, string progressString)
        {
            Console.WriteLine("\n{0} Analyzing drive \"{1}\" user \"{2}\" {3}", dtNow, device, userName, progressString);
        }
        public void WriteProgreesToConsole(DateTime dtNow, string device, string progressString)
        {
            Console.WriteLine("\n{0} Analyzing drive \"{1}\" {2}", dtNow, device, progressString);
        }
        public void WriteProgreesToConsole(DateTime dtNow, string pathToTorrentFile)
        {
            Console.WriteLine("\n{0} Analyzing torrent file \"{1}\"", dtNow, pathToTorrentFile);
        }
        public void WriteFinishedToConsole(DateTime dtNow)
        {
            Console.WriteLine("\nFinished.\n\n {0} BitTorrent-Artifact-Analyzer. Press any key to exit", dtNow);
            Console.ReadKey();
        }
        public void CreateReport(bool demoMode)
        {
            DateTime progressTime = DateTime.Now;
            WriteProgreesToConsole(progressTime, deviceForAnalyzer, "Analizing Users directory to detect BitTorrent activity artifacts");
            DateTime startDateTime = DateTime.Now;
            Comments allComments = new Comments();
            String format = "yyyy-MM-dd HH:mm:sszzz";
            allComments.analyzerStartDateAndTime = "L3CE BitTorrent Analyzer Start: " + startDateTime.ToString(format);
            HelperMethods hlpMethods = new HelperMethods();
            DateTime endDateTime;

            string[] usersDirectoriesList = Directory.GetDirectories(deviceForAnalyzer + "\\Users");
            List<string> listOfUsersDirectories = new List<string>();
            string tmpDir;
            foreach (var dir in usersDirectoriesList)
            {
                tmpDir = dir.Replace(Path.GetDirectoryName(dir) + Path.DirectorySeparatorChar, "");
                if (tmpDir == "All Users") continue;
                if (tmpDir == "Default") continue;
                if (tmpDir == "Default User") continue;
                if (tmpDir == "Public") continue;
                listOfUsersDirectories.Add(tmpDir);
            }
            using (XmlTextWriter writer = new XmlTextWriter(reportPathAndName, System.Text.Encoding.UTF8))
            {
                XmlSerializerNamespaces xmlnsEmpty = new XmlSerializerNamespaces();
                xmlnsEmpty.Add("", "");

                writer.Formatting = Formatting.None;
                writer.WriteStartDocument(true);
                writer.WriteStartElement("L3CEBitTorrentAnalyzerStart");
                writer.WriteAttributeString("Started", allComments.analyzerStartDateAndTime);
                if (demoMode) writer.WriteAttributeString("DemonstrationModeOn", "The tool will analyze a few nodes and peers IP addresses in files dht.dat and resume.dat accordingly");
                writer.WriteStartElement("Users");
                Console.WriteLine("\nWill analyze {0} users directories:", listOfUsersDirectories.Count);
                foreach (var usrName in listOfUsersDirectories)
                {
                    Console.WriteLine("  -user directory: {0}", usrName);
                }
                foreach (var usrName in listOfUsersDirectories)
                {
                    writer.WriteStartElement("User");
                    writer.WriteAttributeString("UserName", usrName);
                    fullPath = hlpMethods.GetBitTorrentInstallationPath(deviceForAnalyzer, usrName);
                    allComments.analyzerStartDateAndTime = "L3CE BitTorrent Analyzer Started: " + startDateTime.ToString(format);
                    string dhtString = "";
                    progressTime = DateTime.Now;
                    WriteProgreesToConsole(progressTime, deviceForAnalyzer, usrName, " dht.dat file");
                    BtDht btDhtEntity = new BtDht();
                    reportPathAndName = fullPath + "\\dht.dat";
                    if (!File.Exists(reportPathAndName)) Console.WriteLine("File not found: {0}", reportPathAndName);
                    else
                    {
                        btDhtEntity.dhtFileName = reportPathAndName;
                        btDhtEntity.dhtFileCreated = File.GetCreationTime(reportPathAndName);
                        btDhtEntity.dhtFileModified = File.GetLastWriteTime(reportPathAndName);
                        btDhtEntity.dhtFileAccessed = File.GetLastAccessTime(reportPathAndName);
                        StreamReader reader = new StreamReader(reportPathAndName, System.Text.Encoding.Default);
                        dhtString = reader.ReadToEnd();
                        reader.Close();
                        hlpMethods.btFillDht(dhtString, btDhtEntity, demoMode);
                    }

                    progressTime = DateTime.Now;
                    WriteProgreesToConsole(progressTime, deviceForAnalyzer, usrName, " resume.dat file");
                    string resumeString = "";
                    BtResumeFile btResumeFile = new BtResumeFile();
                    reportPathAndName = fullPath + "\\resume.dat";
                    if (!File.Exists(reportPathAndName)) Console.WriteLine("File not found: {0}", reportPathAndName);
                    else
                    {
                        btResumeFile.resumeFileName = reportPathAndName;
                        btResumeFile.resumeFileCreated = File.GetCreationTime(reportPathAndName);
                        btResumeFile.resumeFileModified = File.GetLastWriteTime(reportPathAndName);
                        btResumeFile.resumeFileAccessed = File.GetLastAccessTime(reportPathAndName);
                        StreamReader reader = new StreamReader(reportPathAndName, System.Text.Encoding.Default);
                        resumeString = reader.ReadToEnd();
                        reader.Close();
                        hlpMethods.btFillResume(resumeString, btResumeFile, demoMode);
                    }

                    progressTime = DateTime.Now;
                    WriteProgreesToConsole(progressTime, deviceForAnalyzer, usrName, " settings.dat file");
                    string settingsString = "";
                    BtSettings btSettingsFile = new BtSettings();
                    reportPathAndName = fullPath + "\\settings.dat";
                    if (!File.Exists(reportPathAndName)) Console.WriteLine("File not found: {0}", reportPathAndName);
                    else
                    {
                        btSettingsFile.settingsFileName = reportPathAndName;
                        btSettingsFile.settingsFileCreated = File.GetCreationTime(reportPathAndName);
                        btSettingsFile.settingsFileModified = File.GetLastWriteTime(reportPathAndName);
                        btSettingsFile.settingsFileAccessed = File.GetLastAccessTime(reportPathAndName);
                        StreamReader reader = new StreamReader(reportPathAndName, System.Text.Encoding.Default);
                        settingsString = reader.ReadToEnd();
                        reader.Close();
                        hlpMethods.btFillSettings(settingsString, btSettingsFile);
                    }
                    try
                    {
                        string[] torrentFiles = Directory.GetFiles(fullPath, "*.torrent");
                        string torrentString = "";
                        XmlSerializer serializer = new XmlSerializer(typeof(BtDht));
                        XmlSerializer serializer1 = new XmlSerializer(typeof(BtTorrent));
                        XmlSerializer serializer2 = new XmlSerializer(typeof(BtResumeFile));
                        XmlSerializer serializer3 = new XmlSerializer(typeof(BtSettings));
                        writer.WriteComment(allComments.aboutBtDHT);
                        serializer.Serialize(writer, btDhtEntity, xmlnsEmpty);
                        writer.WriteComment(allComments.aboutBtResume);
                        serializer2.Serialize(writer, btResumeFile, xmlnsEmpty);
                        writer.WriteComment(allComments.aboutBtSettings);
                        serializer3.Serialize(writer, btSettingsFile, xmlnsEmpty);
                        writer.WriteComment(allComments.aboutBtTorrent);
                        foreach (string torrentFileName in torrentFiles)
                        {
                            progressTime = DateTime.Now;
                            WriteProgreesToConsole(progressTime, torrentFileName);
                            if (!File.Exists(torrentFileName)) Console.WriteLine("File not found: {0}", reportPathAndName);
                            else
                            {
                                BtTorrent btTorrentEntity = new BtTorrent();
                                btTorrentEntity.torrentFileName = torrentFileName;
                                torrentString = "";
                                btTorrentEntity.torrentFileCreated = File.GetCreationTime(torrentFileName);
                                btTorrentEntity.torrentFileModified = File.GetLastWriteTime(torrentFileName);
                                btTorrentEntity.torrentFileAccessed = File.GetLastAccessTime(torrentFileName);
                                StreamReader reader = new StreamReader(torrentFileName, System.Text.Encoding.Default);
                                torrentString = reader.ReadToEnd();
                                reader.Close();
                                hlpMethods.btFillTorrent(torrentString, btTorrentEntity);
                                serializer1.Serialize(writer, btTorrentEntity, xmlnsEmpty);
                            }
                        }
                    }
                    catch (DirectoryNotFoundException)
                    {
                        Console.WriteLine("\nBitTorrent directory not found in analyzed user {0} directory", usrName);
                        
                    }

                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                endDateTime = DateTime.Now;
                writer.WriteStartElement("L3CEBitTorrentAnalyzerFinish");
                allComments.analyzerEndDateAndTime = "L3CE BitTorrent Analyzer Finished: " + endDateTime.ToString(format);
                writer.WriteAttributeString("Finish", allComments.analyzerEndDateAndTime);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                progressTime = DateTime.Now;
                WriteFinishedToConsole(progressTime);
            }
        }
    }
}
