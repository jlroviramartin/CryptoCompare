using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using System.Web.Services.Description;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using Quobject.SocketIoClientDotNet.Client;

namespace CryptoCompare
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1st We build basic services.
            //BuilderFirst.BuildServices();

            // 2nd We build al json samples and schemas.
            //Builder.DownloadSamples();
            //Builder.BuildAllSchemas();

            // 3rd we must edit the schemas.

            //Builder.DownloadSamples();
            Builder.ValidateAllSchemas();
            Builder.BuildAllCSharp();
            Builder.BuildServices();

            //RestTest("https://api.coindesk.com/v1/bpi/currentprice.json").Wait();
            //CoinList().Wait();
            //Test().Wait();
            //TestWebSocket();
        }

        public static void Generate(string wsdlPath, string outputFilePath)
        {
            if (File.Exists(wsdlPath) == false)
            {
                return;
            }

            ServiceDescription wsdlDescription = ServiceDescription.Read(wsdlPath);
            ServiceDescriptionImporter wsdlImporter = new ServiceDescriptionImporter();

            wsdlImporter.ProtocolName = "Soap12";
            wsdlImporter.AddServiceDescription(wsdlDescription, null, null);
            wsdlImporter.Style = ServiceDescriptionImportStyle.Server;

            wsdlImporter.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties;

            CodeNamespace codeNamespace = new CodeNamespace();
            CodeCompileUnit codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(codeNamespace);

            ServiceDescriptionImportWarnings importWarning = wsdlImporter.Import(codeNamespace, codeUnit);

            if (importWarning == 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringWriter stringWriter = new StringWriter(stringBuilder);

                CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp");
                codeProvider.GenerateCodeFromCompileUnit(codeUnit, stringWriter, new CodeGeneratorOptions());

                stringWriter.Close();

                File.WriteAllText(outputFilePath, stringBuilder.ToString(), Encoding.UTF8);
            }
            else
            {
                Console.WriteLine(importWarning);
            }
        }

        public static async Task RestTest(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            Task<string> stringTask = client.GetStringAsync(url);
            String json = await stringTask;

            // https://www.newtonsoft.com/json
            // https://github.com/RSuter/NJsonSchema

            JToken parsedJson = JToken.Parse(json);
            string beautified = parsedJson.ToString(Formatting.Indented);
            Debug.WriteLine("--------------------------------------------------");
            Debug.WriteLine(beautified);

            JsonSchema4 jsonSchema = JsonSchema4.FromSampleJson(beautified);
            string beautifiedSchema = jsonSchema.ToJson();
            Debug.WriteLine("--------------------------------------------------");
            Debug.WriteLine(beautifiedSchema);

            CSharpGenerator generator = new CSharpGenerator(jsonSchema);
            string file = generator.GenerateFile();
            Debug.WriteLine("--------------------------------------------------");
            Debug.WriteLine(file);
        }

        public static async Task CoinList()
        {
            UriBuilder uriBuilder = new UriBuilder("https://min-api.cryptocompare.com/data/all/coinlist");
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["extraParams"] = "your_app_name";
            uriBuilder.Query = query.ToString();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

            client.DefaultRequestHeaders.Add("User-Agent", ".NET User-Agent");
            Task<string> stringTask = client.GetStringAsync(uriBuilder.Uri);

            String json = await stringTask;
            {
                JToken parsedJson = JToken.Parse(json);
                string beautified = parsedJson.ToString(Formatting.Indented);
                Debug.WriteLine(beautified);
            }
        }

        public static void TestCSharpGenerator()
        {
            string str =
                @"{ 
                ""properties"": {
                    ""Pet"": {
                        ""type"": ""object"",
                        ""additionalProperties"": {
                            ""type"": ""boolean""
                        }
                    }
                }
            }";

            JsonSchema4 jsonSchema = JsonSchema4.FromJsonAsync(str).Result;
            CSharpGeneratorSettings settings = new CSharpGeneratorSettings();
            settings.ClassStyle = CSharpClassStyle.Poco;
            CSharpGenerator generator = new CSharpGenerator(jsonSchema, settings);
            Debug.WriteLine(generator.GenerateFile());
        }

        public static void TestWebSocket()
        {
            String url = "wss://streamer.cryptocompare.com";

            ManualResetEvent resetEvent = new ManualResetEvent(false);

            Socket socket = IO.Socket(url);

            socket.On(Socket.EVENT_CONNECT, () =>
            {
                Debug.WriteLine("Connect");

                socket.Emit("SubAdd", "{ subs: ['0~Cryptsy~BTC~USD'] }");
            });

            socket.On(Socket.EVENT_MESSAGE, (data) =>
            {
                Debug.WriteLine("Message " + data);
                socket.Disconnect();
            });

            socket.On("m", (data) =>
            {
                Debug.WriteLine("m " + data);
                socket.Disconnect();
            });

            socket.On(Socket.EVENT_ERROR, (data) =>
            {
                Debug.WriteLine("Error " + data);
            });
            socket.On(Socket.EVENT_CONNECT_ERROR, () =>
            {
                Debug.WriteLine("Error");
            });
            socket.On(Socket.EVENT_CONNECT_TIMEOUT, () =>
            {
                Debug.WriteLine("Timeout");
            });

            socket.On(Socket.EVENT_DISCONNECT, () =>
            {
                resetEvent.Set();
                Debug.WriteLine("Disconnect");

            });

            resetEvent.WaitOne();
            socket.Close();
        }
    }
}