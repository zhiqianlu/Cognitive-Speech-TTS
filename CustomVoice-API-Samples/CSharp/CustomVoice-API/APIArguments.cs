﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomVoice_API
{
    public class APIArguments
    {
        public static Dictionary<string, string> GetApiKindAndAction(string[] args)
        {
            if (args.Length <= 0)
            {
                return null;
            }

            var arguments = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    arguments.Add("apikind", args[i].ToLower());
                }
                else if (i == 1)
                {
                    arguments.Add("action", args[i].ToLower());
                    break;
                }
            }

            return arguments;
        }

        public static Dictionary<string, string> GetArguments(string[] args)
        {
            if(args.Length <= 0)
            {
                return null;
            }

            var arguments = new Dictionary<string, string>();
            string argumentName = "";

            for (int i = 2; i < args.Length; i++)
            {
                if (argumentName == "")
                {
                    argumentName = args[i].Replace("-", "").ToLower();
                }
                else
                {
                    arguments.Add(argumentName, args[i]);
                    argumentName = "";
                }
            }
            return arguments;
        }

        public static bool NoAPIKind(Dictionary<string, string> ApiKindAndAction)
        {
            if (ApiKindAndAction == null || !ApiKindAndAction.Keys.Contains("apikind") || !Enum.IsDefined(typeof(APIKind), ApiKindAndAction["apikind"]))
            {
                return true;
            }

            return false;
        }

        public static bool NoAction(Dictionary<string, string> ApiKindAndAction)
        {
            if (ApiKindAndAction == null || !ApiKindAndAction.Keys.Contains("action") || !Enum.IsDefined(typeof(Action), ApiKindAndAction["action"]))
            {
                return true;
            }

            return false;
        }

        public static bool ParametersNoMatch(Dictionary<string, string> arguments, List<string> requiredParameters)
        {
            if(requiredParameters.Except(arguments.Keys).Count() > 0)
            {
                return true;
            }

            return false;
        }

        public static Dictionary<string, List<string>> GetParameters(APIKind apiKind, Action action )
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            List<string> RequiredParameters = null;
            List<string> OptionalParameters = null;

            switch ($"{apiKind}-{action}")
            {
                case nameof(APIKind.project) + "-" + nameof(Action.create):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "gender", "locale"};
                        OptionalParameters = new List<string>() { "description" };
                        break;
                    }
                case nameof(APIKind.project) + "-" + nameof(Action.get):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.project) + "-" + nameof(Action.delete):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "projectId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.dataset) + "-" + nameof(Action.uploaddataset):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "projectId", "gender", "locale", "wavePath", "scriptPath"};
                        OptionalParameters = new List<string>() { "description"};
                        break;
                    }
                case nameof(APIKind.dataset) + "-" + nameof(Action.uploaddatasetwithlongaudio):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "projectId", "gender", "locale", "wavePath", "scriptPath" };
                        OptionalParameters = new List<string>() { "description" };
                        break;
                    }
                case nameof(APIKind.dataset) + "-" + nameof(Action.uploaddatasetwithaudioonly):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "projectId", "gender", "locale", "wavePath" };
                        OptionalParameters = new List<string>() { "description" };
                        break;
                    }
                case nameof(APIKind.dataset) + "-" + nameof(Action.get):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.dataset) + "-" + nameof(Action.getbyprojectid):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "projectId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.dataset) + "-" + nameof(Action.delete):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "datasetId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.model) + "-" + nameof(Action.create):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "projectId", "gender", "locale", "dataset"};
                        OptionalParameters = new List<string>() { "description", "isNeuralTTS", "isMixlingual" };
                        break;
                    }
                case nameof(APIKind.model) + "-" + nameof(Action.get):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.model) + "-" + nameof(Action.getbyprojectid):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "projectId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.model) + "-" + nameof(Action.delete):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "modelId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.voicetest) + "-" + nameof(Action.create):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "projectId", "modelId", "script"};
                        OptionalParameters = new List<string>() { "isSSML"};
                        break;
                    }
                case nameof(APIKind.voicetest) + "-" + nameof(Action.get):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "modelId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.voicetest) + "-" + nameof(Action.getbyprojectid):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "projectId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.voicetest) + "-" + nameof(Action.delete):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "voiceTestId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.endpoint) + "-" + nameof(Action.create):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "locale", "projectId", "modelId" };
                        OptionalParameters = new List<string>() { "description" };
                        break;
                    }
                case nameof(APIKind.endpoint) + "-" + nameof(Action.get):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.endpoint) + "-" + nameof(Action.getbyprojectid):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "projectId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.batchsynthesis) + "-" + nameof(Action.getbysynthesisid):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "batchSynthesisId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.endpoint) + "-" + nameof(Action.delete):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "endpointId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.endpoint) + "-" + nameof(Action.call):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "issuetokenurl", "endpointUrl", "voiceName", "locale", "script", "outputFile" };
                        OptionalParameters = new List<string>() { "isSSML" };
                        break;
                    }
                case nameof(APIKind.batchsynthesis) + "-" + nameof(Action.create):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "name", "inputTextPath", "locale", "models" };
                        OptionalParameters = new List<string>() { "description", "outputFormat", "isConcatenateResult" };
                        break;
                    }
                case nameof(APIKind.batchsynthesis) + "-" + nameof(Action.get):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.batchsynthesis) + "-" + nameof(Action.getvoices):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                case nameof(APIKind.batchsynthesis) + "-" + nameof(Action.delete):
                    {
                        RequiredParameters = new List<string>() { "subscriptionKey", "hostURI", "batchSynthesisId" };
                        OptionalParameters = new List<string>();
                        break;
                    }
                default:
                    {
                        return null;
                    }
            }

            result.Add("Required", RequiredParameters.Select(x => x.ToLower()).ToList());
            result.Add("Optional", OptionalParameters.Select(x => x.ToLower()).ToList());
            return result;
        }
    }
}
