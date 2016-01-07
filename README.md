# BitTorrent-Artifact-Analyzer
The tool examines the selected Windows directories: \Users\*\AppData\Roaming\Bittorrent for the files: *.torrent, dht.dat, re-sume.dat, and settings.dat. The files are coded in the BEncode format. The tool parses the files, extracts the important for the forensics investigator information and converts it into the XML format. The results are accumulated into a single result file. The results file can be viewed and analyzed either in the special XML editor or in any browser.

The tool is implemented as a command line utility having four parameters: the letter of the disc drive to search for, the full path where to place the report file, the report file name with extension xml and debug mode value. Debug mode value is used for the tool demonstration purpose and to reduce the time to analyze large number of IP addresses. When working in the debug mode (the value of the parameter is not equal to zero) the tool analyzes only a few nodes and a few peer IP addresses in the files dht.dat and resume.dat, accordingly.

You can find:

•	Release folder with executable in Release.zip,

•	Examples of BitTorrent files to analyze in Files folder in Files.zip,

•	Example of the XML report in BtaaReport.zip

•	and Visual Studio 2013 solution in VS2013 Solution/BitTorrent-Artifact-Analyzer directory.


To use the tool perform the following actions: 

1.	Create the directory AnyDeviceLetter:\Users\AnyUserName\AppData\Roaming\BitTorrent.
2.	Copy all BitTorrent files from Files.zip into created directory.
3.	Extract Realease.zip.
4.	Run Command Prompt in Administrator mode.
5.	Change working directory of Command Prompt to the extracted Release directory 
6.	Run the tool on command line:
            BitTorrent-Artifact-Analyzer [Device to analyze] [Path to save XML report] [XMLreport file name] [Debug Mode]

Example: 
       BitTorrent-Artifact-Analyzer W: C:\Temp Report.xml 1
       
Copyright (©) 2014-2016 Kaunas University of Technology. All rights reserved. The program has been developed within the project “Lithuanian Cyber-crime Centre of Excellence for Training, Research and Education”, Grant Agreement No HOME/2013/ISEC/AG/INT/4000005176, co-funded by the Prevention of and Fight against Crime Programme of the European Union.
