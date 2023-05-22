using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Json;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Requests;
using MimeKit;
using Newtonsoft.Json;
using Google.Apis.Drive.v3;
using System.Collections.ObjectModel;

namespace CuratorsHelper
{
    /// <summary>
    /// Логика взаимодействия для CloudPage.xaml
    /// </summary>
    public partial class CloudPage : Page
    {
        private static string userId;
        private static readonly string credentialsPath = "credentials.json";
        private static readonly string appName = "CuratorsHelper";
        private static GmailService service;
        private static readonly string[] scopes = { GmailService.Scope.GmailCompose, GmailService.Scope.GmailSend, GmailService.Scope.GmailReadonly, GmailService.Scope.GmailModify };
        private static Curators cur;
        public CloudPage()
        {
            InitializeComponent();

            string tokensPath = "tokens";

            if (!Directory.Exists(tokensPath))
            {
                Directory.CreateDirectory(tokensPath);
            }

            var currentCur = CuratorsHelperEntities.GetContext().Curators.ToList();
            cur = currentCur.FirstOrDefault(p => p.id_pass == UserId.ID);
            userId = cur.email;
            currentCur = currentCur.Where(p => p.id_pass != UserId.ID).ToList();
            ToTextBox.ItemsSource = currentCur;



            string tokenFileName = $"token_{cur.id_curator}.json";

            if (!File.Exists(System.IO.Path.Combine(tokensPath, tokenFileName)))
            {
                File.Create(System.IO.Path.Combine(tokensPath, tokenFileName)).Close();
            }

            UserCredential credential;
            using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    userId,
                    CancellationToken.None,
                    new FileDataStore(tokensPath, true)).Result;
            }

            service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = appName,
            });

            GetMessage();

        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CreateMessage((string)ToTextBox.SelectedValue, SubjectTextBox.Text, MessageTextBox.Text);
                MyMessage.Show("Сообщение успешно доставленно");
                SubjectTextBox.Clear();
                MessageTextBox.Clear();
            }
            catch (Exception msg)
            {
                MyMessage.Show("Что-то пошло не так");
            }
        }


        private static void CreateMessage(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(cur.FIO.Split(' ')[1] + ' ' + cur.FIO.Split(' ')[2], cur.email));
            message.To.Add(new MailboxAddress("", to));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using (var stream = new MemoryStream())
            {
                message.WriteTo(stream);
                var messageBytes = stream.GetBuffer();

                var gmailMessage = new Google.Apis.Gmail.v1.Data.Message
                {
                    Raw = Base64UrlEncode(messageBytes)
                };

                var request = service.Users.Messages.Send(gmailMessage, userId);
                request.Execute();
            }
        }

        private static string Base64UrlEncode(byte[] input)
        {
            var base64 = Convert.ToBase64String(input);
            var base64Url = base64.Replace("+", "-").Replace("/", "_").TrimEnd('=');
            return base64Url;
        }

        private void GetMessage()
        {
            try
            {
                var messageRequest = service.Users.Messages.List(userId);
                messageRequest.Q = "in:inbox";
                messageRequest.MaxResults = 10;
                var messages = messageRequest.Execute().Messages;

                if (messages != null && messages.Count > 0)
                {
                    foreach (var message in messages)
                    {
                        var messageInfoRequest = service.Users.Messages.Get(userId, message.Id);
                        var messageInfo = messageInfoRequest.Execute();

                        string from = messageInfo.Payload.Headers.FirstOrDefault(h => h.Name == "From")?.Value;
                        string subject = messageInfo.Payload.Headers.FirstOrDefault(h => h.Name == "Subject")?.Value;
                        string snippet = messageInfo.Snippet;
                        
                        MessageListView.Items.Add(new
                        {
                            From = from,
                            Subject = subject,
                            Snippet = snippet
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MyMessage.Show($"Что-то пошло не так");
            }
        }
    }

}
