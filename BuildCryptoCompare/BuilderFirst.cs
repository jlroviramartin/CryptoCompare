using System;
using System.IO;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace CryptoCompare
{
    public class BuilderFirst
    {
        public static void BuildServices()
        {
            FileInfo inputSchema = new FileInfo(Path.Combine(outputDirectory, "cryptocompare_First.schema.json"));
            FileInfo outputCSharp = new FileInfo(Path.Combine(outputDirectory, "CryptoCompareServices.cs"));
            Utils.WriteServices(inputSchema, outputCSharp, GetSwaggerToCSharpClientGeneratorSettings("CryptoCompareServices"));
        }

        private static CSharpGeneratorSettings GetCSharpGeneratorSettings()
        {
            return new CSharpGeneratorSettings
            {
                ClassStyle = CSharpClassStyle.Poco,
                Namespace = _namespace,
                SchemaType = SchemaType.JsonSchema,
                //TypeNameGenerator = new MyTypeNameGenerator(),
                //EnumNameGenerator = new MyEnumNameGenerator(),
                //PropertyNameGenerator = new MyPropertyNameGenerator(),
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
                    //TypeNameGenerator = new MyTypeNameGenerator(),
                    //EnumNameGenerator = new MyEnumNameGenerator(),
                    //PropertyNameGenerator = new MyPropertyNameGenerator(),
                }
            };
        }

        private static readonly string _namespace = "CryptoCompare";
        private static readonly string outputDirectory = @".\schemas";
    }
}