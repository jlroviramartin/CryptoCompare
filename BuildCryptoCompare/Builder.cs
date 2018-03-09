using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag.CodeGeneration.CSharp;

namespace CryptoCompare
{
    // https://www.newtonsoft.com/json
    // https://github.com/RSuter/NJsonSchema
    public class Builder
    {
        /// <summary>
        /// This method download json samples.
        /// </summary>
        public static void DownloadSamples(bool overwrite = true)
        {
            DownloadPrice(overwrite);
            DownloadPricemulti(overwrite);
            DownloadPricemultifull(overwrite);
            DownloadGenerateAvg(overwrite);

            DownloadHistoday(overwrite);
            DownloadHistohour(overwrite);
            DownloadHistominute(overwrite);
            DownloadPricehistorical(overwrite);
            DownloadDayAvg(overwrite);

            DownloadSubsWatchlist(overwrite);
            DownloadSubs(overwrite);

            DownloadProviders(overwrite);
            DownloadNews(overwrite);

            DownloadExchanges(overwrite);
            DownloadCoinlist(overwrite);
            DownloadLimit(overwrite);
        }

        /// <summary>
        /// This method creates (json) schemas based on samples.
        /// </summary>
        public static void BuildAllSchemas(bool overwrite = true)
        {
            BuildSchema("Price", overwrite);
            BuildSchema("PriceMulti", overwrite);
            BuildSchema("PriceMultiFull", overwrite);
            BuildSchema("GenerateAverage", overwrite);

            BuildSchema("HistoDay", overwrite);
            BuildSchema("HistoHour", overwrite);
            BuildSchema("HistoMinute", overwrite);
            BuildSchema("PriceHistorical", overwrite);
            BuildSchema("DayAverage", overwrite);

            BuildSchema("SubscriptionsWatchlist", overwrite);
            BuildSchema("SubscriptionsByPair", overwrite);

            BuildSchema("NewsProviders", overwrite);
            BuildSchema("NewsArticles", overwrite);

            BuildSchema("Exchanges", overwrite);
            BuildSchema("CoinList", overwrite);
            BuildSchema("Limit", overwrite);
        }

        /// <summary>
        /// This method validates json samples against their (json) schemas.
        /// </summary>
        public static void ValidateAllSchemas()
        {
            Validate("Price");
            Validate("PriceMulti");
            Validate("PriceMultiFull");
            Validate("GenerateAverage");

            Validate("HistoDay", "HistoricalData");
            Validate("HistoHour", "HistoricalData");
            Validate("HistoMinute", "HistoricalData");
            Validate("PriceHistorical", "PriceHistorical");
            Validate("DayAverage");

            Validate("SubscriptionsWatchlist");
            Validate("SubscriptionsByPair");

            Validate("NewsProviders");
            Validate("NewsArticles");

            Validate("Exchanges");
            Validate("CoinList");
            Validate("Limit");
        }

        /// <summary>
        /// This method builds the C# code based on (json) schemas.
        /// </summary>
        public static void BuildAllCSharp(bool overwrite = true)
        {
            WriteCSharp("Price", overwrite);
            WriteCSharp("PriceMulti", overwrite);
            WriteCSharp("PriceMultiFull", overwrite);
            WriteCSharp("GenerateAverage", overwrite);

            WriteCSharp("HistoricalData", overwrite);
            WriteCSharp("Pricehistorical", overwrite);
            WriteCSharp("DayAverage", overwrite);

            WriteCSharp("SubscriptionsWatchlist", overwrite);
            WriteCSharp("SubscriptionsByPair", overwrite);

            WriteCSharp("NewsProviders", overwrite);
            WriteCSharp("NewsArticles", overwrite);

            WriteCSharp("Exchanges", overwrite);
            WriteCSharp("CoinList", overwrite);
            WriteCSharp("Limit", overwrite);

            // Especial: used to subscribe to a stream.
            WriteCSharp("Subscription", overwrite);
        }

        /// <summary>
        /// This method builds the C# code based on a (json) swagger file.
        /// </summary>
        public static void BuildServices()
        {
            FileInfo inputSchema = new FileInfo(Path.Combine(outputDirectory, "cryptocompare.schema.json"));
            FileInfo outputCSharp = new FileInfo(Path.Combine(outputDirectory, "CryptoCompareServices.cs"));
            Utils.WriteServices(inputSchema, outputCSharp, GetSwaggerToCSharpClientGeneratorSettings("CryptoCompareServices"));
        }

        #region Price

        public static void DownloadPrice(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.PriceAsync(null, "ETH", "BTC", null, null, null).Result as JToken;
                        },
                        "Price",
                        overwrite);
        }

        public static void DownloadPricemulti(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.PricemultiAsync(null, "ETH,DASH", "BTC,USD,EUR", null, null, null).Result as JToken;
                        },
                        "PriceMulti",
                        overwrite);
        }

        public static void DownloadPricemultifull(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.PricemultifullAsync(null, "ETH,DASH", "BTC,USD,EUR", null, null, null).Result as JToken;
                        },
                        "PriceMultiFull",
                        overwrite);
        }

        public static void DownloadGenerateAvg(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.GenerateAvgAsync("BTC", "USD", "CCCAGG", null, null).Result as JToken;
                        },
                        "GenerateAverage",
                        overwrite);
        }

        #endregion

        #region Historical Data

        public static void DownloadHistoday(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.HistodayAsync(null, "BTC", "USD", null, null, null, null, null, null, null).Result as JToken;
                        },
                        "HistoDay",
                        overwrite);
        }

        public static void DownloadHistohour(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.HistohourAsync(null, "BTC", "USD", null, null, null, null, null, null).Result as JToken;
                        },
                        "HistoHour",
                        overwrite);
        }

        public static void DownloadHistominute(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.HistominuteAsync(null, "BTC", "USD", null, null, null, null, null, null).Result as JToken;
                        },
                        "HistoMinute",
                        overwrite);
        }

        public static void DownloadPricehistorical(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.PricehistoricalAsync(null, "BTC", "USD,EUR", null, null, null, null, null).Result as JToken;
                        },
                        "PriceHistorical",
                        overwrite);
        }

        public static void DownloadDayAvg(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.DayAvgAsync(null, "BTC", "USD", null, null, null, null, null, null).Result as JToken;
                        },
                        "DayAverage",
                        overwrite);
        }

        #endregion

        #region Streaming

        public static void DownloadSubsWatchlist(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.SubsWatchlistAsync("BTC,ETH,XMR,MLN,DASH", "XMR", null, null).Result as JToken;
                        },
                        "SubscriptionsWatchlist",
                        overwrite);
        }

        public static void DownloadSubs(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.SubsAsync("ETH", "BTC,EUR", null, null).Result as JToken;
                        },
                        "SubscriptionsByPair",
                        overwrite);
        }

        #endregion

        #region News

        public static void DownloadProviders(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.ProvidersAsync(null, null).Result as JToken;
                        },
                        "NewsProviders",
                        overwrite);
        }

        public static void DownloadNews(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.NewsAsync(null, null, Lang.EN, null, null).Result as JToken;
                        },
                        "NewsArticles",
                        overwrite);
        }

        #endregion

        #region Other Info

        public static void DownloadExchanges(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.ExchangesAsync(null, null).Result as JToken;
                        },
                        "Exchanges",
                        overwrite);
        }

        public static void DownloadCoinlist(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.CoinlistAsync(null, null).Result as JToken;
                        },
                        "CoinList",
                        overwrite);
        }

        public static void DownloadLimit(bool overwrite = false)
        {
            WriteSample(() =>
                        {
                            CryptoCompareServices client = new CryptoCompareServices(baseUrl);
                            return client.LimitAsync().Result as JToken;
                        },
                        "Limit",
                        overwrite);
        }

        #endregion

        #region private

        private static void WriteSample(Func<JToken> fjobj, string name, bool overwrite)
        {
            Directory.CreateDirectory(outputSampleDirectory);
            Directory.CreateDirectory(outputSchemaDirectory);

            string samplePath = Path.Combine(outputSampleDirectory, name + ".json");
            if (!File.Exists(samplePath) || overwrite)
            {
                JToken jobj = fjobj();

                try
                {
                    string beautified = jobj.ToString(Formatting.Indented);
                    File.WriteAllText(samplePath, beautified);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }

            /*string schemaPath = Path.Combine(outputSchemaDirectory, name + ".schema.json");
            if (!File.Exists(schemaPath) || overwrite)
            {
                jobj = jobj ?? fjobj();
                WriteSchema(jobj.ToString(), new FileInfo(schemaPath));
            }*/
        }

        private static void BuildSchema(string name, bool overwrite)
        {
            Directory.CreateDirectory(outputSampleDirectory);
            Directory.CreateDirectory(outputSchemaDirectory);

            string samplePath = Path.Combine(outputSampleDirectory, name + ".json");
            string schemaPath = Path.Combine(outputSchemaDirectory, name + ".schema.json");

            if (!File.Exists(schemaPath) || overwrite)
            {
                try
                {
                    JsonSchema4 schema = JsonSchema4.FromSampleJson(File.ReadAllText(samplePath));
                    File.WriteAllText(schemaPath, schema.ToJson());
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                    throw;
                }
            }
        }

        private static void WriteCSharp(string name, bool overwrite)
        {
            string schemaPath = Path.Combine(outputSchemaDirectory, name + ".schema.json");
            JsonSchema4 schema;
            try
            {
                schema = JsonSchema4.FromFileAsync(schemaPath).Result;
            }
            catch (Exception exception)
            {
                Utils.Print(exception);
                throw;
            }

            string outputCSharp = Path.Combine(outputCSharpDirectory, name + ".cs");
            if (!File.Exists(outputCSharp) || overwrite)
            {
                Utils.WriteCSharp(schema, new FileInfo(outputCSharp), GetCSharpGeneratorSettings());
            }
        }

        private static void Validate(string name)
        {
            Validate(name, name);
        }

        private static void Validate(string sampleName, string schemaName)
        {
            string samplePath = Path.Combine(outputSampleDirectory, sampleName + ".json");
            string schemaPath = Path.Combine(outputSchemaDirectory, schemaName + ".schema.json");
            Debug.WriteLine("Validating: " + sampleName + " against " + schemaName);
            Debug.Indent();

            Utils.Validate(new FileInfo(samplePath), new FileInfo(schemaPath));

            Debug.Unindent();
        }

        private static CSharpGeneratorSettings GetCSharpGeneratorSettings()
        {
            return new CSharpGeneratorSettings
            {
                ClassStyle = CSharpClassStyle.Poco,
                Namespace = _namespace,
                SchemaType = SchemaType.JsonSchema,
                TypeNameGenerator = new MyTypeNameGenerator(),
                EnumNameGenerator = new MyEnumNameGenerator(),
                PropertyNameGenerator = new MyPropertyNameGenerator(),
            };
        }

        private static SwaggerToCSharpClientGeneratorSettings GetSwaggerToCSharpClientGeneratorSettings(string className)
        {
            return new SwaggerToCSharpClientGeneratorSettings
            {
                ClassName = className,
                GenerateClientInterfaces = true,
                //OperationNameGenerator = new 
                //ParameterNameGenerator = new 
                CSharpGeneratorSettings =
                {
                    ClassStyle = CSharpClassStyle.Poco,
                    Namespace = _namespace,
                    SchemaType = SchemaType.JsonSchema,
                    TypeNameGenerator = new MyTypeNameGenerator(),
                    EnumNameGenerator = new MyEnumNameGenerator(),
                    PropertyNameGenerator = new MyPropertyNameGenerator(),
                }
            };
        }

        private static readonly string baseUrl = "https://min-api.cryptocompare.com";

        private static readonly string _namespace = "CryptoCompare.Services";
        private static readonly string outputDirectory = @".\schemas";
        private static readonly string outputSampleDirectory = Path.Combine(outputDirectory, "sample");
        private static readonly string outputSchemaDirectory = Path.Combine(outputDirectory, "schema");
        private static readonly string outputCSharpDirectory = Path.Combine(outputDirectory, "csharp");

        #endregion

        private class MyPropertyNameGenerator : CSharpPropertyNameGenerator
        {
            public override string Generate(JsonProperty property)
            {
                string name = base.Generate(property);
                return name;
            }
        }

        private class MyTypeNameGenerator : DefaultTypeNameGenerator
        {
            public override string Generate(JsonSchema4 schema,
                                            string typeNameHint,
                                            IEnumerable<string> reservedTypeNames)
            {
                string name = base.Generate(schema, typeNameHint, reservedTypeNames);
                if (name.EndsWith("2"))
                {
                    name = name.Substring(0, name.IndexOf("2")) + "_BORRAR";
                }

                return name;
            }

            protected override string Generate(JsonSchema4 schema, string typeNameHint)
            {
                string name = base.Generate(schema, typeNameHint);
                if (name.EndsWith("2"))
                {
                    name = name.Substring(0, name.IndexOf("2")) + "_BORRAR";
                }

                return name;
            }
        }

        private class MyEnumNameGenerator : IEnumNameGenerator
        {
            private static readonly DefaultEnumNameGenerator aux = new DefaultEnumNameGenerator();

            public string Generate(int index, string name, object value, JsonSchema4 schema)
            {
                string name2 = aux.Generate(index, name, value, schema);
                return name2;
            }
        }
    }
}