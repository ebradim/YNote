using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Ynote
{
   
    public class Note
    {
        public  string NoteText { get; set; }
        public  string NoteTitle { get; set; }
        public  string NoteColor { get; set; }
        public  string NoteTime { get; set; }
        public  string Important { get; set; }
        public int num { get; set; }
        public  string Tag { get; set; }
        public string CatName { get; set; }
        public string CatInfo { get; set; }
        public string Password { get; set; }
        public string DateModified { get; set; }
        public string AudioPath { get; set; }
        public Note()
        {

        }
        public Note( int num,string color, string text , string time , string important , string tag , string password , string modified,string title,string audiopath)
        {
            this.num = num;
            NoteText = text;
            NoteColor = color;
            NoteTime = time;
            Important = important;
            Tag = tag;
            Password = password;
            DateModified = modified;
            NoteTitle = title;
            AudioPath = audiopath;
        }



    }
    public static class CONNECTION
    {
        public static string DBPath
        {
            get
            {
                return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "ynotelite.db");

            }
        }
    }
    public static class INFONOTE
    {
          public static string NoteText { get; set; }
          public static string NoteTitle { get; set; }
    public static string NoteColor { get; set; }
    public static string TagColor { get; set; }
    public static  string NoteTime { get; set; }
    public static string Important { get; set; }
    public static int NoteID { get; set; }
    public static string Tag { get; set; }
    public static string Password { get; set; }
    public static string FullText { get; set; }
    public static string FontColor { get; set; }
    public static string FontColor2 { get; set; }
    public static string DateModified { get; set; }
    public static string AudioPath { get; set; }
}


   
    public class Colorize
    {
        public string setTextColor(string color)
        {
            INFONOTE.FontColor = color switch
            {
                "#FEA3A3" => "#202020",
                "#FBA9BD" => "#f2003b",
                "#8d84f7" => "#202020",
                "#bdb8ff" => "#2c059c",
                "#38ef7d" => "#ffffff",
                "#f8ff7a" => "#202020",
                "#84e2f7" => "#202020",
                "#3700b3" => "#ffffff",
                "#cf6679" => "#202020",
                "#03dac6" => "#202020",
                "#bb86fc" => "#202020",
                "#121212" => "#ffffff",
                "#B00020" => "#000000",
                "#FF0266" => "#202020",
                _ => "000000",
            };
            return "#000000";

        }

        public string SetTextTwoColor(string color)
        {
            INFONOTE.FontColor2 = color switch
            {
                "#FEA3A3" => "#202020",
                "#FBA9BD" => "#f2003b",
                "#8d84f7" => "#202020",
                "#bdb8ff" => "#2c059c",
                "#38ef7d" => "#ffffff",
                "#f8ff7a" => "#202020",
                "#84e2f7" => "#202020",
                "#3700b3" => "#ffffff",
                "#cf6679" => "#202020",
                "#03dac6" => "#202020",
                "#bb86fc" => "#202020",
                "#121212" => "#ffffff",
                "#B00020" => "#000000",
                "#FF0266" => "#202020",
                _ => "000000",
            };
            return "#000000";

        }

    }















    public interface IStringConv
    {
        void setTextColor();
    }
   
    

}