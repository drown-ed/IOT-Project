using AladinBookSearch.Logic;
using AladinBookSearch.Model;
using MahApps.Metro.Controls;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace AladinBookSearch
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private async void BtnSearchBook_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtSearchName.Text))
            {
                await Commons.ShowMessageAsync("검색", "검색할 도서명을 입력하세요.");
                return;
            }

            try
            {
                SearchBook(TxtSearchName.Text);
            }
            catch (Exception ex)
            {
                await Commons.ShowMessageAsync("오류", $"오류 발생 : {ex.Message}");
            }
        }

        private void TxtSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearchBook_Click(sender, e);
            }
        }

        private async void SearchBook(string BookName)
        {
            string apikey = "ttbotooe121744001";
            string encoding_BookName = HttpUtility.UrlEncode(BookName, Encoding.UTF8);
            string openApiUri = $"http://www.aladin.co.kr/ttb/api/ItemSearch.aspx?ttbkey={apikey}" +
                                  $"&Query={encoding_BookName}" + $"&MaxResults=100";
            //  + "&QueryType=Title" + "&MaxResults=10" + "&start=1&" + $"SearchTarget=Book" + "&output=xml&Version=20131101"
            string result = string.Empty;

            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(openApiUri); // URL을 넣어서 객체를 생성
                res = await req.GetResponseAsync(); // 요청한 결과를 비동기 응답에 할당
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
                res.Close();
            }

            XmlDocument XmlFile = new XmlDocument();
            XmlFile.LoadXml(result);

            XmlNodeList XmlList = XmlFile.GetElementsByTagName("object");
            string totalResult = "";
            foreach(XmlNode XmlNode in XmlList)
            {
                totalResult = XmlNode["totalResults"].InnerText;               
            };

            Results.Text = $"검색 결과 {totalResult}건";

            XmlNodeList XmlLists = XmlFile.GetElementsByTagName("item");

            var bookItems = new List<BookItem>();
            foreach (XmlNode item in XmlLists)
            {
                var BookItems = new BookItem()
                {
                    Title = item["title"].InnerText,
                    Author = item["author"].InnerText,
                    Cover = item["cover"].InnerText,
                    Publisher = item["publisher"].InnerText,
                    PriceSales = item["priceSales"].InnerText
                };
                bookItems.Add(BookItems);
                Debug.WriteLine(BookItems.Title, BookItems.Author);
            }
            this.DataContext = bookItems;
        }

        private async void GrdResult_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                string posterPath = string.Empty;

                if (GrdResult.SelectedItem is BookItem) // openAPI로 검색된 영화의 포스터
                {
                    var book = GrdResult.SelectedItem as BookItem;
                    posterPath = book.Cover;
                    BookName.Text = book.Title;
                    Publisher.Text = "출판사 : "+ book.Publisher;
                    Price.Text = "판매가 : " + book.PriceSales +"원";
                }
                
                Debug.WriteLine(posterPath);
                if (string.IsNullOrEmpty(posterPath)) // 포스터 이미지가 없으면 No_Picture
                {
                    ImgPoster.Source = new BitmapImage(new Uri("/No_Picture.png", UriKind.RelativeOrAbsolute));
                }
                else // 포스터이미지 경로가 있으면
                {
                    ImgPoster.Source = new BitmapImage(new Uri($"{posterPath}", UriKind.RelativeOrAbsolute));
                }
            }
            catch
            {
                await Commons.ShowMessageAsync("오류", $"이미지로드 오류발생");
            }
        }
    }
}
