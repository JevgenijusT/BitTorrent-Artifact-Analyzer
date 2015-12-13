# BitTorrent-Artifact-Analyzer
The tool looks in selected Windows device directories: \Users\\*\AppData\Roaming\Bittorrent 
for the files: *.torrent, dht.dat, re-sume.dat, and settings.dat. The files are coded in the BEncode format. The tool parses the files, extracts the important for the forensics investigator information and converts it into XML format. The results are accumulated into a single result file. The results file can be viewed and analyzed either in the special XML editor or in any browser.

The tool is implemented as a command line utility having four parameters: the letter of the disc to search for, the full path where to place the report file, the report file name with extension xml and demonstration mode value. Demonstration mode value is used for tool demonstration purpose and reduce the time to analyze large number of IP addresses. When working in demonstration mode (value of the parameter not zero) the tool analyze only a few nodes and peers IP addresses in files dht.dat and resume.dat accordingly.

You can find Release folder with executable in Release.zip, examples of BitTorrent files to analyze in Files folder in Files.zip, and example of the XML report in BtaaReport.zip. 

Do the following to view the tool in practice: 

1. Create fake directory AnyDeviceLetter:\Users\AnyUserName\AppData\Roaming\BitTorrent 
2. Copy all BitTorrent files from Files.zip into created fake directory 
3. Extract Realease.zip 
4. Run as Administrator Command Prompt
5. Change Command Promt working directory to the extracted Release directory 
6. Run the tool from command line:
7. Usage:
 
BitTorrent-Artifact-Analyzer [Device to analyze] [Path to save XML report] [XMLreport file name] [Demonstration Mode]

8. Usage example: 

BitTorrent-Artifact-Analyzer W: C:\Temp Report.xml 1

Copyright (©) 2014-2016 Kaunas University of Technology. All rights reserved.
The program has been developed out partially within the project “Lithuanian Cyber-crime Centre of Excellence for Training, Research and Education”, Grant Agreement No HOME/2013/ISEC/AG/INT/4000005176, co-funded by the Prevention of and Fight against Crime Programme of the European Union.
