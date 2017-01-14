using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TrackingPixel_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string str_Params = HttpContext.Current.Request.Params[0];

        //Use command URL?CreateCPNCampaignName to create new folder CampaignName
        if (str_Params == "GetDateTimeNow")
        {            
            p_status.InnerText = helper_GetDateTimeNow();
        }
        else if (str_Params == "ListOfCPN")
        {
            string[] filesindirectory = System.IO.Directory.GetDirectories(Server.MapPath("."));
            p_status.InnerText = "";
            foreach (string subdir in filesindirectory)
            {
                string CPNName = new System.IO.DirectoryInfo(subdir).Name;
                p_status.InnerText = p_status.InnerText + CPNName + ";";
            }            
        }
        else if (str_Params.Length > 9)
        {
            if (str_Params.Substring(0, 9) == "CreateCPN")
            {
                string CPNName = str_Params.Substring(9);
                string CPNPathAndName = Server.MapPath(".") + @"\" + CPNName;
                string str_ModifiedContents = "";

                if (!System.IO.Directory.Exists(CPNPathAndName))
                {
                    System.IO.Directory.CreateDirectory(CPNPathAndName);
                    System.IO.File.Copy((Server.MapPath(".") + @"\TrackingPixel.bmp"), (CPNPathAndName + @"\" + CPNName + ".bmp"));
                    System.IO.File.Copy((Server.MapPath(".") + @"\TrackingPixel.txt"), (CPNPathAndName + @"\" + CPNName + ".txt"));
                    System.IO.File.Copy((Server.MapPath(".") + @"\TrackingPixel.accdb"), (CPNPathAndName + @"\" + CPNName +".accdb"));
                    str_ModifiedContents = System.IO.File.ReadAllText(Server.MapPath(".") + @"\TrackingPixel.aspx");
                    str_ModifiedContents = str_ModifiedContents.Replace("TrackingPixel", CPNName);
                    System.IO.File.WriteAllText((CPNPathAndName + @"\" + CPNName + ".aspx"), str_ModifiedContents);

                    str_ModifiedContents = System.IO.File.ReadAllText(Server.MapPath(".") + @"\TrackingPixel.aspx.cs");
                    str_ModifiedContents = str_ModifiedContents.Replace("TrackingPixel", CPNName);
                    System.IO.File.WriteAllText((CPNPathAndName + @"\" + CPNName + ".aspx.cs"), str_ModifiedContents);

                    str_ModifiedContents = System.IO.File.ReadAllText(Server.MapPath(".") + @"\EmailTemplate.html");
                    str_ModifiedContents = str_ModifiedContents.Replace("|CPN|", CPNName);
                    System.IO.File.WriteAllText((CPNPathAndName + @"\EmailTemplate.html"), str_ModifiedContents);

                    p_status.InnerText = "Campaign " + CPNName + " successfully created." + Environment.NewLine + p_status.InnerText;
                }
                else
                {
                    p_status.InnerText = "Campaign already exists. Please choose another name for your campaign." + Environment.NewLine + p_status.InnerText;
                }

            }
            else if (str_Params.Substring(0, 9) == "DeleteCPN")
            {
                string CPNName = str_Params.Substring(9);
                string CPNPathAndName = Server.MapPath(".") + @"\" + CPNName;
                if (System.IO.Directory.Exists(CPNPathAndName))
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(CPNPathAndName);

                    foreach (System.IO.FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    System.IO.Directory.Delete(CPNPathAndName);
                    p_status.InnerText = "Campaign " + CPNName + " successfully deleted." + Environment.NewLine + p_status.InnerText;
                }
                else
                {
                    p_status.InnerText = "Campaign does not exist. No campaigns deleted." + Environment.NewLine + p_status.InnerText;
                }
            }
            else if (str_Params.Substring(0, 9) == "ExistsCPN")
            {
                string CPNName = str_Params.Substring(9);
                string CPNPathAndName = Server.MapPath(".") + @"\" + CPNName;
                if (System.IO.Directory.Exists(CPNPathAndName))
                {
                    p_status.InnerText = "Campaign exists." + Environment.NewLine + p_status.InnerText;
                }
                else
                {
                    p_status.InnerText = "Campaign does not exist." + Environment.NewLine + p_status.InnerText;
                }
            }
            else
            {
                p_status.InnerText = "Campaign action parameter not valid." + Environment.NewLine + p_status.InnerText;
            }
        }
        else
        {
            p_status.InnerText = "No campaign action selected." + Environment.NewLine + p_status.InnerText;
        }

    }

    public static string helper_GetDateTimeNow()
    {
        System.Net.WebClient client = new System.Net.WebClient();
        string str_DownloadedText = client.DownloadString("https://www.timeanddate.com/worldclock/singapore/singapore");
        string str_Date = helper_GetStringBetweenTwoStrings(str_DownloadedText, @"<span id=ctdat>", @"</span>");//.Split(',')[1].Trim()
        string str_Time = helper_GetStringBetweenTwoStrings(str_DownloadedText, @"<span id=ct class=h1>", @"</span>");
        return (str_Date + "," + str_Time);
    }
    public static string helper_GetStringBetweenTwoStrings(string str_FullString, string str_StartString, string str_EndString)
    {
        //Test with System.Diagnostics.Debug.WriteLine(CLS_Generic.Chp05_7GetStringBetweenTwoStrings("<html><p>Trial Test</p></html>", "<p>", "</p>"));
        return str_FullString.Split(new string[] { str_StartString }, StringSplitOptions.None)[1].Split(new string[] { str_EndString }, StringSplitOptions.None)[0].Trim();
    }
}