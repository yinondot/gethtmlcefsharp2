using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HtmlAgilityPack;
using CefSharp;
using System.Threading;

namespace GetHtmlCefSharp
{
   /// <summary>
   /// Interaction logic for Window1.xaml
   /// </summary>
   public partial class Window1 : Window
   {
      public Window1()
      {
         InitializeComponent();

      }


      string filePath = @"C:\Users\yinon\OneDrive\Desktop\tableExample.html";
      // string filePath ;
      private void Window_Loaded(object sender, RoutedEventArgs e)
      {


      }

      private async void Button_Click(object sender, RoutedEventArgs e)
      {
         Browser.LoadHtml(tbURL.Text);
         Browser.Address = tbURL.Text;
        
    //     File.WriteAllText(filePath,  Browser.GetSourceAsync());

         try
         {
             tbHtml.Clear();


            HtmlDocument doc = new HtmlDocument();
            doc.Load(filePath);
            //  var tablesCollection = doc.DocumentNode.SelectNodes("//table");
            var rowCollection = doc.DocumentNode.SelectNodes("//tr");
            //var divOverall = doc.DocumentNode.SelectNodes("//div[@id='tab-match-head-2-head']");
            //var headCollection = divOverall[0].SelectNodes(".//thead");
            //var teamHeader = headCollection[0].SelectNodes(".//td");
            //string teamHeaderHome = teamHeader[0].InnerHtml;
            //var bodyCollection = divOverall[0].SelectNodes(".//tbody");
            //var rowCollection = bodyCollection[0].SelectNodes(".//tr");

            int count = 0;

            List<string> stringResults = new List<string>();
            for (int i = 0; i < 10; i++)
            {
               var strongCollection = rowCollection[i].SelectNodes(".//strong");
               try
               {
                  stringResults.Add(strongCollection[1].InnerHtml);
               }
               catch (Exception)
               {

                  count++;
               }

            }

            List<int> results = new List<int>();
            foreach (var item in stringResults)
            {
               string temp;
               temp = item.Trim();
               var twoResults = temp.Split(':');
               twoResults[0] = twoResults[0].Trim();
               twoResults[1] = twoResults[1].Trim();
               int diff = int.Parse(twoResults[0]) - int.Parse(twoResults[1]);
               if (diff > 20)
               {
                  diff = 20;
               }
               if (diff < -20)
               {
                  diff = -20;
               }
               results.Add(diff);
            }
            double sum = 0;
            foreach (var item in results)
            {
               sum += item;
               tbHtml.Text += item + Environment.NewLine;
            }
            tbHtml.Text += $"{count} draw results" + Environment.NewLine;
            tbHtml.Text += $"Result={sum / 10}";
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.Message);
         }
      }


   }

   
}
