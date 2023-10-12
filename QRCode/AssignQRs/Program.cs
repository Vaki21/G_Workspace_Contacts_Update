using System.Linq;
using System.Text.RegularExpressions;

bool running = true;
while (running == true)
{
    Console.WriteLine("Tool for updating CSV database...");
    Console.WriteLine("Select action You want to do:");
    Console.WriteLine("To update QR Code URLs type 1");
    Console.WriteLine("To update Google Database type 2");
    Console.WriteLine("To EXIT type y");

    var option = Console.ReadLine();
    if (option != null)
    {
        option = option.ToString();
        if (option == "1")
        {
            addQRurls("C:\\Users\\USERNAME\\Downloads\\export\\users.csv");
            running = false;
        }
        else if (option == "2")
        {
            updateFile("C:\\Users\\USERNAME\\Downloads\\test\\users.csv", "C:\\Users\\USERNAME\\Downloads\\test\\info.csv");
            running = false;
        }
        else if (option == "y")
        {
            Environment.Exit(exitCode: 0);
        }
    }
    else
    {
        Console.WriteLine("Wrong value, please try again...");
        running = false;
    }
}


//Assigns QR Code URL to each user
static void addQRurls(string filepath)
{
    string tempFile = "temp.txt";
    var separatorChar = ",";
    Regex regx = new Regex(separatorChar + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

    try
    {
        string[] lines = System.IO.File.ReadAllLines(filepath);

        //Gets this month and year
        string thisMonth = DateTime.Now.ToString("MM");
        string thisYear = DateTime.Now.ToString("yyyy");
        //Find the record to be edited. Finds a user and assigns his QRurl

        for (int i = 0; i < lines.Length; i++)
        {
            string[] fields = regx.Split(lines[i]);
            //string[] fields = lines[i].Split(',');

            if (fields[10] != "customField1")
            {
                addRecord(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7], fields[8], fields[9], "http://URL" + thisYear + "/" + "10" + "/" + fields[0] + "_cropped_" + "10" + "_2.bmp", fields[11], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], fields[18], fields[19], @tempFile);
            }
            else
            {
                addRecord(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7], fields[8], fields[9], fields[10], fields[11], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], fields[18], fields[19], @tempFile);
            }
        }


        //Delete old file
        File.Delete(@filepath);

        //Rename new file
        System.IO.File.Move(tempFile, filepath);

        Console.WriteLine("QR Code URLs added...");

    }
    catch (Exception ex)
    {
        Console.WriteLine("Program did an error.");
        throw new ApplicationException("Error: ", ex);
    }
}

//Compares data from two files and updates them in the first one. Updates Google DB
static void updateFile(string oFilepath, string nFilepath)
{
    string tempFile = "temp.txt";

    try
    {
        //Google file path
        string[] oLines = System.IO.File.ReadAllLines(oFilepath);
        //GIS file path
        string[] nLines = System.IO.File.ReadAllLines(nFilepath);

        var separatorChar = ","; 
        Regex regx = new Regex(separatorChar + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        //string[] line = regx.Split();

        //Find the record to be edited
        for (int i = 0; i < oLines.Length; i++)
        {
            //string[] fields = oLines[i].Split(',');
            string[] fields = regx.Split(oLines[i]);

            // Define the query expression. Selects users in file2 which are in both files.
            
            IEnumerable<string> myquery =
                from nLine in nLines
                where nLine.Contains(fields[2])
                select nLine;
              
            if (myquery.Count() > 0)
            {

                //string[] nFields = query.First().Split(',');
                string[] nFields = regx.Split(myquery.First());

                //IF (Google wPhone && GIS wPhone isnt empty && GwPhone contains GIS wPhone) || GwPhone isnt empty && GwPhone contains +420 || +421
                if ((fields[11] != "" && nFields[4] != "" && fields[11].Contains(nFields[5])) || (fields[11] != "" && fields[11].Contains("+420") || fields[11].Contains("+421")))
                {
                    addRecord2(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], nFields[3], fields[8], fields[9], fields[10], fields[11], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], nFields[6], fields[19], fields[20], fields[21], fields[22], fields[23], fields[24], fields[25], fields[26], fields[27], @tempFile);
                }
                //IF GwPhone isnt empty && (GISwPhone && GIShPhone is empty ) then keep GwPhone
                else if (fields[11] != "" && nFields[4] == "" && nFields[5] == "")
                {
                    addRecord2(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], nFields[3], fields[8], fields[9], fields[10], fields[11], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], nFields[6], fields[19], fields[20], fields[21], fields[22], fields[23], fields[24], fields[25], fields[26], fields[27], @tempFile);
                }
                //if Google wPhone is empty and GIS wPhone is empty then put personal number there
                else if (fields[11] == "" && nFields[4] == "")
                {
                    addRecord2(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], nFields[3], fields[8], fields[9], fields[10], nFields[5], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], nFields[6], fields[19], fields[20], fields[21], fields[22], fields[23], fields[24], fields[25], fields[26], fields[27], @tempFile);
                }
                else
                {
                    addRecord2(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], nFields[3], fields[8], fields[9], fields[10], nFields[4], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], nFields[6], fields[19], fields[20], fields[21], fields[22], fields[23], fields[24], fields[25], fields[26], fields[27], @tempFile);
                }
            }
            else
            {
                addRecord2(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7], fields[8], fields[9], fields[10], fields[11], fields[12], fields[13], fields[14], fields[15], fields[16], fields[17], fields[18], fields[19], fields[20], fields[21], fields[22], fields[23], fields[24], fields[25], fields[26], fields[27], @tempFile);
            }
            
            

        }

        //Delete old files
        File.Delete(@oFilepath);
        File.Delete(@nFilepath);

        //Rename new file
        System.IO.File.Move(tempFile, oFilepath);
        
        Console.WriteLine("Records updated...");
    }         
    catch (Exception ex)
    {
        Console.WriteLine("Program did an error.");
        throw new ApplicationException("Error: ", ex);
    }
}

//Updates records in Satori signatures
static void addRecord(string user, string firstName, string lastName, string email, string phone, string mobile, string address, string department, string jobtitle, string userTwitter, string customField1, string customField2, string customField3, string customField4, string customField5, string customField6, string customField7, string customField8, string customField9, string customField10, string filepath)
{
    try
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
        {
            file.WriteLine(user + "," + firstName + "," + lastName + "," + email + "," + phone + "," + mobile + "," + address + "," + department + "," + jobtitle + "," + userTwitter + "," + customField1 + "," + customField2 + "," + customField3 + "," + customField4 + "," + customField5 + "," + customField6 + "," + customField7 + "," + customField8 + "," + customField9 + "," + customField10);
            file.Close();
        }
    }
    catch (Exception ex)
    {

        throw new ApplicationException("Error: ", ex);
    }
}

//Updates records in Google DB
static void addRecord2(string firstName, string lastName, string email, string password, string passwordHash, string path, string newEmail, string recoveryEmail, string homeEmail, string workSecondaryEmail, string recoveryPhone, string workPhone, string homePhone, string mobilePhone, string workAddress, string homeAddress, string employeeID, string employeeType, string employeeTitle, string managerEmail, string department, string costCenter, string buildingID, string floorName, string floorSection, string changePassword, string newStatus, string advancedProtection, string filepath)
{
    try
    {
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filepath, true))
        {
            file.WriteLine(firstName + "," + lastName + "," + email + "," + password + "," + passwordHash + "," + path + "," + newEmail + "," + recoveryEmail + "," + homeEmail + "," + workSecondaryEmail + "," + recoveryPhone + "," + workPhone + "," + homePhone + "," + mobilePhone + "," + workAddress + "," + homeAddress + "," + employeeID + "," + employeeType + "," + employeeTitle + "," + managerEmail + "," + department + "," + costCenter + "," + buildingID + "," + floorName + "," + floorSection + "," + changePassword + "," + newStatus + "," + advancedProtection);
            file.Close();
        }
    }
    catch (Exception ex)
    {

        throw new ApplicationException("Error: ", ex);
    }
}
