using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NJsonSchema.Validation;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace CryptoCompare
{
    public static class Utils
    {
        public static void WriteSchema(string sampleJson, FileInfo outputSchema)
        {
            Directory.CreateDirectory(outputSchema.DirectoryName);

            try
            {
                JsonSchema4 schema = JsonSchema4.FromSampleJson(sampleJson);
                File.WriteAllText(outputSchema.FullName, schema.ToJson());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public static void WriteCSharp(string sampleJson, FileInfo outputCSharp, CSharpGeneratorSettings settings)
        {
            JsonSchema4 schema = JsonSchema4.FromSampleJson(sampleJson);
            WriteCSharp(schema, outputCSharp, settings);
        }

        public static void WriteCSharp(FileInfo inputSchema, FileInfo outputCSharp, CSharpGeneratorSettings settings)
        {
            JsonSchema4 schema;

            try
            {
                schema = JsonSchema4.FromJsonAsync(File.ReadAllText(inputSchema.FullName)).Result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            WriteCSharp(schema, outputCSharp, settings);
        }

        public static void WriteCSharp(JsonSchema4 schema, FileInfo outputCSharp, CSharpGeneratorSettings settings)
        {
            Directory.CreateDirectory(outputCSharp.DirectoryName);

            CSharpGenerator generator = new CSharpGenerator(schema, settings);

            try
            {
                File.WriteAllText(outputCSharp.FullName, generator.GenerateFile());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public static void WriteServices(FileInfo inputSchema, FileInfo outputCSharp, SwaggerToCSharpClientGeneratorSettings settings)
        {
            SwaggerDocument document;
            try
            {
                document = SwaggerDocument.FromFileAsync(inputSchema.FullName).Result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            SwaggerToCSharpClientGenerator generator = new SwaggerToCSharpClientGenerator(document, settings);

            try
            {
                File.WriteAllText(outputCSharp.FullName, generator.GenerateFile());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public static void Validate(FileInfo jsonFile, FileInfo schemaFile)
        {
            JsonSchema4 schema;
            try
            {
                schema = JsonSchema4.FromFileAsync(schemaFile.FullName).Result;
            }
            catch (Exception exception)
            {
                Print(exception);
                throw;
            }

            JToken json;
            try
            {
                using (FileStream filej = jsonFile.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader readerj = new StreamReader(filej))
                using (JsonReader r = new JsonTextReader(readerj))
                {
                    json = JToken.ReadFrom(r);
                }
            }
            catch (Exception exception)
            {
                Print(exception);
                throw;
            }

            Validate(json, schema);
        }

        public static void Validate(JToken json, JsonSchema4 schema)
        {
            JsonSchemaValidator validator = new JsonSchemaValidator();
            ICollection<ValidationError> errors = validator.Validate(json, schema);
            foreach (ValidationError error in errors)
            {
                Debug.WriteLine(error.Path + " : " + error.Kind);
            }
        }

        public static void Print(Exception e)
        {
            while (e != null)
            {
                Debug.WriteLine(e.Message);
                e = e.InnerException;
            }
        }
    }
}