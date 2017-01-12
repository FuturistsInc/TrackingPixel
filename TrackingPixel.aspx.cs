using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_TrackingPixel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //str_Text adds on the latest e-mail respondent to the list of respondents already on the tracking list.
        string str_Text = System.IO.File.ReadAllText(Server.MapPath(".") + @"\TrackingPixel.txt")
            + Environment.NewLine + HttpContext.Current.Request.Params[0] + "," + helper_GetDateTimeNow();
        System.IO.File.WriteAllText((Server.MapPath(".") + @"\TrackingPixel.txt"), str_Text);
        Response.Redirect("TrackingPixel.bmp", false);

    }
    public static string helper_GetDateTimeNow()
    {
        System.Net.WebClient client = new System.Net.WebClient();
        string str_DownloadedText = client.DownloadString("https://www.timeanddate.com/worldclock/singapore/singapore");
        string str_Date = helper_GetStringBetweenTwoStrings(str_DownloadedText, @"<span id=ctdat>", @"</span>"); //.Split(',')[1].Trim()
        string str_Time = helper_GetStringBetweenTwoStrings(str_DownloadedText, @"<span id=ct class=h1>", @"</span>");
        return (str_Date + "," + str_Time);
    }
    public static string helper_GetStringBetweenTwoStrings(string str_FullString, string str_StartString, string str_EndString)
    {
        //Test with System.Diagnostics.Debug.WriteLine(CLS_Generic.Chp05_7GetStringBetweenTwoStrings("<html><p>Trial Test</p></html>", "<p>", "</p>"));
        return str_FullString.Split(new string[] { str_StartString }, StringSplitOptions.None)[1].Split(new string[] { str_EndString }, StringSplitOptions.None)[0].Trim();
    }
}